using System.Collections.Generic;

using Models;
using Models.Core;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static Folder BuildCombinedResults()
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

            return folder;
        }

    }
}
