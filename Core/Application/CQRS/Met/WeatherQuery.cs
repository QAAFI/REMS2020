using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using Models.Climate;
using System.IO;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Weather model for an experiment
    /// </summary>
    public class WeatherQuery : IRequest<Weather>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class WeatherQueryHandler : IRequestHandler<WeatherQuery, Weather>
    {
        private readonly IRemsDbContext _context;

        public WeatherQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Weather> Handle(WeatherQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private Weather Handler(WeatherQuery request, CancellationToken token)
        {
            // Find the MetStation used by the experiment
            var met = _context.Experiments.Find(request.ExperimentId)
                .MetStation;

            if (!met.MetData.Any()) request.Report.ValidateItem("", "Weather Data");

            // Create a .met file to output to
            string file = met.Name.Replace('/', '-').Replace(' ', '_') + ".met";

            using (var stream = new FileStream(file, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                var contents = BuildContents(request.ExperimentId, request.Report);
                writer.Write(contents);
                writer.Close();
            }

            return new Weather { FileName = file };
        }

        private string BuildContents(int id, Markdown report)
        {
            var experiment = _context.Experiments.Find(id);
            var station = experiment.MetStation;

            // Attach header lines to the file
            var builder = new StringBuilder();
            builder.AppendLine("[weather.met.weather]");
            builder.AppendLine($"!experiment number = {experiment.ExperimentId}");
            builder.AppendLine($"!experiment = {experiment.Name}");
            builder.AppendLine($"!station name = {station.Name}");
            builder.AppendLine($"latitude = {station.Latitude ?? 0.0} (DECIMAL DEGREES)");
            builder.AppendLine($"longitude = {station.Longitude ?? 0.0} (DECIMAL DEGREES)");
            builder.AppendLine($"tav = {station.TemperatureAverage} (oC) \t ! annual average ambient temperature");
            builder.AppendLine($"amp = {station.Amplitude} (oC) \t ! annual amplitude in mean monthly temperature");
            builder.AppendLine();
            builder.AppendLine($"{"Year",-7}{"Day",3}{"maxt",8}{"mint",8}{"radn",8}{"rain",8}");
            builder.AppendLine($"{"()",-7}{"()",3}{"()",8}{"()",8}{"()",8}{"()",8}");

            // Find the weather traits
            Trait maxT = _context.GetTraitByName("MaxT");
            Trait minT = _context.GetTraitByName("MinT");
            Trait radn = _context.GetTraitByName("Radn");
            Trait rain = _context.GetTraitByName("Rain");

            var datas = station.MetData
                .ToArray()
                .GroupBy(d => d.Date)
                .OrderBy(d => d.Key);

            if (!datas.Any())
                report.AddLine("* Met data not found for experiment");
            else if (datas.Count() < 10)
                report.AddLine("* Low met data count for experiment");

            // Format and add the data
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

            // Find the value of a trait for a given MetData entity
            string GetTraitValue(IEnumerable<MetData> mets, Trait trait)
            {
                var data = mets.FirstOrDefault(d => d.TraitId == trait.TraitId);

                if (data?.Value is double value)
                    return Math.Round(value, 2).ToString();

                return "";
            }

            return builder.ToString();
        }
    }
}
