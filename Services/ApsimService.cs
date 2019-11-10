using Models;
using Models.Core;
using Models.Core.Run;
using Models.Graph;
using Models.PMF;
using Models.PostSimulationTools;
using Models.Report;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Storage;
using Models.Surface;
using REMS;
using REMS.Context;
using REMS.Context.Entities;
using Services.Interfaces;
using Services.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Services
{
    public static class ApsimService
    {
        public static void RunApsimFile(this IApsimX apsimx)
        {
            ApsimX apsim = apsimx as ApsimX;
            var tmp = new Runner(apsim.Simulations);
            var list = tmp.Run();

        }

        public static void SaveApsimFile(this IApsimX apsimx, string filename)
        {
            ApsimX apsim = apsimx as ApsimX;
            apsim.Simulations.FileName = filename;
            apsim.Simulations.Write(filename);
        }

        public static IApsimX CreateApsimFile(this IREMSDatabase db, string filepath)
        {
            var context = (db as REMSDatabase).context;
            var Apsim = new ApsimX();
            Apsim.Simulations = new Simulations();
            //JBTest(sims);            
            Apsim.Simulations.Children.Add(GetDataStore(context, filepath));
            Apsim.Simulations.Children.Add(GetReplacements());  
            Apsim.Simulations.Children.Add(GetValidations(context));

            return Apsim;
        }

        public static void GenerateMetFiles(this IREMSDatabase db, string path)
        {
            var context = (db as REMSDatabase).context;

            var mets = from met in context.MetStations
                       select met;

            foreach (var met in mets)
            {
                string file = path + "\\" + met.Name + ".met";

                if (File.Exists(file)) continue;

                using var stream = new FileStream(file, FileMode.Create);
                using var writer = new StreamWriter(stream);

                writer.WriteLine("[weather.met.weather]");
                writer.WriteLine($"!experiment number = 1");
                writer.WriteLine($"!experiment = ");
                writer.WriteLine($"!station name = {met.Name}");
                writer.WriteLine($"latitude = {met.Latitude} (DECIMAL DEGREES)");
                writer.WriteLine($"longitude = {met.Longitude} (DECIMAL DEGREES)");
                writer.WriteLine($"tav = {met.TemperatureAverage} (oC)");
                writer.WriteLine($"amp = {met.Amp} (oC)\n");

                writer.WriteLine($"{"Year",-7}{"Day",3}{"maxt",8}{"mint",8}{"radn",8}{"Rain",8}");
                writer.WriteLine($"{" () ",-7}{" ()",3}{" ()",8}{" ()",8}{" ()",8}{" ()",8}");

                var dates = context.MetDatas
                    .Select(d => d.Date)
                    .Distinct()
                    .OrderBy(d => d.Date);

                var TMAX = context.Traits.First(t => t.Name == "TMAX");
                var TMIN = context.Traits.First(t => t.Name == "TMIN");
                var SOLAR = context.Traits.First(t => t.Name == "SOLAR");
                var RAIN = context.Traits.First(t => t.Name == "RAIN");

                foreach (var date in dates)
                {
                    var value = from data in context.MetDatas
                                where data.Date == date
                                where data.Value.HasValue
                                select data;

                    double tmax = Math.Round(value.FirstOrDefault(d => d.Trait == TMAX).Value.Value, 2);
                    double tmin = Math.Round(value.FirstOrDefault(d => d.Trait == TMIN).Value.Value, 2);
                    double radn = Math.Round(value.FirstOrDefault(d => d.Trait == SOLAR).Value.Value, 2);
                    double rain = Math.Round(value.FirstOrDefault(d => d.Trait == RAIN).Value.Value, 2);

                    writer.Write($"{date.Year,-7}{date.DayOfYear,3}{tmax,8}{tmin,8}{radn,8}{rain,8}\n");
                }
            }
        }

        private static string GetScript(string file)
        {
            StringBuilder builder = new StringBuilder();
            using var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                builder.AppendLine(reader.ReadLine());
            };

            return builder.ToString();
        }
                
        private static DataStore GetDataStore(REMSContext context, string filepath)
        {
            var store = new DataStore();

            var excel = new ExcelInput()
            {
                Name = "ExcelInput",
                FileName = "Observed.xlsx",
                SheetNames = new string[] { "Observed" }
            };

            var observed = new PredictedObserved()
            {
                PredictedTableName = "HarvestReport",
                ObservedTableName = "Observed",
                FieldNameUsedForMatch = "SimulationID",
                Name = "PredictedObserved"
            };

            store.Children.Add(excel);
            store.Children.Add(observed);

            var inputs = context.Treatments.Select(t => new Input() 
            { 
                Name = $"{t.Experiment.Crop.Name}_{t.Experiment.Name}_{t.TreatmentId}",
                FileName = $"{t.Experiment.Crop.Name}_{t.Experiment.Name}_{t.TreatmentId}.out"
            });            

            foreach (var input in inputs)
                if (!File.Exists(input.FileName)) File.Create($"{filepath}\\{input.FileName}").Close();

            store.Children.AddRange(inputs);

            return store;
        }

        private static Replacements GetReplacements()
        {
            var replacements = new Replacements() { Name = "Replacements" };

            using var stream = new StreamReader("Sorghum.json");
            using var reader = new JsonTextReader(stream);

            var serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };

            var sorghum = serializer.Deserialize<Plant>(reader);
            replacements.Children.Add(sorghum);

            var daily = new Report()
            {
                Name = "DailyReport",
                VariableNames = GetScript("Daily.txt").Replace("\r", "").Split("\n"),
                EventNames = new string[] { "[Clock].DoReport" }
            };

            var harvest = new Report()
            {
                Name = "HarvestReport",
                VariableNames = GetScript("Harvest.txt").Replace("\r", "").Split("\n"),
                EventNames = new string[] { "[Sorghum].Harvesting" }
            };

            replacements.Children.Add(daily);
            replacements.Children.Add(harvest);

            return replacements;
        }

        private static Folder GetValidations(REMSContext dbContext)
        {
            var validations = new Folder() { Name = "Validations" };

            validations.Children.Add(GetCombinedResults());

            foreach (var experiment in dbContext.Experiments)
            {
                var simulations = experiment.Treatments.Select(t => NewSorghumSimulation(t, dbContext));                
                var folder = new Folder() { Name = experiment.Name };
                folder.Children.AddRange(simulations);
                validations.Children.Add(folder);
            }

            var predictedObserved = new Dictionary<string, IEnumerable<string>>()
            {
                { "Biomass", new List<string>(){ "BiomassWt", "GrainGreenWt" } },
                { "StemLeafWt", new List<string>(){ "BiomassWt", "StemGreenWt", "LeafGreenWt" } },
                { "Grain", new List<string>(){ "GrainNo", "GrainGreenNConc" } },
                { "LAI", new List<string>(){ "LAI" } },
                { "LeafNo", new List<string>(){ "LeafNo" } },
                { "Stage", new List<string>(){ "Stage" } },
                { "BiomassN", new List<string>(){ "BiomassN", "GrainGreenN" } },
                { "SteamLeafN", new List<string>(){ "BiomassN", "StemGreenN", "LeafGreenN" } },
                { "SLN", new List<string>(){ "NO3", "SLN" } },
            };

            validations.Children.Add(NewPanel(predictedObserved, "PredictedObserved.cs.txt"));

            return validations;
        }

        public static Folder GetCombinedResults()
        {
            var folder = new Folder()
            {
                Name = "Combined Results"
            };            

            var variables = new List<string>()
            {
                "BiomassWt",
                "GrainGreenWt",
                "GrainGreenN",
                "biomass_n",
                "TPLA",
                "GrainNo",
                "GrainSize",
            };

            var axes = new List<Axis>()
            {
                new Axis()
                {
                    Type = Axis.AxisType.Bottom,
                    Inverted = false,
                    DateTimeAxis = false,
                    CrossesAtZero = false
                },
                new Axis()
                {
                    Type = Axis.AxisType.Left,
                    Inverted = false,
                    DateTimeAxis = false,
                    CrossesAtZero = false
                }
            };

            foreach (string variable in variables)
            {
                var series = new Series()
                {
                    Type = SeriesType.Scatter,
                    XAxis = Axis.AxisType.Bottom,
                    YAxis = Axis.AxisType.Left,
                    ColourArgb = 0,
                    FactorToVaryColours = "SimulationName",
                    Marker = 0,
                    MarkerSize = 0,
                    Line = LineType.None,
                    LineThickness = LineThicknessType.Normal,
                    Checkpoint = "Current",
                    TableName = "PredictedObserved",
                    XFieldName = $"Observed.{variable}",
                    YFieldName = $"Predicted.{variable}",
                    IncludeSeriesNameInLegend = false,
                    Cumulative = false,
                    CumulativeX = false
                };

                series.Children.Add(new Regression()
                {
                    ForEachSeries = false,
                    showOneToOne = true,
                    showEquation = true,
                    Name = "Regression"
                });

                var graph = NewGraph(variable, axes);                
                graph.Children.Add(series);
                folder.Children.Add(graph);
            }                

            return folder;
        }

        public static Graph NewGraph(string variable, List<Axis> axes)
        {
            
            var graph = new Graph()
            {
                Name = variable,
                Axis = axes,
                LegendPosition = Graph.LegendPositionType.BottomRight
            };

            return graph;
        }
        
        public static Simulation NewSorghumSimulation(Treatment treatment, REMSContext dbContext)
        {                    
            var simulation = new Simulation()
            {
                Name = $"{treatment.Experiment.Crop.Name}_{treatment.Experiment.Name}_{treatment.TreatmentId}"
            };

            var memo = new Memo()
            {
                Name = "Treatment factors",
                Text = GetTreatmentMemo(treatment, dbContext)
            };
            simulation.Children.Add(memo);

            simulation.Children.Add(new Clock()
            {
                Name = "Clock",
                StartDate = (DateTime)treatment.Experiment.BeginDate,
                EndDate = (DateTime)treatment.Experiment.EndDate
            });

            simulation.Children.Add(new Summary()
            {
                Name = "SummaryFile"
            });

            simulation.Children.Add(new Weather()
            {
                Name = "Weather",
                FileName = treatment.Experiment.MetStation?.Name + ".met"
            });

            simulation.Children.Add(new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            });

            simulation.Children.Add(new SoilArbitrator() { Name = "SoilArbitrator" });

            simulation.Children.Add(GetField(treatment, dbContext));

            return simulation;
        }

        private static string GetTreatmentMemo(Treatment treatment, REMSContext context)
        {
            var designs = from design in context.Designs
                          where design.Treatment == treatment
                          select design;

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("This treatment uses the following factor/level combinations:\n");
            foreach (var design in designs)
            {
                var factor = "\tFactor: " + design.Level.Factor.Name;
                var level = "Level: " + design.Level.Name;                

                builder.AppendLine($"{factor, -30}{level, -30}");
            }
            
            return builder.ToString();
        }

        private static Zone GetField(Treatment treatment, REMSContext dbContext)
        {
            var field = treatment.Experiment.Field;

            var zone = new Zone()
            {
                Name = field.Name,
                Slope = (double)field.Slope,
                Area = 1
            };

            zone.Children.Add(GetManagers());
            zone.Children.Add(new Models.Irrigation() { Name = "Irrigation" });
            zone.Children.Add(new Fertiliser() { Name = "Fertiliser" });
            zone.Children.Add(GetOperations(treatment, dbContext));

            //zone.Children.Add(GetSoil(field, dbContext));

            // TEMPORARY FOR DEMO
            if (field.Soil.Type == "BW5") zone.Children.Add(GetDemoBW5());
            else zone.Children.Add(GetDemoBW8());

            zone.Children.Add(new Plant() { Name = "Sorghum" });

            var daily = new Report()
            {
                Name = "DailyReport",
                VariableNames = new string[] { "GrainTempFactor" },
                EventNames = new string[] { "[Clock].DoReport" }
            };

            var harvest = new Report()
            {
                Name = "HarvestReport",
                EventNames = new string[] { "[Sorghum].Harvesting" }
            };

            zone.Children.Add(daily);
            zone.Children.Add(harvest);

            return zone;
        }

        private static Folder GetManagers()
        {
            var folder = new Folder() { Name = "Manager folder" };            

            var skiprow = new Manager()
            {
                Name = "Sow SkipRow on a fixed date",
                Code = GetScript("SkipRow.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Date", "1997-01-09"),
                    new KeyValuePair<string, string>("Density", "10"),
                    new KeyValuePair<string, string>("Depth", "30"),
                    new KeyValuePair<string, string>("Cultivar", "QL41xQL36"),
                    new KeyValuePair<string, string>("RowSpacing", "500"),
                    new KeyValuePair<string, string>("RowConfiguration", "solid"),
                    new KeyValuePair<string, string>("Ftn", "0")
                }
            };            
            folder.Children.Add(skiprow);

            var harvest = new Manager()
            {
                Name = "Harvesting rule",
                Code = GetScript("Harvest.cs.txt")
            };
            folder.Children.Add(harvest);

            return folder;
        }

        private static Operations GetOperations(Treatment treatment, REMSContext dbContext)
        {
            var model = new Operations()
            {
                Operation = new List<Operation>()
            };            

            var iquery = dbContext.Query.IrrigationsByTreatment(treatment);
            var irrigations = iquery
                .Select(i => new Operation()
                {
                    Action = $"[Irrigation].Apply({i.Amount})",
                    Date = ((DateTime)i.Date).ToString()
                }
                );

            var fquery = dbContext.Query.FertilizationsByTreatment(treatment);
            var fertilizations = fquery
                .Select(f => new Operation()
                {
                    Action = $"[Fertiliser].Apply({f.Amount}, Fertiliser.Types.NO3N, {f.Depth})",
                    Date = ((DateTime)f.Date).ToString()
                }
                );

            model.Operation?.AddRange(irrigations);
            model.Operation?.AddRange(fertilizations);

            return model;
        }

        private static Models.Soils.Soil GetSoil(Field field, REMSContext dbContext)
        {
            var model = new Models.Soils.Soil()
            {
                Name = "Soil",
                Latitude = (double)field.Latitude,
                Longitude = (double)field.Longitude
            };

            model.Children.Add(GetWater(field.SoilId, dbContext));
            model.Children.Add(GetSoilWater(field.SoilId, dbContext));
            model.Children.Add(GetSoilNitrogen());
            model.Children.Add(GetSoilOrganicMatter(field.SoilId, dbContext));
            model.Children.Add(GetChemicalAnalysis(field.SoilId, dbContext));
            model.Children.Add(GetSample(field.SoilId, "Initial Water"));
            model.Children.Add(GetSample(field.SoilId, "Initial Nitrogen"));
            model.Children.Add(new CERESSoilTemperature() { Name = "ExampleSoilTemperature" });

            return model;
        }
        
        private static Models.Soils.Soil GetDemoBW5()
        {
            var soil = new Models.Soils.Soil()
            {
                Name = "Soil",
                SoilType = "BW5",
                Site = "ICRISAT",
                Region = "Hyderebad",
                LocalName = "ICRISAT",
                Latitude = 18.0,
                Longitude = 76.0
            };

            var phys = new Physical
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-150", "150-165", "165-180", "180-195"},
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0 },
                BD = new[] { 1.32, 1.49, 1.54, 1.54, 1.49, 1.47, 1.38, 1.37, 1.33, 1.35, 1.35, 1.33, 1.33 },
                AirDry = new[] { 0.1, 0.21, 0.23, 0.23, 0.22, 0.22, 0.22, 0.21, 0.22, 0.22, 0.22, 0.22, 0.22 },
                LL15 = new[] { 0.21, 0.22, 0.24, 0.24, 0.23, 0.23, 0.23, 0.22, 0.22, 0.22, 0.22, 0.22, 0.22 },
                DUL = new[] { 0.42, 0.39, 0.36, 0.36, 0.39, 0.42, 0.43, 0.43, 0.45, 0.43, 0.43, 0.45, 0.45 },
                SAT = new[] { 0.44, 0.41, 0.38, 0.38, 0.41, 0.44, 0.45, 0.45, 0.47, 0.45, 0.45, 0.47, 0.47}
            };

            var soilCrop = new SoilCrop() 
            { 
                Name = "SorghumSoil",
                LL = new[] { 0.21, 0.22, 0.24, 0.24, 0.23, 0.23, 0.23, 0.22, 0.22, 0.22, 0.22, 0.22, 0.22 },
                KL = new[] { 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.07, 0.04, 0.04, 0.04, 0.04},
                XF = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 }
            };            

            phys.Children.Add(soilCrop);

            var sw = new SoilWater()
            {
                SummerDate = "1-Nov",
                SummerU = 6.0,
                SummerCona = 3.5,
                WinterDate = "1-Apr",
                WinterU = 6.0,
                WinterCona = 3.5,
                DiffusConst = 40.0,
                DiffusSlope = 16.0,
                Salb = 0.11,
                CN2Bare = 85.0,
                CNRed = 0.8,
                CNCov = 0.2,
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0 },
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-150", "150-165", "165-180", "180-195" },
                SWCON = new[] { 0.16, 0.11, 0.14, 0.14, 0.11, 0.11, 0.1, 0.1, 0.1, 0.12, 0.12, 0.1, 0.1 }
            };
            soil.Children.Add(phys);
            soil.Children.Add(sw);

            soil.Children.Add(GetSoilNitrogen());

            var organic = new Organic()
            {
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0 },
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-150", "150-165", "165-180", "180-195" },
                Carbon = new[] { 0.45, 0.32, 0.29, 0.23, 0.27, 0.3, 0.23, 0.22, 0.19, 0.12, 0.12, 0.14, 0.14},
                SoilCNRatio = new[] { 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5},
                FBiom = new[] { 0.05, 0.02, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01},
                FInert = new[] { 0.39, 0.47, 0.52, 0.62, 0.74, 0.83, 0.93, 0.93, 0.93, 0.93, 0.93, 0.93, 0.93},
                FOM = new[] { 216.87490805674403, 172.18190341843405, 136.69911439011696, 108.5285242179618, 86.163254396184115, 68.406959936462783, 54.309835446002637, 43.117808902945995, 34.232205443513422, 27.177723528684304, 21.577010497334516, 17.130477521809652, 13.600274243805792}
            };
            soil.Children.Add(organic);

            var chem = new Chemical()
            {
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0 },
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-150", "150-165", "165-180", "180-195" },
                NO3N = new[] { 6.1, 5.6, 4.0, 3.3, 3.2, 3.7, 5.0, 3.0, 3.0, 2.0, 2.0, 2.0, 2.0 },
                NH4N = new[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 },
                PH = new[] { 8.35, 8.52, 8.8, 8.95, 9.0, 9.0, 9.0, 9.0, 8.92, 8.97, 8.97, 8.82, 8.82 }
            };
            soil.Children.Add(chem);

            var sample = new Sample()
            {
                Name = "Initial",
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0 },
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-150", "150-165", "165-180", "180-195" },
                SW = new[] { 0.281, 0.338, 0.353, 0.36, 0.39, 0.407, 0.43, 0.43, 0.45, 0.43, 0.43, 0.45, 0.45 }
            };
            soil.Children.Add(sample);

            var ceres = new CERESSoilTemperature();
            soil.Children.Add(ceres);

            return soil;
        }

        private static Models.Soils.Soil GetDemoBW8()
        {
            var soil = new Models.Soils.Soil()
            {
                Name = "Soil",
                SoilType = "BW8",
                Site = "ICRISAT",
                Region = "Hyderebad",
                Latitude = 18.0,
                Longitude = 76.0
            };

            var phys = new Physical
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                BD = new[] { 1.49,
1.41,
1.41,
1.56,
1.49,
1.52,
1.61,
1.68,
1.63,
1.62,
1.62 },
                AirDry = new[] { 0.12,
0.15,
0.15,
0.1,
0.11,
0.13,
0.09,
0.08,
0.09,
0.09,
0.09 },
                LL15 = new[] { 0.16,
0.19,
0.19,
0.14,
0.15,
0.17,
0.13,
0.12,
0.13,
0.13,
0.13 },
                DUL = new[] { 0.32,
0.35,
0.35,
0.28,
0.31,
0.33,
0.26,
0.22,
0.24,
0.24,
0.24 },
                SAT = new[] { 0.43,
0.46,
0.46,
0.41,
0.43,
0.42,
0.39,
0.36,
0.38,
0.38,
0.38 }
            };

            var soilCrop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = new[] { 0.16,
    0.19,
    0.19,
    0.14,
    0.15,
    0.17,
    0.13,
    0.12,
    0.13,
    0.13,
    0.13 },
                KL = new[] { 0.07,
    0.07,
    0.07,
    0.07,
    0.07,
    0.07,
    0.07,
    0.07,
    0.07,
    0.04,
    0.04 },
                XF = new[] { 1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0,
    1.0 }
            };

            phys.Children.Add(soilCrop);

            var sw = new SoilWater()
            {
                SummerDate = "1-Nov",
                SummerU = 6.0,
                SummerCona = 3.5,
                WinterDate = "1-Apr",
                WinterU = 6.0,
                WinterCona = 3.5,
                DiffusConst = 40.0,
                DiffusSlope = 16.0,
                Salb = 0.11,
                CN2Bare = 85.0,
                CNRed = 0.8,
                CNCov = 0.2,
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                SWCON = new[] { 0.16,
0.11,
0.14,
0.14,
0.11,
0.11,
0.1,
0.1,
0.1,
0.12,
0.1 }
            };
            soil.Children.Add(phys);
            soil.Children.Add(sw);

            soil.Children.Add(GetSoilNitrogen());

            var organic = new Organic()
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                Carbon = new[] { 0.49,
0.45,
0.28,
0.18,
0.14,
0.11,
0.09,
0.07,
0.06,
0.05,
0.03 },
                SoilCNRatio = new[] { 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5 },
                FBiom = new[] { 0.05, 0.02, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 },
                FInert = new[] { 0.39, 0.47, 0.52, 0.62, 0.74, 0.83, 0.93, 0.93, 0.93, 0.93, 0.93 },
                FOM = new[] { 226.92975737066317,
180.16467610969974,
143.03681850192319,
113.56017111087074,
90.157992870603834,
71.5784733233617,
56.827771783434649,
45.116855612176408,
35.819293920007865,
22.577373292364666,
14.230816104894044 }
            };
            soil.Children.Add(organic);

            var chem = new Chemical()
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                NO3N = new[] { 9.0,
11.0,
8.0,
6.0,
4.0,
3.0,
3.0,
3.0,
6.0,
4.0,
5.0 },
                NH4N = new[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 },
                PH = new[] { 8.35, 8.52, 8.8, 8.95, 9.0, 9.0, 9.0, 9.0, 8.92, 8.97, 8.82 }
            };
            soil.Children.Add(chem);

            var initial = new InitialWater()
            {
                PercentMethod = 0,
                FractionFull = 0.5,
                Name = "Initial Water"
            };
            soil.Children.Add(initial);

            var sample = new Sample()
            {
                Name = "Initial",
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 }
            };
            soil.Children.Add(sample);

            var ceres = new CERESSoilTemperature();
            soil.Children.Add(ceres);

            return soil;
        }

        private static Physical GetWater(int soilId, REMSContext dbContext)
        {
            var model = new Physical()
            {
                Name = "Physical",
                Thickness = dbContext.Query.SoilLayerThicknessBySoil(soilId).ToArray(),
                BD = dbContext.Query.SoilLayerDataByTrait("BD", soilId).ToArray(),
                AirDry = dbContext.Query.SoilLayerDataByTrait("AirDry", soilId).ToArray(),
                LL15 = dbContext.Query.SoilLayerDataByTrait("LL15", soilId).ToArray(),
                DUL = dbContext.Query.SoilLayerDataByTrait("DUL", soilId).ToArray(),
                SAT = dbContext.Query.SoilLayerDataByTrait("SAT", soilId).ToArray(),
                KS = dbContext.Query.SoilLayerDataByTrait("KS", soilId).ToArray()
            };

            model.Children.Add(GetSoilCrop(soilId, dbContext));

            return model;
        }

        private static SoilCrop GetSoilCrop(int soilId, REMSContext dbContext)
        {
            return new SoilCrop()
            {
                Name = "SoilCrop",
                LL = dbContext.Query.SoilLayerDataByTrait("LL", soilId).ToArray(),
                KL = dbContext.Query.SoilLayerDataByTrait("KL", soilId).ToArray(),
                XF = dbContext.Query.SoilLayerDataByTrait("XF", soilId).ToArray()
            };
        }

        private static SoilWater GetSoilWater(int soilId, REMSContext dbContext)
        {
            return new SoilWater()
            {
                Name = "SoilWater",
                Thickness = dbContext.Query.SoilLayerThicknessBySoil(soilId).ToArray(),
                SWCON = dbContext.Query.SoilLayerDataByTrait("SWCON", soilId).ToArray(),
                KLAT = dbContext.Query.SoilLayerDataByTrait("KLAT", soilId).ToArray()
            };
            // TODO: Initiliase single parameters           
        }

        private static SoilNitrogen GetSoilNitrogen()
        {
            var model = new SoilNitrogen() { Name = "SoilNitrogen" };

            model.Children.Add(new SoilNitrogenNH4() { Name = "NH4" });
            model.Children.Add(new SoilNitrogenNO3() { Name = "NO3" });
            model.Children.Add(new SoilNitrogenUrea() { Name = "Urea" });
            model.Children.Add(new SoilNitrogenPlantAvailableNH4() { Name = "PlantAvailableNH4" });
            model.Children.Add(new SoilNitrogenPlantAvailableNO3() { Name = "PlantAvailableNO3" });

            return model;
        }

        private static Organic GetSoilOrganicMatter(int soilId, REMSContext dbContext)
        {
            return new Organic()
            {
                Name = "Organic",
                Thickness = dbContext.Query.SoilLayerThicknessBySoil(soilId).ToArray(),
                Carbon = dbContext.Query.SoilLayerDataByTrait("Carbon", soilId).ToArray(),
                SoilCNRatio = dbContext.Query.SoilLayerDataByTrait("SoilCNRatio", soilId).ToArray(),
                FBiom = dbContext.Query.SoilLayerDataByTrait("FBiom", soilId).ToArray(),
                FInert = dbContext.Query.SoilLayerDataByTrait("FInert", soilId).ToArray(),
                FOM = dbContext.Query.SoilLayerDataByTrait("FOM", soilId).ToArray()
            };
        }

        private static Chemical GetChemicalAnalysis(int soilId, REMSContext dbContext)
        {
            var chem = new Chemical()
            {
                Name = "Chemical",
                PH = dbContext.Query.SoilLayerDataByTrait("PH", soilId).ToArray()
            };


            //var chem = new Chemical()
            //{
            //    Name = "Chemical",
            //    Depth = new[] { "0-15", "15-30", "30-45", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" },
            //    Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 },
            //    NO3N = new[] { 10.4, 1.6329999999999996, 1.2330000000000008, 0.9, 1.1, 1.4670000000000005, 3.6329999999999996, 5.6670000000000007, 5.8, 7.267000000000003 },
            //    NH4N = new[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 },
            //    PH = new[] { 6.3, 6.4, 6.5, 6.6, 6.6, 6.5, 6.5, 6.5, 6.5, 6.5 }
            //};

            return chem;
        }

        private static Sample GetSample(int soilId, string name)
        {
            return new Sample()
            {
                Name = name
            };
        }

        private static GraphPanel NewPanel(Dictionary<string, IEnumerable<string>> variables, string scriptFile)
        {
            var panel = new GraphPanel()
            {
                SameAxes = false,
                NumCols = 3,
                Name = "PredictedObserved"                
            };

            var manager = new Manager()
            {
                Name = "Config",
                Code = GetScript(scriptFile)
            };

            panel.Children.Add(manager);

            var axes = new List<Axis>()
            {
                new Axis()
                {
                    Type = Axis.AxisType.Bottom,
                    Inverted = false,
                    DateTimeAxis = false,
                    CrossesAtZero = false
                },
                new Axis()
                {
                    Type = Axis.AxisType.Left,
                    Inverted = false,
                    DateTimeAxis = false,
                    CrossesAtZero = false
                }
            };
            foreach (var variable in variables)
            {
                var graph = NewGraph(variable.Key, axes);
                foreach(var value in variable.Value)
                {
                    AddPredictedObservedPair(graph, value);
                }
                panel.Children.Add(graph);
            }

            return panel;
        }

        private static void AddPredictedObservedPair(Graph graph, string variable)
        {
            var predicted = new Series()
            {
                Type = SeriesType.Scatter,
                XAxis = Axis.AxisType.Bottom,
                YAxis = Axis.AxisType.Left,
                ColourArgb = 0,
                Marker = MarkerType.None,
                MarkerSize = 0,
                Line = 0,
                LineThickness = LineThicknessType.Normal,
                Checkpoint = "Current",
                TableName = "DailyPredictedObserved",
                XFieldName = $"Date",
                YFieldName = $"Predicted.{variable}",
                ShowInLegend = true,
                IncludeSeriesNameInLegend = false,
                Cumulative = false,
                CumulativeX = false,
                Name = $"Predicted {variable}"
            };

            var observed = new Series()
            {
                Type = SeriesType.Scatter,
                XAxis = Axis.AxisType.Bottom,
                YAxis = Axis.AxisType.Left,
                ColourArgb = 0,
                Marker = 0,
                MarkerSize = 0,
                Line = LineType.None,
                LineThickness = LineThicknessType.Normal,
                Checkpoint = "Current",
                TableName = "DailyPredictedObserved",
                XFieldName = $"Date",
                YFieldName = $"Observed.{variable}",
                ShowInLegend = true,
                IncludeSeriesNameInLegend = false,
                Cumulative = false,
                CumulativeX = false,
                Name = $"Predicted {variable}"
            };

            graph.Children.Add(predicted);
            graph.Children.Add(observed);
        }

        public static void JBTest(Simulations sims)
        {
            Simulation test = new Simulation();
            var simProps = new SimulationProperties()
            {
                SimulationName = "Sorghum_HE1_T1",
                StartDate = new DateTime(1996, 12, 1),
                EndDate = new DateTime(1997, 06, 11),
                MetFile = "HE1.met",

                ReportFile = "Sorghum_HE1_T1",
                ReportVariables = new List<string>()
                {
                    "today format dd/mm/yyyy as Date",
                    "daysAfterSowing as Das",
                    "Stage",
                    "Biomass",
                    "Biomass units g/m^2 as BiomassWt"
                },
                SowingDate = new DateTime(1997, 1, 9),
                SowingDensity = 10,
                SowingDepth = 30,
                Crop = "Sorghum",
                CropCultivar = "QL41xQL36",
                RowSpacing = 500,
                RowConfiguration = "solid",
                FTN = 0
            };


            test.Name = simProps.SimulationName;
            test.Children.Add(new Clock() { StartDate = simProps.StartDate, EndDate = simProps.EndDate });
            sims.Children.Add(test);

            test.Children.Add(new Summary());
            test.Children.Add(new Weather() { FileName = Path.Combine("Met", simProps.MetFile) });
            test.Children.Add(new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 100.0
            });
            test.Children.Add(new SoilArbitrator());

            var paddock = new Zone() { Name = "Paddock", Area = 1.0 };
            var report = new Report() { EventNames = new[] { "[Clock].DoReport" } };
            report.VariableNames = new[] { "[Clock].Today", "[Sorghum].Phenology.CurrentStageName", "[Sorghum].AbioveGround.Wt" };
            paddock.Children.Add(report);

            var ops = new Operations();
            ops.Operation = new List<Operation>();
            ops.Operation.Add(new Operation() { Date = "1997-01-09", Action = "[Irrigation].Apply(52);" });
            ops.Operation.Add(new Operation() { Date = "1997-01-16", Action = "[Irrigation].Apply(36);" });
            ops.Operation.Add(new Operation() { Date = "1997-01-21", Action = "[Irrigation].Apply(18);" });
            ops.Operation.Add(new Operation() { Date = "1997-01-29", Action = "[Irrigation].Apply(20);" });
            ops.Operation.Add(new Operation() { Date = "1997-02-07", Action = "[Irrigation].Apply(14);" });
            ops.Operation.Add(new Operation() { Date = "1997-03-13", Action = "[Irrigation].Apply(30);" });
            ops.Operation.Add(new Operation() { Date = "1997-03-21", Action = "[Irrigation].Apply(45);" });
            ops.Operation.Add(new Operation() { Date = "1997-04-10", Action = "[Irrigation].Apply(27);" });
            ops.Operation.Add(new Operation() { Date = "1997-04-17", Action = "[Irrigation].Apply(42);" });
            ops.Operation.Add(new Operation() { Date = "1997-05-02", Action = "[Irrigation].Apply(27);" });
            ops.Operation.Add(new Operation() { Date = "1996-12-20", Action = "[Fertiliser].Apply(33, Fertiliser.Types.NO3N, 50);" });
            ops.Operation.Add(new Operation() { Date = "1996-12-23", Action = "[Fertiliser].Apply(0, Fertiliser.Types.NO3N, 50);" });
            ops.Operation.Add(new Operation() { Date = "1996-12-23", Action = "[Fertiliser].Apply(119, Fertiliser.Types.NO3N, 50);" });
            ops.Operation.Add(new Operation() { Date = "1997-02-12", Action = "[Fertiliser].Apply(59, Fertiliser.Types.NO3N, 50);" });
            ops.Operation.Add(new Operation() { Date = "1997-03-21", Action = "[Fertiliser].Apply(59, Fertiliser.Types.NO3N, 50);" });
            paddock.Children.Add(ops);

            paddock.Children.Add(new Models.Irrigation());
            paddock.Children.Add(new Fertiliser());
            paddock.Children.Add(new MicroClimate()
            {
                b_interception = 1.0,
                soil_albedo = 0.3,
                SoilHeatFluxFraction = 0.4,
                NightInterceptionFraction = 0.5,
                ReferenceHeight = 2.0
            });

            var mgr = new Manager();
            mgr.Name = "Sow SkipRow on a fixed date";
            mgr.Code = "using System;\nusing Models.Core;\nusing Models.PMF;\n\nusing APSIM.Shared.Utilities;\n\nnamespace Models\n{\n\t[Serializable]\n\tpublic class Script : Model\n\t{\n\t\t[Description(\"Enter sowing date (dd/mm/yyyy) : \")]\n\t\tpublic DateTime Date { get; set; }\n\n\t\t[Description(\"Enter sowing density  (plants/m2) : \")]\n\t\tpublic double Density { get; set; }\n\n\t\t[Description(\"Enter sowing depth  (mm) : \")]\n\t\tpublic double Depth { get; set; }\n\n\t\t[Description(\"Enter cultivar : \")]\n\t\t[Display(Type = DisplayType.CultivarName)]\n\t\tpublic string Cultivar { get; set; } // QL41xQL36\n\n\t\t[Description(\"Enter row spacing (m) : \")]\n\t\tpublic double RowSpacing { get; set; }\n\n\t\t[Description(\"Enter skip row configuration : \")]\n\t\tpublic RowConfigurationType RowConfiguration { get; set; }\n\n\t\t[Description(\"Enter Fertile Tiller No. : \")]\n\t\tpublic double Ftn { get; set; }\n\n\t\tpublic enum RowConfigurationType \n\t\t{\n\t\t\tsolid, single, _double /*replaces double*/\n\t\t}\n\n\t\t[Link]\n\t\tprivate Zone paddock;\n\n\t\t[Link]\n\t\tprivate Clock clock;\n\n\t\t[Link]\n\t\tprivate IPlant crop;\n\n\t\t[EventSubscribe(\"DoManagement\")]\n\t\tprivate void OnDoManagement(object sender, EventArgs e)\n\t\t{\n\t\t\tif (clock.Today == Date /* && isFallow */)\n\t\t\t{\n\t\t\t\tdouble population = Density * paddock.Area;\n\t\t\t\tcrop.Sow(Cultivar, population, Depth, RowSpacing, budNumber: Ftn, rowConfig: (double)RowConfiguration + 1);\n\t\t\t\t/*\n            \tif (paddock_is_fallow() = 1 and today = date('[date]')) then\n              \t\t[crop] sow plants =[density], sowing_depth = [depth], cultivar = [cultivar], row_spacing = [row_spacing], skip = [RowConfiguration], tiller_no_fertile = [ftn] ()\n            \tendif\n\t\t\t\t*/\n\t\t\t}\n\t\t}\n\t}\n}\n";

            mgr.Parameters = new List<KeyValuePair<string, string>>();
            mgr.Parameters.Add(new KeyValuePair<string, string>("Date", simProps.SowingDate.ToString("yyyy-MM-dd")));
            mgr.Parameters.Add(new KeyValuePair<string, string>("Density", simProps.SowingDensity.ToString()));
            mgr.Parameters.Add(new KeyValuePair<string, string>("Depth", simProps.SowingDepth.ToString()));
            mgr.Parameters.Add(new KeyValuePair<string, string>("Cultivar", simProps.CropCultivar));
            mgr.Parameters.Add(new KeyValuePair<string, string>("RowSpacing", simProps.RowSpacing.ToString()));
            mgr.Parameters.Add(new KeyValuePair<string, string>("RowConfiguration", simProps.RowConfiguration));
            mgr.Parameters.Add(new KeyValuePair<string, string>("Ftn", simProps.FTN.ToString()));
            paddock.Children.Add(mgr);

            mgr = new Manager();
            mgr.Name = "Harvesting rule";
            mgr.Code = "using System;\nusing Models.Core;\nusing Models.PMF;\n\nnamespace Models\n{\n\t[Serializable]\n\n\tpublic class Script : Model\n\t{\n\t\t[Link]\n\t\tprivate Clock clock;\n\n\t\t[Link]\n\t\tprivate IPlant crop;\n\n\t\t[EventSubscribe(\"DoManagement\")]\n\t\tprivate void OnDoCalculations(object sender, EventArgs e)\n\t\t{\n\t\t\tif (crop.IsReadyForHarvesting)\n\t\t\t{\n\t\t\t\tcrop.Harvest();\n\t\t\t\tcrop.EndCrop();\n\t\t\t}\n\t\t\t/*\n\t\t\t\t\n\n           if [crop].StageName = 'harvest_ripe' or [crop].plant_status = 'dead' then\n              [crop]  harvest\n              [crop]  end_crop\n           endif\n\n            \n\t\t\t*/\n\t\t}\n\t}\n}\n";
            paddock.Children.Add(mgr);

            var soil = new Models.Soils.Soil()
            {
                Name = "HRS",
                SoilType = "HMM",
                Site = "HRS",
                Region = "SE Queensland",

            };
            var phys = new Physical();
            phys.Depth = new[] { "0-10", "10-20", "20-40", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" };
            phys.Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 };
            phys.BD = new[] { 1.34, 1.34, 1.33, 1.38, 1.4, 1.55, 1.59, 1.63, 1.66, 1.68 };
            phys.AirDry = new[] { 0.08, 0.19, 0.23, 0.26, 0.26, 0.28, 0.25, 0.28, 0.3, 0.31 };
            phys.LL15 = new[] { 0.226, 0.226, 0.258, 0.27, 0.268, 0.304, 0.335, 0.33, 0.32, 0.33 };
            phys.DUL = new[] { 0.42, 0.42, 0.46, 0.46, 0.43, 0.4, 0.37, 0.33, 0.32, 0.33 };
            phys.SAT = new[] { 0.45, 0.45, 0.48, 0.47, 0.45, 0.41, 0.38, 0.37, 0.37, 0.36 };

            var soilCrop = new SoilCrop() { Name = "SorghumSoil" };
            soilCrop.LL = new[] { 0.226, 0.226, 0.258, 0.27, 0.268, 0.304, 0.335, 0.33, 0.32, 0.33 };
            soilCrop.KL = new[] { 0.07, 0.07, 0.07, 0.07, 0.06, 0.06, 0.06, 0.05, 0.05, 0.04 };
            soilCrop.XF = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };
            phys.Children.Add(soilCrop);

            var sw = new SoilWater()
            {
                SummerDate = "1-Nov",
                SummerU = 1.5,
                SummerCona = 6.5,
                WinterDate = "1-Apr",
                WinterU = 1.5,
                WinterCona = 6.5,
                DiffusConst = 40.0,
                DiffusSlope = 16.0,
                Salb = 0.2,
                CN2Bare = 85.0,
                CNRed = 20.0,
                CNCov = 0.8,
                Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 },
                Depth = new[] { "0-10", "10-20", "20-40", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" },
                SWCON = new[] { 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3, 0.3 }
            };
            soil.Children.Add(phys);
            soil.Children.Add(sw);
            var sn = new SoilNitrogen(); //these names are used to find these nodes
            sn.Children.Add(new SoilNitrogenNO3() { Name = "NO3" });
            sn.Children.Add(new SoilNitrogenNH4() { Name = "NH4" });
            sn.Children.Add(new SoilNitrogenUrea() { Name = "Urea" });
            sn.Children.Add(new SoilNitrogenPlantAvailableNO3() { Name = "PlantAvailableNO3" });
            sn.Children.Add(new SoilNitrogenPlantAvailableNH4() { Name = "PlantAvailableNH4" });
            soil.Children.Add(sn);

            var organic = new Organic()
            {
                Depth = new[] { "0-10", "10-20", "20-40", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" },
                Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 },
                Carbon = new[] { 1.19, 0.59, 0.45, 0.3, 0.2, 0.16, 0.17, 0.17, 0.17, 0.17 },
                SoilCNRatio = new[] { 12.5, 12.5, 12.5, 12.5, 12.5, 12.5, 12.5, 12.5, 12.5, 12.5 },
                FBiom = new[] { 0.05, 0.02, 0.01, 0.01, 0.01, 0.01, 0.01, 0.05, 0.02, 0.01 },
                FInert = new[] { 0.45, 0.6, 0.75, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9, 0.9 },
                FOM = new[] { 260.58740315916066, 220.5824745109322, 158.05424955092769, 113.25081857248298, 81.147757455295647, 58.144908999566972, 41.662647848653442, 29.852591664969907, 21.390316629725067, 15.32683160828522 }
            };
            soil.Children.Add(organic);

            var chem = new Chemical()
            {
                Depth = new[] { "0-10", "10-20", "20-40", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" },
                Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 },
                NO3N = new[] { 10.4, 1.6329999999999996, 1.2330000000000008, 0.9, 1.1, 1.4670000000000005, 3.6329999999999996, 5.6670000000000007, 5.8, 7.267000000000003 },
                NH4N = new[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 },
                PH = new[] { 6.3, 6.4, 6.5, 6.6, 6.6, 6.5, 6.5, 6.5, 6.5, 6.5 }
            };
            soil.Children.Add(chem);

            var sample = new Sample()
            {
                Name = "Initial",
                Depth = new[] { "0-10", "10-20", "20-40", "40-60", "60-80", "80-100", "100-120", "120-140", "140-160", "160-180" },
                Thickness = new[] { 100.0, 100.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0, 200.0 },
                SW = new[] { 0.235, 0.252, 0.27, 0.3, 0.268, 0.304, 0.335, 0.33, 0.32, 0.33 }
            };
            soil.Children.Add(sample);

            var ceres = new CERESSoilTemperature();
            soil.Children.Add(ceres);

            paddock.Children.Add(soil);
            paddock.Children.Add(new Plant() { Name = "Sorghum", ResourceName = "Sorghum", CropType = "Sorghum" });

            test.Children.Add(paddock);
            sims.Children.Add(new DataStore());
        }
    }
}
