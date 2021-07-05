using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using MediatR;
using Models;
using Models.Core;
using Models.Core.ApsimFile;
using Models.Factorial;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Surface;

using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using WindowsClient.Utilities;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages the construction and output of an .apsimx file
    /// </summary>
    public class ApsimXporter : TaskRunner
    {
        /// <summary>
        /// The name of the .apsimx to output to
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The names of the experiments in the database to export
        /// </summary>
        public IEnumerable<string> Experiments { get; set; }

        public IFileManager Manager { get; set; }

        /// <inheritdoc/>
        public override int Items => Experiments.Count();

        /// <inheritdoc/>
        public override int Steps => Items * numModelsToExport;

        private readonly int numModelsToExport = 29;

        private Markdown summary = new Markdown();

        /// <summary>
        /// Creates an .apsimx file and populates it with experiment models
        /// </summary>
        public async override Task Run()
        {
            // Reset the markdown report
            summary.Clear();
            summary.AddSubHeading("REMS export summary", 1);

            var simulations = JsonTools.LoadJson<Simulations>(Manager.GetFileInfo("Simulation"));

            var sorghum = JsonTools.LoadJson<Folder>(Manager.GetFileInfo("Sorghum"));

            foreach (IModel model in sorghum.Children)
                simulations.Children.Add(model);

            // Find the experiments
            var folder = new Folder { Name = "Experiments" };
            var experiments = await QueryManager.Request(new ExperimentsQuery());

            // Convert each experiment into an APSIM model
            foreach (var experiment in experiments)
            {
                if (!Experiments.Contains(experiment.Value)) continue;

                OnNextItem(experiment.Value);

                var model = await CreateExperiment(experiment.Value, experiment.Key);
                folder.Children.Add(model);
            }

            simulations.Children.Add(folder);

            var memo = new Memo { Name = "ExportSummary", Text = summary.Text };
            simulations.Children.Add(memo);

            simulations.ParentAllDescendants();
            SanitiseNames(simulations);

            // Save the file
            File.WriteAllText(FileName, FileFormat.WriteToString(simulations));
        }

        /// <summary>
        /// Invokes a request for the specified Apsim model
        /// </summary>
        /// <typeparam name="R">A request that can be parameterised with specific values</typeparam>
        /// <param name="id">The experiment data is requested from</param>
        /// <param name="children">Any child models to include</param>
        private async Task<T> Request<T>(IRequest<T> query, IEnumerable<IModel> children = null) where T : IModel
        {
            var model = await QueryManager.Request(query);

            // Attach child models
            if (children != null) foreach (var child in children)
                model.Children.Add(child);

            Reporter.Increment(1);

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

            Reporter.Increment(1);

            return model;
        }

        private void SanitiseNames(IModel model)
        {
            model.Name = model.Name.Replace('.', '_');

            foreach (IModel child in model.Children)
                SanitiseNames(child);
        }

        /// <summary>
        /// Creates and populates an experiment model for Apsim
        /// </summary>
        /// <param name="name">The name of the experiment</param>
        /// <param name="id">The database ExperimentId</param>
        private async Task<IModel> CreateExperiment(string name, int id)
        {
            // Creates a model tree
            // The indentation below indicates depth in the tree, grouped models at the same indentation 
            // represent siblings in the tree
            
            summary.AddSubHeading(name + ':', 2);

            var experiment =
            Create<Experiment>(name, new IModel[]
            {
                await Request(new FactorsQuery{ ExperimentId = id, Report = summary }),
                Create<Simulation>(name, new IModel[]
                {
                    await Request(new ClockQuery{ ExperimentId = id, Report = summary }),
                    Create<Summary>(),
                    await Request(new WeatherQuery{ ExperimentId = id, Report = summary }),
                    Create<SoilArbitrator>(),
                    await Request(new ZoneQuery{ ExperimentId = id, Report = summary }, new IModel[]
                    {
                        Create<Report>("DailyReport"),
                        Create<Report>("HarvestReport"),
                        await QueryManager.Request(new ManagersQuery {ExperimentId = id }),
                        await Request(new PlantQuery{ ExperimentId = id, Report = summary }),
                        await CreateSoilModel(id),
                        CreateOrganicMatter(),
                        Create<Operations>("Irrigations"),
                        Create<Operations>("Fertilisations"),
                        Create<Irrigation>("Irrigation"),
                        Create<Fertiliser>("Fertiliser"),
                        new MicroClimate
                        {
                            a_interception = 0,
                            b_interception = 1,
                            c_interception = 0,
                            d_interception = 0,
                            soil_albedo = 0.13,
                            SoilHeatFluxFraction = 0.4,
                            MinimumHeightDiffForNewLayer = 0,
                            NightInterceptionFraction = 0.5,
                            ReferenceHeight = 2
                        }
                    })
                })
            });
            summary.AddLine("\n");
            return experiment;
        }   

        private async Task<IModel> CreateSoilModel(int id)
        {
            var soil = 
                await Request(new SoilQuery { ExperimentId = id, Report = summary }, new IModel[] 
                {
                    new InitialWater { PercentMethod = 0, FractionFull = 0.6 },
                    await Request(new PhysicalQuery{ ExperimentId = id, Report = summary }, new IModel[] 
                    {
                        await Request(new SoilCropQuery{ ExperimentId = id, Report = summary })
                    }),
                    await Request(new WaterBalanceQuery{ ExperimentId = id, Report = summary }),
                    Create<SoilNitrogen>("SoilNitrogen", new IModel[] 
                    {
                        Create<SoilNitrogenNH4>("NH4"),
                        Create<SoilNitrogenNO3>("NO3"),
                        Create<SoilNitrogenUrea>("Urea"),
                        Create<SoilNitrogenPlantAvailableNH4>("PlantAvailableNH4"),
                        Create<SoilNitrogenPlantAvailableNO3>("PlantAvailableNO3")
                    }),
                    await Request(new OrganicQuery{ ExperimentId = id, Report = summary }),
                    await Request(new ChemicalQuery{ ExperimentId = id, Report = summary }),
                    Create<CERESSoilTemperature>("Temperature"),
                    await Request(new SampleQuery{ ExperimentId = id, Report = summary })
                });

            return soil;
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
