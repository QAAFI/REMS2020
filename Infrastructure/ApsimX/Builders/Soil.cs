using System.Threading.Tasks;

using Models.Soils;

using Rems.Application.Treatments.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Soil> BuildSoil(TreatmentDetailVm treatment)
        {
            var model = new Soil()
            {
                Name = "Soil",
                Latitude = treatment.FieldLat,
                Longitude = treatment.FieldLon
            };

            model.Children.Add(await BuildPhysical(treatment.SoilId));
            model.Children.Add(await BuildSoilWater(treatment.SoilId));
            model.Children.Add(BuildSoilNitrogen());
            model.Children.Add(await BuildSoilOrganicMatter(treatment.SoilId));
            model.Children.Add(await BuildChemicalAnalysis(treatment.SoilId));
            model.Children.Add(await BuildSample("Initial Water", treatment.SoilId));
            model.Children.Add(await BuildSample("Initial Nitrogen", treatment.SoilId));
            model.Children.Add(new CERESSoilTemperature() { Name = "SoilTemperature" });

            return model;
        }
    }
}
