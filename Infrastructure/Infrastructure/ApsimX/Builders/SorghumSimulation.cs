using System.Threading.Tasks;

using Models;
using Models.Climate;
using Models.Core;
using Models.Soils.Arbitrator;
using Models.Surface;

using Rems.Application.Treatments.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Simulation> BuildSorghumSimulation(TreatmentDetailVm treatment)
        {
            var simulation = new Simulation()
            {
                Name = $"{treatment.CropName}_{treatment.ExperimentName}_{treatment.Id}"
            };

            simulation.Children.Add(new Clock()
            {
                Name = "Clock",
                StartDate = treatment.Start,
                EndDate = treatment.End
            });

            simulation.Children.Add(new Summary()
            {
                Name = "Summary"
            });

            simulation.Children.Add(new Weather()
            {
                Name = "Weather",
                FileName = treatment.MetFileName
            });

            simulation.Children.Add(new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            });

            simulation.Children.Add(new SoilArbitrator() { Name = "SoilArbitrator" });

            simulation.Children.Add(await BuildField(treatment));

            return simulation;
        }

    }
}
