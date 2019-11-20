using System.Threading.Tasks;

using Models.Soils;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Soil> BuildSoil(int SoilId, int lat, int lon)
        {
            var model = new Soil()
            {
                Name = "Soil",
                Latitude = lat,
                Longitude = lon
            };

            model.Children.Add(await BuildPhysical(SoilId));
            model.Children.Add(await BuildSoilWater(SoilId));
            model.Children.Add(BuildSoilNitrogen());
            model.Children.Add(await BuildSoilOrganicMatter(SoilId));
            model.Children.Add(await BuildChemicalAnalysis(SoilId));
            model.Children.Add(BuildSample("Initial Water"));
            model.Children.Add(BuildSample("Initial Nitrogen"));
            model.Children.Add(new CERESSoilTemperature() { Name = "ExampleSoilTemperature" });

            return model;
        }
    }
}
