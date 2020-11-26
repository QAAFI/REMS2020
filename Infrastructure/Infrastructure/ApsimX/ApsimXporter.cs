using MediatR;
using Models;
using Models.Core;
using Models.Core.ApsimFile;

using Rems.Application.Common;
using Rems.Application.CQRS;

using System;
using System.Collections.Generic;
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

        public override int Items => OnSendQuery(new ExperimentCount());
        public override int Steps => Items * 30;

        public ApsimXporter(QueryHandler query) : base(query)
        { }

        public async override Task Run()
        {
            var path = Path.Combine("DataFiles", "apsimx", "Sorghum.apsimx");
            var simulations = JsonTools.LoadJson<Simulations>(path);

            var folder = new Folder() { Name = "Experiments" };
            var experiments = OnSendQuery(new ExperimentsQuery());

            foreach (var experiment in experiments)
            {
                OnNextItem(experiment.Value);
                await Task.Run(() =>
                {
                    var model = CreateExperiment(experiment.Value, experiment.Key);
                    folder.Children.Add(model);
                });
            }

            simulations.Children.Add(folder);
            simulations.ParentAllDescendants();

            File.WriteAllText(FileName, FileFormat.WriteToString(simulations));

            OnTaskFinished();
        }

        #region General
        private IModel Query<R>(int id, IEnumerable<IModel> children = null)
            where R : IRequest<IModel>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(id);

            var model = OnSendQuery(request);

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

        private IModel CreateExperiment(string name, int id)
        {
            var experiment = 
            Create<Experiment>(name, new IModel[] {
                Create<Factors>("Factors", new IModel[] {
                    Query<PermutationQuery>(id ,null)}),
                Create<Simulation>("Base", new IModel[] {
                    Query<ClockQuery>(id, new IModel[] {
                        Create<Summary>(),
                        Query<WeatherQuery>(id),
                        Create<SoilArbitrator>(),
                        Query<ZoneQuery>(id, new IModel[] {
                            Query<PlantQuery>(id),
                            Query<SoilQuery>(id, new IModel[] {
                                Query<PhysicalQuery>(id, new IModel[] {
                                    Query<SoilCropQuery>(id)
                                }),
                                Query<WaterBalanceQuery>(id),
                                Create<SoilNitrogen>("SoilNitrogen", new IModel[] {
                                    Create<SoilNitrogenNH4>("NH4"),
                                    Create<SoilNitrogenNO3>("NO3"),
                                    Create<SoilNitrogenUrea>("Urea"),
                                    Create<SoilNitrogenPlantAvailableNH4>("PlantAvailableNH4"),
                                    Create<SoilNitrogenPlantAvailableNO3>("PlantAvailableNO3")
                                }),
                                Query<OrganicQuery>(id),
                                Query<ChemicalQuery>(id),
                                Query<SampleQuery>(id),
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
