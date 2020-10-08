using MediatR;
using Models;
using Models.Climate;
using Models.Core;
using Models.Factorial;
using Models.PMF;
using Models.PostSimulationTools;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Surface;
using Models.WaterModel;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rems.Application.Common.Extensions
{
    public enum NextNode
    {
        Parent,
        Sibling,
        Child,
        None
    }

    public static class ModelExtensions
    {
        #region General
        public static IModel Add<M, R>(this IModel model, NextNode next, params object[] args)
            where M : IModel
            where R : IRequest<M>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(args);

            var child = EventManager.OnSendQuery(request);
            return model.Add(child, next);
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
            EventManager.InvokeProgressIncremented();

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
        #endregion

        #region Data
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
        #endregion

        #region Simulations       

        public static IModel AddExperiment(this IModel model, NextNode next, int id, string name)
        {
            var experiment = new Experiment() { Name = name };
            experiment.Add<Factors>(NextNode.Child)
                .Add<Permutation>(NextNode.Child)
                    .AddFactors(id);
            
            experiment.AddTreatment(id);
            return model.Add(experiment, next);
        }

        public static void AddFactors(this IModel model, int id)
        {
            var factors = EventManager.OnSendQuery(new FactorQuery() { ExperimentId = id });

            foreach (var factor in factors)
            {
                model.Children.Add(factor);
            }
        }

        public static void AddTreatment(this IModel model, int id)
        {
            var dVars = new string[] { "GrainTempFactor" };
            var dEvents = new string[] { "[Clock].DoReport" };
            var hEvents = new string[] { "[Sorghum].Harvesting" };

            var sim = new Simulation() { Name = "Base" };

            // Look for metfile, if it does not exist, create it
            sim.Add<Clock, ClockQuery>(NextNode.Sibling, id)
                .Add<Summary>(NextNode.Sibling)
                .AddWeather(NextNode.Sibling, id)
                .Add<SoilArbitrator>(NextNode.Sibling)
                .Add<Zone, ZoneQuery>(NextNode.Child, id)
                    .Add<Plant, PlantQuery>(NextNode.Sibling, id)
                    .AddSoil(NextNode.Sibling, id)
                    .AddSurfaceOrganicMatter(NextNode.Sibling)
                    .Add<Operations>(NextNode.Sibling)
                    .Add<Irrigation>(NextNode.Sibling, "Irrigation")
                    .Add<Fertiliser>(NextNode.Sibling, "Fertiliser")
                    .Add<Folder>(NextNode.Child, "Managers")
                        .AddSowingManager(NextNode.Sibling, id)
                        .AddHarvestManager(NextNode.Parent)
                    .AddReport(NextNode.Sibling, "Daily", dVars, dEvents)
                    .AddReport(NextNode.None, "Harvest", null, hEvents);

            model.Children.Add(sim);

            EventManager.InvokeProgressIncremented();
        }
        #endregion

        #region Models
        public static IModel AddSoil(this IModel model, NextNode next, int id)
        {
            var soil = EventManager.OnSendQuery(new SoilQuery() { ExperimentId = id });

            soil.Add<Physical, PhysicalQuery>(NextNode.Child, id)
                    .Add<SoilCrop, SoilCropQuery>(NextNode.Parent, id)
                .Add<WaterBalance, WaterBalanceQuery>(NextNode.Sibling, id)
                .Add<SoilNitrogen>(NextNode.Child, "SoilNitrogen")
                    .Add<SoilNitrogenNH4>(NextNode.Sibling, "NH4")
                    .Add<SoilNitrogenNO3>(NextNode.Sibling, "NO3")
                    .Add<SoilNitrogenUrea>(NextNode.Sibling, "Urea")
                    .Add<SoilNitrogenPlantAvailableNH4>(NextNode.Sibling, "PlantAvailableNH4")
                    .Add<SoilNitrogenPlantAvailableNO3>(NextNode.Parent, "PlantAvailableNH4")
                .Add<Organic, OrganicQuery>(NextNode.Sibling, id)
                .Add<Chemical, ChemicalQuery>(NextNode.Sibling, id)
                .Add<Sample, SampleQuery>(NextNode.Sibling, id)
                .Add<CERESSoilTemperature>(NextNode.Parent, "SoilTemperature");

            return model.Add(soil, next);
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

        public static IModel AddSowingManager(this IModel model, NextNode next, int id)
        {
            var summary = EventManager.OnSendQuery(new SowingSummary() { ExperimentId = id });

            var sowing = new Manager()
            {
                Name = "Sow SkipRow on a fixed date",
                Code = EventManager.OnRequestRawData("SkipRow.cs.txt"),
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
                Code = EventManager.OnRequestRawData("Harvest.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
            };

            return model.Add(harvest, next);
        }

        public static IModel AddWeather(this IModel model, NextNode next, int id)
        {
            var met = EventManager.OnSendQuery(new MetStationQuery() { ExperimentId = id });

            string file = met + ".met";

            if (!File.Exists(file))
            {
                using (var stream = new FileStream(file, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    var builder = EventManager.OnSendQuery(new MetFileDataQuery() { ExperimentId = id });
                    writer.Write(builder.ToString());
                    writer.Close();
                }
            }

            var weather = new Weather()
            {
                FileName = file
            };

            return model.Add(weather, next);
        }

        public static IModel AddOperations(this IModel model, NextNode next, int id)
        {
            var operations = new Operations()
            {
                Operation = new List<Operation>()
            };

            var i = EventManager.OnSendQuery(new IrrigationsQuery() { TreatmentId = id });
            operations.Operation.AddRange(i);

            var f = EventManager.OnSendQuery(new FertilizationsQuery() { TreatmentId = id });            
            operations.Operation.AddRange(f);

            return model.Add(operations, next);
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
        #endregion

        #region Graphs
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
                Code = EventManager.OnRequestRawData("PredictedObserved.cs.txt")
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

            

            return model.Add(panel, next);
        }

        private static void AddPredictedObservedPair(Graph graph, string variable)
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
    }
}
