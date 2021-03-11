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
    /// <summary>
    /// Manages the construction and output of an .apsimx file
    /// </summary>
    public class ApsimXporter : ProgressTracker
    {
        /// <summary>
        /// The name of the .apsimx to output to
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The names of the experiments in the database to export
        /// </summary>
        public IEnumerable<string> Experiments { get; set; }

        /// <inheritdoc/>
        public override int Items => Experiments.Count();

        /// <inheritdoc/>
        public override int Steps => Items * 29;

        /// <summary>
        /// Creates an .apsimx file and populates it with experiment models
        /// </summary>
        public async override Task Run()
        {
            // Create the file
            var path = Path.Combine("DataFiles", "apsimx", "Sorghum.apsimx");
            var simulations = JsonTools.LoadJson<Simulations>(path);

            // Find the experiments
            var folder = new Folder() { Name = "Experiments" };
            var experiments = await InvokeQuery(new ExperimentsQuery());

            // Convert each experiment into an APSIM model
            foreach (var experiment in experiments)
            {
                if (!Experiments.Contains(experiment.Value)) continue;

                OnNextItem(experiment.Value);

                var model = await CreateExperiment(experiment.Value, experiment.Key);
                folder.Children.Add(model);
            }

            simulations.Children.Add(folder);
            simulations.ParentAllDescendants();

            // Save the file
            File.WriteAllText(FileName, FileFormat.WriteToString(simulations));

            OnTaskFinished();
        }

        #region General
        /// <summary>
        /// Invokes a request for the specified Apsim model
        /// </summary>
        /// <typeparam name="R">A request that can be parameterised with specific values</typeparam>
        /// <param name="id">The experiment data is requested from</param>
        /// <param name="children">Any child models to include</param>
        private async Task<IModel> Request<R>(int id, IEnumerable<IModel> children = null)
            where R : IRequest<IModel>, IParameterised, new()
        {
            // Create and invoke a request for data
            var request = new R();
            request.Parameterise(id);
            var model = await InvokeQuery(request);

            // Attach child models
            if (children != null) foreach (var child in children)
                model.Children.Add(child);

            OnIncrementProgress();

            return model;
        }

        /// <summary>
        /// Creates an Apsim model
        /// </summary>
        /// <typeparam name="M">The model type</typeparam>
        /// <param name="name">The model name</param>
        /// <param name="children">Any child models to include</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates and populates an experiment model for Apsim
        /// </summary>
        /// <param name="name">The name of the experiment</param>
        /// <param name="id">The database ExperimentId</param>
        private async Task<IModel> CreateExperiment(string name, int id)
        {
            // Creates a model tree
            // The indentation below indicates depth in the tree, adjacent models at the same indentation 
            // represent siblings in the tree

            var experiment = 
            Create<Experiment>(name, new IModel[] {
                Create<Factors>("Factors", new IModel[] {
                    await Request<PermutationQuery>(id ,null)}),
                Create<Simulation>(name, new IModel[] {
                    await Request<ClockQuery>(id),
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
