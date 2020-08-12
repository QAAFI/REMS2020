using System;
using System.Collections.Generic;
using System.Text;

using Models;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static Graph BuildGraph(string variable, List<Axis> axes)
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
    }
}
