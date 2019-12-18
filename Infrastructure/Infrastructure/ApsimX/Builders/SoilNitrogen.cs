using Models.Soils;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static SoilNitrogen BuildSoilNitrogen()
        {
            var model = new SoilNitrogen() { Name = "SoilNitrogen" };

            model.Children.Add(new SoilNitrogenNH4() { Name = "NH4" });
            model.Children.Add(new SoilNitrogenNO3() { Name = "NO3" });
            model.Children.Add(new SoilNitrogenUrea() { Name = "Urea" });
            model.Children.Add(new SoilNitrogenPlantAvailableNH4() { Name = "PlantAvailableNH4" });
            model.Children.Add(new SoilNitrogenPlantAvailableNO3() { Name = "PlantAvailableNO3" });

            return model;
        }
    }
}
