using System.Collections.Generic;

using Models;
using Models.Graph;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static GraphPanel BuildPanel(Dictionary<string, IEnumerable<string>> variables, string scriptFile)
        {
            var panel = new GraphPanel()
            {
                NumCols = 3,
                Name = "PredictedObserved"
            };

            var manager = new Manager()
            {
                Name = "Config",
                Code = DataFiles.ReadRawText(scriptFile)
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

            return panel;
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
                Checkpoint = "Current",
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
                Checkpoint = "Current",
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

    }
}
