using System;
using System.Collections.Generic;
using System.Text;

using Models;
using Models.Core;
using Models.Core.ApsimFile;
using Models.Core.Run;
using Models.Graph;
using Models.PMF;
using Models.PostSimulationTools;
using Models.Report;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Storage;
using Models.Surface;

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
