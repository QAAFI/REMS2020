using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using Models.Climate;
using System.IO;

namespace Rems.Application.CQRS
{
    public class MetFileDataQuery : IRequest<Weather>
    {
        public int ExperimentId { get; set; }
    }

    public class MetFileDataQueryHandler : IRequestHandler<MetFileDataQuery, Weather>
    {
        private readonly IRemsDbContext _context;

        public MetFileDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Weather> Handle(MetFileDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private Weather Handler(MetFileDataQuery request, CancellationToken token)
        {
            var met = _context.Experiments.Find(request.ExperimentId)
                .MetStation
                .Name;

            string file = met + ".met";

            if (!File.Exists(file))
            {
                using (var stream = new FileStream(file, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    var contents = BuildContents(request.ExperimentId);
                    writer.Write(contents);
                    writer.Close();
                }
            }

            return new Weather()
            {
                FileName = file
            };
        }

        private string BuildContents(int id)
        {
            var experiment = _context.Experiments.Find(id);
            var station = experiment.MetStation;

            var builder = new StringBuilder();
            builder.AppendLine("[weather.met.weather]");
            builder.AppendLine($"!experiment number = {experiment.ExperimentId}");
            builder.AppendLine($"!experiment = {experiment.Name}");
            builder.AppendLine($"!station name = {station.Name}");
            builder.AppendLine($"latitude = {station.Latitude} (DECIMAL DEGREES)");
            builder.AppendLine($"longitude = {station.Longitude} (DECIMAL DEGREES)");
            builder.AppendLine($"tav = {station.TemperatureAverage} (oC)");
            builder.AppendLine($"amp = {station.Amplitude} (oC)\n");

            Trait maxT = _context.GetTraitByName("MaxT", null);
            Trait minT = _context.GetTraitByName("MinT", null);
            Trait radn = _context.GetTraitByName("Radn", null);
            Trait rain = _context.GetTraitByName("Rain", null);

            var datas = station.MetData
                .ToArray()
                .GroupBy(d => d.Date)
                .OrderBy(d => d.Key);

            foreach (var data in datas)
            {
                var date = data.Key;
                var mets = data.AsEnumerable();

                builder.Append($"{date.Year,-7}");
                builder.Append($"{date.DayOfYear,3}");
                builder.Append($"{GetTraitValue(mets, maxT),8}");
                builder.Append($"{GetTraitValue(mets, minT),8}");
                builder.Append($"{GetTraitValue(mets, radn),8}");
                builder.AppendLine($"{GetTraitValue(mets, rain),8}");
            }

            string GetTraitValue(IEnumerable<MetData> mets, Trait trait)
            {
                var data = mets.FirstOrDefault(d => d.TraitId == trait.TraitId);

                if (data.Value is double value)
                    return Math.Round(value, 2).ToString();

                return "";
            }

            return builder.ToString();
        }
    }
}
