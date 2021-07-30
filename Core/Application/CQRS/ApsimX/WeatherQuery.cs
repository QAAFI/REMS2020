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
        private readonly IRemsDbContextFactory _factory;

        private readonly IFileManager _file;

        public WeatherQueryHandler(IRemsDbContextFactory context, IFileManager file)
        {
            _factory = context;
            _file = file;
        }

        public Task<Weather> Handle(WeatherQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private Weather Handler(WeatherQuery request, CancellationToken token)
        {
            using var _context = _factory.Create();
            // Find the MetStation used by the experiment
            var met = _context.Experiments.Find(request.ExperimentId)
                .MetStation;

            if (!met.MetData.Any()) 
                request.Report.AddLine("No valid met data found. " +
                    "Either import met data or provide a valid file within APSIM NG before " +
                    "running the simulation.");

            // Create a .met file to output to
            string name = met.Name.Replace('/', '-').Replace(' ', '_') + ".met";
            WriteFile(request.ExperimentId, name, request.Report);

            return new Weather { FileName = "met\\" + name };
        }

        private void WriteFile(int id, string name, Markdown report)
        {            
            var info = Directory.CreateDirectory(_file.ExportPath + "\\met");

            using var stream = new FileStream(info.FullName + '\\' + name, FileMode.Create);
            using var writer = new StreamWriter(stream);
            using var _context = _factory.Create();

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
            builder.AppendLine($"{"Year",-5}{"Day",3}{"maxt",5}{"mint",5}{"radn",5}{"rain",6}");
            builder.AppendLine($"{"()",-5}{"()",3}{"()",5}{"()",5}{"()",5}{"()",6}");

            // Find the weather traits
            Trait maxT = _context.GetTraitByName("MaxT");
            Trait minT = _context.GetTraitByName("MinT");
            Trait radn = _context.GetTraitByName("Radn");
            Trait rain = _context.GetTraitByName("Rain");

            var datas = station.MetData
                .ToArray()
                .GroupBy(d => d.Date)
                .OrderBy(d => d.Key);

            // Check the met data covers the experiment
            var span = experiment.EndDate - experiment.BeginDate;
            var days = datas.Select(d => d.Key)
                .Where(d => experiment.BeginDate <= d && d <= experiment.EndDate);

            if (days.Count() < span.TotalDays)
            {
                report.AddLine("The provided met data does not extend for the duration of " +
                    "the experiment. Either import additional data or provide APSIM NG a" +
                    "valid .met file before running the simulation.");

                return;
            }

            // Format and add the data
            foreach (var data in datas)
            {
                var date = data.Key;
                var mets = data.AsEnumerable();

                builder.Append($"{date.Year,-5}");
                builder.Append($"{date.DayOfYear,3}");
                builder.Append($"{GetTraitValue(mets, maxT),5:F1}");
                builder.Append($"{GetTraitValue(mets, minT),5:F1}");
                builder.Append($"{GetTraitValue(mets, radn),5:F1}");
                builder.AppendLine($"{GetTraitValue(mets, rain),6:F1}");
            }

            // Find the value of a trait for a given MetData entity
            string GetTraitValue(IEnumerable<MetData> mets, Trait trait)
            {
                var data = mets.FirstOrDefault(d => d.TraitId == trait.TraitId);

                if (data?.Value is double value)
                    return Math.Round(value, 2).ToString();

                return "0";
            }

            writer.Write(builder.ToString());
            writer.Close();
        }
    }
}
