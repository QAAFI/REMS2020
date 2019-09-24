using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Models;
using Models.Core;
using Models.Functions;
using Models.Functions.DemandFunctions;
using Models.Functions.SupplyFunctions;
using Models.PMF;
using Models.PMF.Library;
using Models.PMF.Organs;
using Models.PMF.Phen;
using Models.PMF.Struct;
using Models.PostSimulationTools;
using Models.Report;
using Models.Soils;
using Models.Storage;
using Models.Surface;

namespace Models
{
    public static class ExampleApsimX
    {
        public static void GenerateExampleA(string file)
        {
            var simulations = new Simulations();

            simulations.Add(ExampleDataStore());
            //simulations.Add(ExampleReplacements());
            simulations.Add(ExampleValidation());

            using (StreamWriter stream = new StreamWriter(file))
            using (JsonWriter writer = new JsonTextWriter(stream))
            {
                writer.CloseOutput = true;
                writer.AutoCompleteOnClose = true;

                JsonSerializer serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Objects
                };
                serializer.Serialize(writer, simulations);

                serializer = null;
                simulations.Dispose();
            }
        }

        private static DataStore ExampleDataStore()
        {
            var dataStore = new DataStore();

            dataStore.Add(new ExcelInput() { Name = "ExampleExcel", FileName = "Example.xlsx"});
            dataStore.Add(new PredictedObserved() { Name = "ExampleObserved" });
            dataStore.Add(new Input() { Name = "ExampleInput" });

            return dataStore;
        }

        //private static Replacements ExampleReplacements()
        //{
        //    var replacements = new Replacements() { Name = "Replacements" };

        //    replacements.Add(ExamplePlant());
        //    replacements.Add(ExampleReport());

        //    return replacements;
        //}

        //private static Plant ExamplePlant()
        //{
        //    var plant = new Plant() { Name = "ExamplePlant" };

        //    plant.Add(ExamplePhenology());
        //    //plant.Add(ExampleStructure());
        //    //plant.Add(ExampleGrain());
        //    //plant.Add(ExampleRoot());
        //    //plant.Add(ExampleLeaf());
        //    //plant.Add(ExampleOrgan());
        //    //plant.Add(ExampleAdd());
        //    //plant.Add(ExampleCultivar());

        //    return plant;
        //}

        //private static Phenology ExamplePhenology()
        //{
        //    var phenology = new Phenology();

        //    //phenology.Add(ExamplePhaseLookup());
        //    //phenology.Add(ExampleMultiply());
        //    //phenology.Add(ExampleWeather());
        //    //phenology.Add(ExampleGerminating());
        //    //phenology.Add(ExampleEmerging());
        //    //phenology.Add(ExamplePhase());
        //    //phenology.Add(ExampleEndPhase());
        //    //phenology.Add(ExamplePhotoperiod());
        //    //phenology.Add(ExampleEvent());
        //    //phenology.Add(ExampleConstant());
        //    //phenology.Add()

        //    return phenology;
        //}

        //private static Report.Report ExampleReport()
        //{
        //    var report = new Report.Report() { Name = "ExampleReport" };

        //    return report;
        //}

        private static Folder ExampleValidation()
        {
            var validation = new Folder() { Name = "Validation" };

            validation.Add(ExampleSimulation());

            return validation;
        }

        private static Simulation ExampleSimulation()
        {
            var simulation = new Simulation() { Name = "Example" };

            simulation.Add(new Clock() { Name = "Clock" });
            simulation.Add(new Summary() { Name = "SummaryFile" });
            simulation.Add(new Weather() { Name = "Weather" });
            simulation.Add(ExampleZone());
            simulation.Add(new SurfaceOrganicMatter() { Name = "SurfaceOrganicMatter" });

            return simulation;
        }

        private static Zone ExampleZone()
        {
            var zone = new Zone() { Name = "ExamplePaddock" };

            zone.Add(ExampleManagers());
            zone.Add(new Irrigation() { Name = "Irrigation" });
            zone.Add(new Fertiliser() { Name = "Fertiliser" });
            zone.Add(new Operations() { Name = "Operations" });
            zone.Add(ExampleSoil());
            zone.Add(new Plant() { Name = "ExamplePlant" });
            zone.Add(new Report.Report() { Name = "ExampleReport" });

            return zone;
        }

        private static Folder ExampleManagers()
        {
            var managers = new Folder(){ Name = "Manager folder" };

            managers.Add(new Manager() { Name = "ExampleManagerA" });
            managers.Add(new Manager() { Name = "ExampleManagerB" });
            managers.Add(new Manager() { Name = "ExampleManagerC" });

            return managers;
        }

        private static Soil ExampleSoil()
        {
            var soil = new Soil() { Name = "Soil" };

            soil.Add(ExampleWater());
            soil.Add(new SoilWater() { Name = "ExampleWater" });
            soil.Add(ExampleSoilNitrogen());
            //soil.Add(new SoilOrganicMatter() { Name = "ExampleOrganic" });
            //soil.Add(new Analysis() { Name = "ExampleAnalysis" });
            soil.Add(new Sample() { Name = "Initial Water" });
            soil.Add(new Sample() { Name = "Initial Nitrogen" });
            soil.Add(new CERESSoilTemperature() { Name = "ExampleSoilTemperature" });

            return soil;
        }

        private static Water ExampleWater()
        {
            var water = new Water() { Name = "ExampleWater" };

            water.Add(new SoilCrop() { Name = "ExampleSoil" });

            return water;
        }

        private static SoilNitrogen ExampleSoilNitrogen()
        {
            var nitrogen = new SoilNitrogen() { Name = "SoilNitrogen" };

            nitrogen.Add(new SoilNitrogenNH4() { Name = "NH4" });
            nitrogen.Add(new SoilNitrogenNO3() { Name = "NO3" });
            nitrogen.Add(new SoilNitrogenUrea() { Name = "Urea" });
            nitrogen.Add(new SoilNitrogenPlantAvailableNH4() { Name = "PlantAvailableNH4" });
            nitrogen.Add(new SoilNitrogenPlantAvailableNO3() { Name = "PlantAvailableNO3" });

            return nitrogen;
        }
    }
}
