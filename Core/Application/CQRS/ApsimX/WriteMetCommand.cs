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
using System.IO;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Writes the met files for each weather station in the given experiments
    /// </summary>
    public class WriteMetCommand : IRequest<(string Station, string File, DateTime Start, DateTime End)[]>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int[] ExperimentIds { get; set; }
    }

    public class WriteMetCommandHandler : IRequestHandler<WriteMetCommand, (string, string, DateTime, DateTime)[]>
    {
        private readonly IRemsDbContextFactory _factory;

        private readonly IFileManager _file;

        public WriteMetCommandHandler(IRemsDbContextFactory context, IFileManager file)
        {
            _factory = context;
            _file = file;
        }

        public Task<(string, string, DateTime, DateTime)[]> Handle(WriteMetCommand request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private (string, string, DateTime, DateTime)[] Handler(WriteMetCommand request, CancellationToken token)
        {
            using var _context = _factory.Create();

            // LINQ EXPLANATION:
            // 1. Find the distinct met stations for each exported experiment
            // 2. Group continuous data for each station
            // 3. Write 1 .met file for each data group in each station
            // 4. Return metadata that lets an experiment find which file it needs to reference in
            //    its weather model
            var mets = request.ExperimentIds.Select(id => _context.Experiments.Find(id))
                .Select(e => e.MetStation)
                .Distinct()
                .SelectMany(m => m.MetData
                .GroupBy(d => d.Date)
                .OrderBy(d => d.Key)
                .Select((g, i) => new { data = g, key = g.Key.Subtract(TimeSpan.FromDays(i)) })
                .GroupBy(g => g.key, g => g.data)
                .Select(g => (m.Name, WriteFile(m, g), g.First().Key, g.Last().Key)));

            return mets.ToArray();
        }

        private string WriteFile(MetStation station, IEnumerable<IGrouping<DateTime, MetData>> datas)
        {
            var info = Directory.CreateDirectory(_file.ExportPath + "\\met");

            string start = "_" + datas.First().Key.ToString("MMM-yyyy");
            var name = station.Name.Replace('/', '-').Replace(' ', '_') + start + ".met";            

            using var stream = new FileStream(info.FullName + '\\' + name, FileMode.Create);
            using var writer = new StreamWriter(stream);            

            // Attach header lines to the file
            var builder = new StringBuilder();
            builder.AppendLine("[weather.met.weather]");
            builder.AppendLine($"!station name = {station.Name}");
            builder.AppendLine($"latitude = {station.Latitude ?? 0.0} (DECIMAL DEGREES)");
            builder.AppendLine($"longitude = {station.Longitude ?? 0.0} (DECIMAL DEGREES)");
            builder.AppendLine($"tav = {station.TemperatureAverage} (oC) \t ! annual average ambient temperature");
            builder.AppendLine($"amp = {station.Amplitude} (oC) \t ! annual amplitude in mean monthly temperature");
            builder.AppendLine();
            builder.AppendLine($"{"Year",-5}{"Day",3}{"maxt",7}{"mint",7}{"radn",6}{"rain",7}");
            builder.AppendLine($"{"()",-5}{"()",3}{"()",7}{"()",7}{"()",6}{"()",7}");

            // Find the weather traits
            using var _context = _factory.Create();
            Trait maxT = _context.GetTraitByName("MaxT");
            Trait minT = _context.GetTraitByName("MinT");
            Trait radn = _context.GetTraitByName("Radn");
            Trait rain = _context.GetTraitByName("Rain");

            // Format and add the data
            foreach (var data in datas)
            {
                var date = data.Key;

                builder.Append($"{date.Year,-5}");
                builder.Append($"{date.DayOfYear,3}");
                builder.Append($"{GetTraitValue(data, maxT),7:F1}");
                builder.Append($"{GetTraitValue(data, minT),7:F1}");
                builder.Append($"{GetTraitValue(data, radn),6:F1}");
                builder.AppendLine($"{GetTraitValue(data, rain),7:F1}");
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
            return "met\\" + name;
        }
    }
}
