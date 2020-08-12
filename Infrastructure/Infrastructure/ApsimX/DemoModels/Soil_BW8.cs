using Models.Soils;
using Models.WaterModel;

namespace Rems.Infrastructure.DemoModels
{
    static class Soil_BW8
    {
        private static Soil GetDemoBW8()
        {
            var soil = new Soil()
            {
                Name = "Soil",
                SoilType = "BW8",
                Site = "ICRISAT",
                Region = "Hyderebad",
                Latitude = 18.0,
                Longitude = 76.0
            };

            var phys = new Physical
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                BD = new[] { 1.49,
                                1.41,
                                1.41,
                                1.56,
                                1.49,
                                1.52,
                                1.61,
                                1.68,
                                1.63,
                                1.62,
                                1.62 },
                AirDry = new[] { 0.12,
                                    0.15,
                                    0.15,
                                    0.1,
                                    0.11,
                                    0.13,
                                    0.09,
                                    0.08,
                                    0.09,
                                    0.09,
                                    0.09 },
                LL15 = new[] { 0.16,
                                0.19,
                                0.19,
                                0.14,
                                0.15,
                                0.17,
                                0.13,
                                0.12,
                                0.13,
                                0.13,
                                0.13 },
                DUL = new[] { 0.32,
                                0.35,
                                0.35,
                                0.28,
                                0.31,
                                0.33,
                                0.26,
                                0.22,
                                0.24,
                                0.24,
                                0.24 },
                SAT = new[] { 0.43,
                                0.46,
                                0.46,
                                0.41,
                                0.43,
                                0.42,
                                0.39,
                                0.36,
                                0.38,
                                0.38,
                                0.38 }
            };

            var soilCrop = new SoilCrop()
            {
                Name = "SorghumSoil",
                LL = new[] { 0.16,
                                0.19,
                                0.19,
                                0.14,
                                0.15,
                                0.17,
                                0.13,
                                0.12,
                                0.13,
                                0.13,
                                0.13 },
                KL = new[] { 0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.07,
                                0.04,
                                0.04 },
                XF = new[] { 1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0,
                                1.0 }
            };

            phys.Children.Add(soilCrop);

            var sw = new WaterBalance()
            {
                SummerDate = "1-Nov",
                SummerU = 6.0,
                SummerCona = 3.5,
                WinterDate = "1-Apr",
                WinterU = 6.0,
                WinterCona = 3.5,
                DiffusConst = 40.0,
                DiffusSlope = 16.0,
                Salb = 0.11,
                CN2Bare = 85.0,
                CNRed = 0.8,
                CNCov = 0.2,
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                SWCON = new[] { 0.16,
                                    0.11,
                                    0.14,
                                    0.14,
                                    0.11,
                                    0.11,
                                    0.1,
                                    0.1,
                                    0.1,
                                    0.12,
                                    0.1 }
            };
            soil.Children.Add(phys);
            soil.Children.Add(sw);

            soil.Children.Add(ApsimBuilder.BuildSoilNitrogen());

            var organic = new Organic()
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                FOMCNRatio = 45.0,
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                Carbon = new[] { 0.49,
                                    0.45,
                                    0.28,
                                    0.18,
                                    0.14,
                                    0.11,
                                    0.09,
                                    0.07,
                                    0.06,
                                    0.05,
                                    0.03 },
                SoilCNRatio = new[] { 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5, 14.5 },
                FBiom = new[] { 0.05, 0.02, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01, 0.01 },
                FInert = new[] { 0.39, 0.47, 0.52, 0.62, 0.74, 0.83, 0.93, 0.93, 0.93, 0.93, 0.93 },
                FOM = new[] { 226.92975737066317,
                                180.16467610969974,
                                143.03681850192319,
                                113.56017111087074,
                                90.157992870603834,
                                71.5784733233617,
                                56.827771783434649,
                                45.116855612176408,
                                35.819293920007865,
                                22.577373292364666,
                                14.230816104894044 }
            };
            soil.Children.Add(organic);

            var chem = new Chemical()
            {
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 },
                NO3N = new[] { 9.0,
                                11.0,
                                8.0,
                                6.0,
                                4.0,
                                3.0,
                                3.0,
                                3.0,
                                6.0,
                                4.0,
                                5.0 },
                NH4N = new[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1 },
                PH = new[] { 8.35, 8.52, 8.8, 8.95, 9.0, 9.0, 9.0, 9.0, 8.92, 8.97, 8.82 }
            };
            soil.Children.Add(chem);

            var initNitrogen = new Sample()
            {
                Name = "Initial Nitrogen",
                Depth = new[] { "0-15", "15-30", "30-45", "45-60", "60-75", "75-90", "90-105", "105-120", "120-135", "135-165", "165-195" },
                Thickness = new[] { 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 150.0, 300.0, 300.0 }
            };
            soil.Children.Add(initNitrogen);

            var initWater = new InitialWater()
            {
                Name = "Initial Water",
                PercentMethod = 0,
                FractionFull = 0.50
            };
            soil.Children.Add(initWater);

            var ceres = new CERESSoilTemperature();
            soil.Children.Add(ceres);

            return soil;
        }

    }
}
