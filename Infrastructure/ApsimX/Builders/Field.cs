using System.Threading.Tasks;

using Models;
using Models.Core;
using Models.PMF;
using Models.Report;

using Rems.Application.Queries;
using Rems.Application.Treatments.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Zone> BuildField(TreatmentDetailVm treatment)
        {
            var zone = new Zone()
            {
                Name = "Paddock",
                Slope = treatment.FieldSlope,
                Area = treatment.FieldArea
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

            var sowing = await _mediator.Send(new SowingQuery() { Id = treatment.SowingId });
            zone.Children.Add(BuildManagers(sowing));
            zone.Children.Add(await BuildOperations(treatment.Id));
            zone.Children.Add(new Irrigation() { Name = "Irrigation" });
            zone.Children.Add(new Fertiliser() { Name = "Fertiliser" });            
            zone.Children.Add(await BuildSoil(treatment));
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
