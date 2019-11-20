using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

using Rems.Application.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Zone> BuildField(int treatmentId)
        {
            // TODO: Fix this to use variables
            var zone = new Zone()
            {
                Name = "",
                Slope = 0,
                Area = 1
            };

            var daily = new Report()
            {
                Name = "DailyReport",
                VariableNames = new string[] { "GrainTempFactor" },
                EventNames = new string[] { "[Clock].DoReport" }
            };
            zone.Children.Add(daily);

            var harvest = new Report()
            {
                Name = "HarvestReport",
                EventNames = new string[] { "[Sorghum].Harvesting" }
            };            
            zone.Children.Add(harvest);

            var sowing = await _mediator.Send(new SowingQuery() { Id = 1 });
            zone.Children.Add(BuildManagers(sowing));

            zone.Children.Add(await BuildOperations(treatmentId));
            zone.Children.Add(new Irrigation() { Name = "Irrigation" });
            zone.Children.Add(new Fertiliser() { Name = "Fertiliser" });
            
            // TODO: Fix this to use variables not constants
            zone.Children.Add(await BuildSoil(1, 0, 0));

            zone.Children.Add(new Plant()
            {
                CropType = "name",
                Name = "Sorghum",
                ResourceName = "Sorghum"
            });

            return zone;
        }

    }
}
