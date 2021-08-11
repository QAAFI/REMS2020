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
using Models.Storage;
using Models.WaterModel;
using Models.Soils.Nutrients;
using System.Reflection;

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

        private readonly int numModelsToExport = 17;

        public Markdown Summary { get; } = new();

        /// <summary>
        /// Creates an .apsimx file and populates it with experiment models
        /// </summary>
        public async override Task Run()
        {
            // Reset the markdown report
            Summary.Clear();
            Summary.AddSubHeading("REMS export summary", 1);

            var simulations = new Simulations();
            var store = new DataStore();

            simulations.Children.Add(store);

            // Find the experiments            
            var exps = (await QueryManager.Request(new ExperimentsQuery()))
                .Where(e => Experiments.Contains(e.Name));

            // Check if replacements is necessary
            if (exps.Any(e => e.Crop == "Sorghum"))
            {
                var info = Manager.GetFileInfo("SorghumReplacements");
                var model = JsonTools.LoadJson<Replacements>(info);
                simulations.Children.Add(model);
            }

            // Convert each experiment into an APSIM model
            foreach (var (ID, Name, Crop) in exps)
            {
                OnNextItem(Name);

                var model = await CreateExperiment(Name, ID);
                simulations.Children.Add(model);
            }

            var memo = new Memo { Name = "ExportSummary", Text = Summary.Text };
            simulations.Children.Add(memo);
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
        private async Task<T> Request<T>(IRequest<T> query, params IModel[] children) where T : IModel
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
        private IModel Create<M>(string name = null, params IModel[] children)
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
            
            Summary.AddSubHeading(name + ':', 2);

            var experiment =
            Create<Experiment>(name, new IModel[]
            {
                await Request(new FactorsQuery{ ExperimentId = id, Report = Summary }),
                Create<Simulation>(name, new IModel[]
                {
                    await Request(new ClockQuery{ ExperimentId = id, Report = Summary }),
                    Create<Summary>(),
                    await Request(new WeatherQuery{ ExperimentId = id, Report = Summary }),
                    Create<SoilArbitrator>(),
                    await Request(new ZoneQuery{ ExperimentId = id, Report = Summary }, new IModel[]
                    {
                        Create<Report>("DailyReport"),
                        Create<Report>("HarvestReport"),
                        await QueryManager.Request(new ManagersQuery {ExperimentId = id }),
                        await Request(new PlantQuery { ExperimentId = id, Report = Summary }),
                        await CreateSoilModel(id),
                        new SurfaceOrganicMatter()
                        {
                            ResourceName = "SurfaceOrganicMatter",
                            InitialResidueName = "wheat_stubble",
                            InitialResidueType = "wheat",
                            InitialCNR = 80.0
                        },
                        Create<Operations>("Irrigations"),
                        Create<Operations>("Fertilisations"),
                        Create<Irrigation>("Irrigation"),
                        await Request(new FertiliserQuery { ExperimentId = id }),
                        new MicroClimate
                        {
                            a_interception = 0,
                            b_interception = 1,
                            c_interception = 0,
                            d_interception = 0,
                            SoilHeatFluxFraction = 0.4,
                            MinimumHeightDiffForNewLayer = 0,
                            NightInterceptionFraction = 0.5,
                            ReferenceHeight = 2
                        }
                    })
                })
            });
            Summary.AddLine("\n");
            return experiment;
        }   

        private async Task<IModel> CreateSoilModel(int id)
        {   
            var query = new SoilModelTraitsQuery { ExperimentId = id };
            var traits = await QueryManager.Request(query);

            var template = query.Crop.ToUpper() == "SORGHUM"
                ? JsonTools.LoadJson<Soil>(Manager.GetFileInfo("DefaultSoil"))
                : JsonTools.LoadJson<Soil>(Manager.GetFileInfo("SorghumSoil"));

            if (traits["Thickness"] is not double[] thickness)
            {
                Summary.AddSubHeading("Soil model", 2);
                Summary.AddLine("No soil layer data found. A template soil model has been used. " +
                    "Check the sensibility before running the simulation.");

                template.FindDescendant<SoilCrop>().Name = query.Crop + "Soil";
                return template;
            }

            double[] getValues<T>(string name)
            {
                // If we found the trait in the query
                if (traits.TryGetValue(name, out double[] result) && result is not null)
                    return result;

                // Look for a template value
                var type = typeof(T);
                if (template.FindDescendant<T>() is T model)
                    if (type.GetProperty(name) is PropertyInfo info)
                        if (info.GetValue(model) is double[] values)
                            if (values.Length <= thickness.Length)
                                return values.Take(thickness.Length).ToArray();

                // If no template exists, return the default
                return default;                
            }

            var missing = traits.Where(t => t.Value is null);
            if (missing.Any())
            {
                Summary.AddSubHeading("Soil model:", 2);
                Summary.AddLine("No data was found for the following soil layer traits:");
                Summary.AddList(missing.Select(t => t.Key));
                Summary.AddLine("\nThese values have been provided a default value." +
                    "It is recommended to check the sensibility of these values before " +
                    "running the simulation.");
            }
            
            var physical = new Physical
            {
                Thickness = thickness,
                BD = getValues<Physical>(nameof(Physical.BD)),
                AirDry = getValues<Physical>(nameof(Physical.AirDry)),
                LL15 = getValues<Physical>(nameof(Physical.LL15)),
                DUL = getValues<Physical>(nameof(Physical.DUL)),
                SAT = getValues<Physical>(nameof(Physical.SAT)),
                KS = getValues<Physical>(nameof(Physical.KS))
            };
            var soilcrop = new SoilCrop
            {
                Name = query.Crop + "Soil",
                LL = getValues<SoilCrop>(nameof(SoilCrop.LL)),
                KL = getValues<SoilCrop>(nameof(SoilCrop.KL)),
                XF = getValues<SoilCrop>(nameof(SoilCrop.XF))
            };
            physical.Children.Add(soilcrop);
            var balance = new WaterBalance
            {
                Name = "SoilWater",
                Thickness = thickness,
                SWCON = getValues<WaterBalance>(nameof(WaterBalance.SWCON)),
                KLAT = getValues<WaterBalance>(nameof(WaterBalance.KLAT)),
                SummerDate = "1-Nov",
                SummerU = 1.5,
                SummerCona = 6.5,
                WinterDate = "1-Apr",
                WinterU = 1.5,
                WinterCona = 6.5,
                DiffusConst = 40,
                DiffusSlope = 16,
                Salb = 0.2,
                CN2Bare = 85,
                DischargeWidth = double.NaN,
                CatchmentArea = double.NaN,
                ResourceName = "WaterBalance"
            };
            var organic = new Organic
            {
                Name = nameof(Organic),
                Depth = query.Depth,
                Thickness = thickness,
                Carbon = getValues<Organic>(nameof(Organic.Carbon)),
                SoilCNRatio = getValues<Organic>(nameof(Organic.SoilCNRatio)),
                FBiom = getValues<Organic>(nameof(Organic.FBiom)),
                FInert = getValues<Organic>(nameof(Organic.FInert)),
                FOM = getValues<Organic>(nameof(Organic.FOM))
            };
            var chemical = new Chemical
            {
                Depth = query.Depth,
                Thickness = thickness,
                NO3N = getValues<Chemical>(nameof(Chemical.NO3N)),
                NH4N = getValues<Chemical>(nameof(Chemical.NH4N)),
                PH = getValues<Chemical>(nameof(Chemical.PH))
            };
            var water = new InitialWater
            {
                PercentMethod = 0,
                FractionFull = 0.6
            };
            var sample = new Sample
            {
                Name = "Initial conditions",
                Depth = new[] { "0-180" },
                Thickness = new[] { 1800.0 },
                NH4 = new[] { 1.0 }
            };
            var temperature = new CERESSoilTemperature
            {
                Name = "Temperature"
            };
            var nitrogen = Create<SoilNitrogen>("SoilNitrogen", new IModel[]
            {
                new SoilNitrogenNO3 { Name = "NO3" },
                new SoilNitrogenNH4 { Name = "NH4" },                    
                new SoilNitrogenUrea { Name = "Urea" },
                new SoilNitrogenPlantAvailableNO3 { Name = "PlantAvailableNO3" },
                new SoilNitrogenPlantAvailableNH4 { Name = "PlantAvailableNH4" }
            });

            var models = new IModel[] { physical, balance, organic, chemical, water, sample, temperature, nitrogen };
            return await Request(new SoilQuery { ExperimentId = id, Report = Summary }, models);
            
        }
    }
}
