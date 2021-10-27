using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Models.Core;
using Models.Soils;
using Models.Soils.Nutrients;
using Models.WaterModel;

using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Rems.Application.Common.Extensions;

namespace Rems.Infrastructure.ApsimX
{
    public class SoilTemplate : ITemplate<Soil>
    {
        readonly IFileManager Manager = FileManager.Instance;

        readonly IQueryHandler Handler;

        readonly IProgress<int> Progress;

        readonly Markdown Summary;

        readonly int ID;

        public SoilTemplate(int id, IQueryHandler handler, IProgress<int> progress, Markdown summary)
        {
            ID = id;
            Handler = handler;
            Progress = progress;
            Summary = summary;
        }

        public Soil Create() => AsyncCreate().Result;

        public async Task<Soil> AsyncCreate()
        {
            var query = new SoilModelTraitsQuery { ExperimentId = ID };
            var traits = Handler.Query(query).Result;

            var info = Manager.GetFileInfo($"{query.Crop}Soil") ?? Manager.GetFileInfo("DefaultSoil");
            var template = JsonTools.LoadJson<Soil>(info);

            if (query.Crop == "Soybean")
            {
                template.Name = "SoybeanSoil";
                return template;
            }

            if (traits["Thickness"] is not double[] thickness)
            {
                Summary.AddSubHeading("Soil model", 2);
                Summary.AddLine("No soil layer data found. A template soil model has been used. " +
                    "Check the sensibility before running the simulation.");

                template.FindDescendant<SoilCrop>().Name = query.Crop + "Soil";

                Progress.Report(1);
                return template;
            }

            var depths = template.FindDescendant<Physical>().Thickness.Cumulative();
            for (int i = depths.Length - 1; i >= 0; i--)
                depths[i] -= depths[0];

            var ds = thickness.Cumulative();
            for (int i = ds.Length - 1; i > 0; i--)
                ds[i] -= (ds[i] - ds[i - 1]) / 2;
            ds[0] /= 2;

            double[] getValues<T>(string name)
            {
                // If we found the trait in the query
                if (traits.TryGetValue(name, out double[] result) && result is not null)
                    return result;

                // Look for a template value
                var type = typeof(T);
                if (template.FindDescendant<T>() is T model)
                    if (type.GetProperty(name) is PropertyInfo info)
                        if (info.GetValue(model) is double[] values)
                        {
                            var table = new LookupTable(depths, values);
                            return ds.Select(d => table.LookUp(d)).ToArray();
                        }

                // If no template exists, return the default
                return new double[thickness.Length];
            }

            var missing = traits.Where(t => t.Value is null);
            if (missing.Any())
            {
                Summary.AddSubHeading("Soil model:", 2);
                Summary.AddLine("No data was found for the following soil layer traits:");
                Summary.AddList(missing.Select(t => t.Key));
                Summary.AddLine("\nThese values have been provided a default value. " +
                    "It is recommended to check the sensibility of these values before " +
                    "running the simulation.");
            }

            var physical = new Physical
            {
                Thickness = thickness,
                BD = getValues<Physical>(nameof(Physical.BD)),
                AirDry = getValues<Physical>(nameof(Physical.AirDry)),
                LL15 = getValues<Physical>(nameof(Physical.LL15)),
                DUL = getValues<Physical>(nameof(Physical.DUL)),
                SAT = getValues<Physical>(nameof(Physical.SAT)),
                KS = getValues<Physical>(nameof(Physical.KS))
            };
            var soilcrop = new SoilCrop
            {
                Name = query.Crop + "Soil",
                LL = getValues<SoilCrop>(nameof(SoilCrop.LL)),
                KL = getValues<SoilCrop>(nameof(SoilCrop.KL)),
                XF = getValues<SoilCrop>(nameof(SoilCrop.XF))
            };
            physical.Children.Add(soilcrop);
            var balance = new WaterBalance
            {
                Name = "SoilWater",
                Thickness = thickness,
                SWCON = getValues<WaterBalance>(nameof(WaterBalance.SWCON)),
                KLAT = getValues<WaterBalance>(nameof(WaterBalance.KLAT)),
                SummerDate = "1-Nov",
                SummerU = 1.5,
                SummerCona = 6.5,
                WinterDate = "1-Apr",
                WinterU = 1.5,
                WinterCona = 6.5,
                DiffusConst = 40,
                DiffusSlope = 16,
                Salb = 0.2,
                CN2Bare = 85,
                DischargeWidth = double.NaN,
                CatchmentArea = double.NaN,
                ResourceName = "WaterBalance"
            };
            var organic = new Organic
            {
                Name = nameof(Organic),
                Depth = query.Depth,
                Thickness = thickness,
                Carbon = getValues<Organic>(nameof(Organic.Carbon)),
                SoilCNRatio = getValues<Organic>(nameof(Organic.SoilCNRatio)),
                FBiom = getValues<Organic>(nameof(Organic.FBiom)),
                FInert = getValues<Organic>(nameof(Organic.FInert)),
                FOM = getValues<Organic>(nameof(Organic.FOM))
            };
            var chemical = new Chemical
            {
                Depth = query.Depth,
                Thickness = thickness,
                NO3N = getValues<Chemical>(nameof(Chemical.NO3N)),
                NH4N = getValues<Chemical>(nameof(Chemical.NH4N)),
                PH = getValues<Chemical>(nameof(Chemical.PH))
            };
            var water = new InitialWater
            {
                PercentMethod = 0,
                FractionFull = 0.6
            };
            var sample = new Sample
            {
                Name = "Initial conditions",
                Depth = new[] { "0-180" },
                Thickness = new[] { 1800.0 },
                NH4 = new[] { 1.0 }
            };
            var temperature = new CERESSoilTemperature
            {
                Name = "Temperature"
            };

            IModel N = query.Crop.ToUpper() != "SORGHUM"
                ? new Nutrient { ResourceName = "Nutrient" }
                : new SoilNitrogen
                {
                    Name = "SoilNitrogen",
                    Children = new List<IModel>
                    {
                        new SoilNitrogenNO3 { Name = "NO3" },
                        new SoilNitrogenNH4 { Name = "NH4" },
                        new SoilNitrogenUrea { Name = "Urea" }
                    }
                };

            var models = new IModel[] { physical, balance, organic, chemical, water, sample, temperature, N };

            var soil = await Handler.Query(new SoilQuery { ExperimentId = ID, Report = Summary });

            foreach (var model in models)
                soil.Children.Add(model);

            Progress.Report(1);

            return soil;
        }
    }
}
