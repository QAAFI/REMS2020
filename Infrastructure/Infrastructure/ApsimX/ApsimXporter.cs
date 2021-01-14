using MediatR;
using Models;
using Models.Core;
using Models.Core.ApsimFile;

using Rems.Application.Common;
using Rems.Application.CQRS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using Models.Factorial;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Surface;
using Rems.Application.Common.Interfaces;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimXporter : ProgressTracker
    {
        public string FileName { get; set; }

        public IEnumerable<string> Experiments { get; set; }

        public override int Items 
        { 
            get => Experiments.Count();
        }

        public override int Steps => Items * 29;        

        public async override Task Run()
        {
            var path = Path.Combine("DataFiles", "apsimx", "Sorghum.apsimx");
            var simulations = JsonTools.LoadJson<Simulations>(path);

            var folder = new Folder() { Name = "Experiments" };
            var experiments = await InvokeQuery(new ExperimentsQuery());

            foreach (var experiment in experiments)
            {
                if (!Experiments.Contains(experiment.Value)) continue;

                OnNextItem(experiment.Value);

                var model = await CreateExperiment(experiment.Value, experiment.Key);
                folder.Children.Add(model);
            }

            simulations.Children.Add(folder);
            simulations.ParentAllDescendants();

            File.WriteAllText(FileName, FileFormat.WriteToString(simulations));

            OnTaskFinished();
        }

        #region General
        private async Task<IModel> Request<R>(int id, IEnumerable<IModel> children = null)
            where R : IRequest<IModel>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(id);

            var model = await InvokeQuery(request);

            if (children != null) foreach (var child in children)
                model.Children.Add(child);

            OnIncrementProgress();

            return model;
        }

        private IModel Create<M>(string name = null, IEnumerable<IModel> children = null)
            where M : IModel, new()
        {
            var model = new M();

            if (!(name is null)) model.Name = name;

            if (children != null) foreach (var child in children)
                model.Children.Add(child);

            OnIncrementProgress();

            return model;
        }
        #endregion

        private async Task<IModel> CreateExperiment(string name, int id)
        {
            var experiment = 
            Create<Experiment>(name, new IModel[] {
                Create<Factors>("Factors", new IModel[] {
                    await Request<PermutationQuery>(id ,null)}),
                Create<Simulation>("Base", new IModel[] {
                    await Request<ClockQuery>(id, new IModel[] {
                        Create<Summary>(),
                        await Request<WeatherQuery>(id),
                        Create<SoilArbitrator>(),
                        await Request<ZoneQuery>(id, new IModel[] {
                            await Request<PlantQuery>(id),
                            await Request<SoilQuery>(id, new IModel[] {
                                await Request<PhysicalQuery>(id, new IModel[] {
                                    await Request<SoilCropQuery>(id)
                                }),
                                await Request<WaterBalanceQuery>(id),
                                Create<SoilNitrogen>("SoilNitrogen", new IModel[] {
                                    Create<SoilNitrogenNH4>("NH4"),
                                    Create<SoilNitrogenNO3>("NO3"),
                                    Create<SoilNitrogenUrea>("Urea"),
                                    Create<SoilNitrogenPlantAvailableNH4>("PlantAvailableNH4"),
                                    Create<SoilNitrogenPlantAvailableNO3>("PlantAvailableNO3")
                                }),
                                await Request<OrganicQuery>(id),
                                await Request<ChemicalQuery>(id),
                                await Request<SampleQuery>(id),
                                Create<CERESSoilTemperature>("SoilTemperature")
                            }),
                            CreateOrganicMatter(),
                            Create<Operations>(),
                            Create<Irrigation>("Irrigation"),
                            Create<Fertiliser>("Fertiliser"),
                            Create<Report>("Daily"),
                            Create<Report>("Harvest")
                        })
                    })
                })
            });

            return experiment;
        }

        private IModel CreateOrganicMatter()
        {
            var organic = new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            };

            return organic;
        }
    }
}
