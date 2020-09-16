using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using MediatR;
using Models;
using Models.Climate;
using Models.Core;
using Models.Core.ApsimFile;
using Models.PMF;
using Models.PostSimulationTools;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Storage;
using Models.Surface;
using Models.WaterModel;

using Rems.Application;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Rems.Infrastructure.Met;

namespace Rems.Infrastructure.ApsimX
{
    public enum NextNode
    {
        Parent,
        Sibling,
        Child,
        None
    }

    public interface IApsimX
    {
        Simulations Simulations { get; }

        IMediator Mediator { get; }
    }

    public static partial class ApsimXExtensions
    {
        public static void SaveFile(this IApsimX apsim, string filename)
        {
            apsim.Simulations.FileName = filename;
            //JsonTools.SaveJson(filename, apsim.Simulations);
            //Calling apsim.Simulations.Write causes the storage to run which looks for sqlite.dll
            File.WriteAllText(filename, FileFormat.WriteToString(apsim.Simulations));
        }

        public static IModel Add<M, R>(this IModel model, NextNode next, IApsimX apsim, params object[] args)
            where M : IModel
            where R : IRequest<M>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(args);

            var task = apsim.Mediator.Send(request);
            task.Wait();

            return model.Add(task.Result, next);
        }
        
        public static IModel Add<M>(this IModel model, NextNode next, string name = null) 
            where M : IModel, new()
        {
            var child = new M();
            if (!(name is null)) child.Name = name;
            return model.Add(child, next);
        }

        public static IModel Add(this IModel model, IModel child, NextNode next)
        {
            model.Children.Add(child);
            child.Parent = model;

            if (next == NextNode.Parent)
                return model.Parent;

            if (next == NextNode.Child)
                return child;

            if (next == NextNode.Sibling)
                return model;

            // if (next == NextNode.None)
            return null;
        }

        public async static Task<Simulations> CreateModels(this IApsimX apsim)
        {
            

            var dVars = DataFiles.ReadRawText("Daily.txt").Split('\n');
            var dEvents = new string[] { "[Clock].DoReport" };

            var hVars = DataFiles.ReadRawText("Harvest.txt").Split('\n');
            var hEvents = new string[] { "[Sorghum].Harvesting" };

            var exps = await apsim.Mediator.Send(new ExperimentsQuery());

            var args = new ProgressTrackingArgs()
            {
                Items = exps.Length,
                Title = "Exporting..."
            };
            EventManager.InvokeProgressTracking(null, args);

            apsim.Simulations
                .Add<DataStore>(NextNode.Child)
                    .AddExcelInput(NextNode.Sibling, "Observed")
                    .AddExcelInput(NextNode.Sibling, "DailyObserved")
                    .AddPredictedObserved(NextNode.Sibling, "Harvest")
                    .AddPredictedObserved(NextNode.Parent, "Daily")
                .Add<Replacements>(NextNode.Child, "Replacements")
                    .AddPlant(NextNode.Sibling)
                    .AddReport(NextNode.Sibling, "Daily", dVars, dEvents)
                    .AddReport(NextNode.Parent, "Harvest", hVars, hEvents)
                .AddValidations(NextNode.None, apsim, exps);

            EventManager.InvokeStopProgress(null, EventArgs.Empty);

            return apsim.Simulations;
        }

        public static IModel AddExcelInput(this IModel model, NextNode next, string name)
        {
            var input = new ExcelInput()
            {
                Name = name,
                FileNames = new string[] { name + ".xlsx" },
                SheetNames = new string[] { name }
            };

            return model.Add(input, next);
        }

        public static IModel AddPredictedObserved(this IModel model, NextNode next, string name)
        {
            var observed = new PredictedObserved()
            {
                PredictedTableName = name + "Report",
                ObservedTableName = name + "Observed",
                FieldNameUsedForMatch = "SimulationID",
                Name = name + "PredictedObserved"
            };

            return model.Add(observed, next);
        }

        public static IModel AddPlant(this IModel model, NextNode next)
        {
            var sorghumModel = Path.Combine("DataFiles", "apsimx", "Sorghum.json");
            var sorghum = JsonTools.LoadJson<Plant>(sorghumModel);

            return model.Add(sorghum, next);
        }

        #region Validations

        public static IModel AddValidations(this IModel model, NextNode next, IApsimX apsim, KeyValuePair<int, string>[] exps)
        {
            var validations = new Folder() { Name = "Validations" };

            foreach (var exp in exps)
                validations.AddExperiment(NextNode.None, apsim, exp.Key, exp.Value);

            validations.AddCombinedResults(NextNode.Sibling)
                .AddPanel(NextNode.None);

            return model.Add(validations, next);
        }

        public static IModel AddCombinedResults(this IModel model, NextNode next)
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
                    FactorToVaryMarkers = "SimulationName",
                    Marker = 0,
                    MarkerSize = 0,
                    Line = LineType.None,
                    LineThickness = LineThicknessType.Normal,
                    TableName = "PredictedObserved",
                    XFieldName = $"Observed.{variable}",
                    YFieldName = $"Predicted.{variable}",
                    ShowInLegend = true,
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

                var graph = BuildGraph(variable, axes);
                graph.Children.Add(series);
                folder.Children.Add(graph);
            }

            return model.Add(folder, next);
        }

        public static IModel AddPanel(this IModel model, NextNode next)
        {
            var variables = new Dictionary<string, IEnumerable<string>>()
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

            var panel = new GraphPanel()
            {
                NumCols = 3,
                Name = "PredictedObserved"
            };

            var manager = new Manager()
            {
                Name = "Config",
                Code = DataFiles.ReadRawText("PredictedObserved.cs.txt")
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
                var graph = BuildGraph(variable.Key, axes);
                foreach (var value in variable.Value)
                {
                    AddPredictedObservedPair(graph, value);
                }
                panel.Children.Add(graph);
            }

            void AddPredictedObservedPair(Graph graph, string variable)
            {
                var predicted = new Series()
                {
                    Type = SeriesType.Scatter,
                    XAxis = Axis.AxisType.Bottom,
                    YAxis = Axis.AxisType.Left,
                    Marker = MarkerType.None,
                    MarkerSize = 0,
                    Line = 0,
                    LineThickness = LineThicknessType.Normal,
                    FactorToVaryColours = "Graph series",
                    TableName = "DailyPredictedObserved",
                    XFieldName = $"Predicted.Date",
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
                    Marker = 0,
                    MarkerSize = 0,
                    Line = LineType.None,
                    LineThickness = LineThicknessType.Normal,
                    FactorToVaryColours = "Graph series",
                    TableName = "DailyPredictedObserved",
                    XFieldName = $"Observed.Date",
                    YFieldName = $"Observed.{variable}",
                    ShowInLegend = true,
                    IncludeSeriesNameInLegend = false,
                    Cumulative = false,
                    CumulativeX = false,
                    Name = $"Observed {variable}"
                };

                graph.Children.Add(predicted);
                graph.Children.Add(observed);
            }

            return model.Add(panel, next);
        }

        private static Graph BuildGraph(string variable, List<Axis> axes)
        {
            var graph = new Graph()
            {
                Name = variable,
                Axis = axes,
                LegendPosition = Graph.LegendPositionType.TopLeft,
                DisabledSeries = new List<string>()
            };

            return graph;
        }

        #endregion

        public static IModel AddExperiment(this IModel model, NextNode next, IApsimX apsim, int id, string name)
        {
            

            var folder = new Folder() { Name = name };

            var request = apsim.Mediator.Send(new TreatmentsQuery() { ExperimentId = id });
            request.Wait();
            var result = request.Result;

            EventManager.InvokeNextItem(null, new NextItemArgs() { Item = name, Maximum = result.Length });

            foreach (var treatment in result)
                folder.AddTreatment(apsim, name + "_" + treatment.Value, id, treatment.Key);

            return model.Add(folder, next);
        }

        public static void AddTreatment(this IModel model, IApsimX apsim, string name, int eId, int tId)
        {
            var dVars = new string[] { "GrainTempFactor" };
            var dEvents = new string[] { "[Clock].DoReport" };
            var hEvents = new string[] { "[Sorghum].Harvesting" };

            var sim = new Simulation() { Name = name };

            sim.Add<Summary>(NextNode.Sibling)
                .Add<Clock, ClockQuery>(NextNode.Sibling, apsim, eId)
                .AddWeather(NextNode.Sibling, apsim, eId)
                .Add<SoilArbitrator>(NextNode.Sibling)
                .Add<Zone, ZoneQuery>(NextNode.Child, apsim, eId)
                    .Add<SoilCrop, SoilCropQuery>(NextNode.Sibling, apsim, eId)
                    .AddSurfaceOrganicMatter(NextNode.Sibling)
                    .AddReport(NextNode.Sibling, "Daily", dVars, dEvents)
                    .AddReport(NextNode.Sibling, "Harvest", null, hEvents)
                    .Add<Folder>(NextNode.Child, "Managers")
                        .AddSowingManager(NextNode.Sibling, apsim, eId)
                        .AddHarvestManager(NextNode.Parent)
                    .AddOperations(NextNode.Sibling, apsim, tId)
                    .Add<Irrigation>(NextNode.Sibling, "Irrigation")
                    .Add<Fertiliser>(NextNode.Sibling, "Fertiliser")
                    .Add<Soil, SoilQuery>(NextNode.Child, apsim, eId)
                        .Add<Physical, PhysicalQuery>(NextNode.Sibling, apsim, eId)
                        .Add<WaterBalance, WaterBalanceQuery>(NextNode.Sibling, apsim, eId)
                        .Add<SoilNitrogen>(NextNode.Child, "SoilNitrogen")
                            .Add<SoilNitrogenNH4>(NextNode.Sibling, "NH4")
                            .Add<SoilNitrogenNO3>(NextNode.Sibling, "NO3")
                            .Add<SoilNitrogenUrea>(NextNode.Sibling, "Urea")
                            .Add<SoilNitrogenPlantAvailableNH4>(NextNode.Sibling, "PlantAvailableNH4")
                            .Add<SoilNitrogenPlantAvailableNO3>(NextNode.Parent, "PlantAvailableNH4")
                        .Add<Organic, OrganicQuery>(NextNode.Sibling, apsim, eId)
                        .Add<Chemical, ChemicalQuery>(NextNode.Sibling, apsim, eId)
                        .Add<Sample, SampleQuery>(NextNode.Sibling, apsim, eId)
                        .Add<CERESSoilTemperature>(NextNode.Parent, "SoilTemperature")
                    .Add<Plant, PlantQuery>(NextNode.None, apsim, eId);

            model.Children.Add(sim);

            EventManager.InvokeProgressIncremented(null, EventArgs.Empty);
        }

        public static IModel AddWeather(this IModel model, NextNode next, IApsimX apsim, int id)
        {
            // Look for metfile, if it does not exist, create it
            MetWriter.GenerateMetFile(apsim.Mediator, id);

            // TODO: Sort out weather 
            var weather = new Weather()
            {
                //FileName = metfile                
            };

            return model.Add(weather, next);
        }

        public static IModel AddSurfaceOrganicMatter(this IModel model, NextNode next)
        {
            var organic = new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            };

            return model.Add(organic, next);
        }

        public static IModel AddReport(this IModel model, NextNode next, string name, string[] variables, string[] events)
        {
            var report = new Report()
            {
                Name = name + "Report",
                VariableNames = variables,
                EventNames = events
            };

            return model.Add(report, next);
        }
    
        public static IModel AddSowingManager(this IModel model, NextNode next, IApsimX apsim, int id)
        {
            var request = apsim.Mediator.Send(new SowingSummary() { ExperimentId = id });
            request.Wait();

            var summary = request.Result;

            var sowing = new Manager()
            {
                Name = "Sow SkipRow on a fixed date",
                Code = DataFiles.ReadRawText("SkipRow.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Date", summary["Date"]),
                    new KeyValuePair<string, string>("Density", ""),
                    new KeyValuePair<string, string>("Depth", summary["Depth"]),
                    new KeyValuePair<string, string>("Cultivar", summary["Cultivar"]),
                    new KeyValuePair<string, string>("RowSpacing", summary["Row"]),
                    new KeyValuePair<string, string>("RowConfiguration", ""),
                    new KeyValuePair<string, string>("Ftn", "")
                }
            };

            return model.Add(sowing, next);
        }
    
        public static IModel AddHarvestManager(this IModel model, NextNode next)
        {
            var harvest = new Manager()
            {
                Name = "Harvesting rule",
                Code = DataFiles.ReadRawText("Harvest.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
            };

            return model.Add(harvest, next);
        }

        public static IModel AddOperations(this IModel model, NextNode next, IApsimX apsim, int id)
        {
            var operations = new Operations()
            {
                Operation = new List<Operation>()
            };
            

            var irrigations = apsim.Mediator.Send(new IrrigationsQuery() { TreatmentId = id });
            var fertilizations = apsim.Mediator.Send(new FertilizationsQuery() { TreatmentId = id });

            if (irrigations.Result is Operation[] i) operations.Operation.AddRange(i);
            if (fertilizations.Result is Operation[] f) operations.Operation.AddRange(f);

            return model.Add(operations, next);
        }        
    }
}
