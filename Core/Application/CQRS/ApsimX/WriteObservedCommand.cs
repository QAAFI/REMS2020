using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.IO;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Writes the met files for each weather station in the given experiments
    /// </summary>
    public class WriteObservedCommand : IRequest
    {
        public string FileName { get; set; }

        public int ID { get; set; }
    }

    public class WriteObservedCommandHandler : IRequestHandler<WriteObservedCommand>
    {
        private readonly IRemsDbContextFactory _factory;

        private readonly IFileManager _file;

        public WriteObservedCommandHandler(IRemsDbContextFactory context, IFileManager file)
        {
            _factory = context;
            _file = file;
        }

        public Task<MediatR.Unit> Handle(WriteObservedCommand request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private MediatR.Unit Handler(WriteObservedCommand request, CancellationToken token)
        {
            using var _context = _factory.Create();

            var info = Directory.CreateDirectory(_file.ExportPath + "\\obs");

            var exp = _context.Experiments.Find(request.ID);

            var data = _context.PlotData.Where(p => p.Plot.Treatment.ExperimentId == exp.ExperimentId);
            var traits = data.Select(p => p.Trait).Distinct().ToArray();

            using var stream = new FileStream(info.FullName + $"\\{exp.Name}_Observed.csv", FileMode.Create);
            using var writer = new StreamWriter(stream);

            var builder = new StringBuilder();
            builder.AppendLine($"Date, SimulationName, {string.Join(", ", traits.Select(t => t.Name))}");

            foreach (var day in data.AsEnumerable().GroupBy(d => d.Date))
            {
                foreach (var treat in exp.Treatments)
                {
                    var values = traits.Select(t => day.Where(d => d.Trait == t).Select(d => d.Value));
                    var averages = values.Select(e => e.Any() ? e.Average().ToString("F2") : "");

                    string date = day.Key.ToShortDateString();
                    string name = exp.Name + treat.Name;
                    builder.AppendLine($"{date}, {name}, {string.Join(", ", averages)}");
                }
            }

            writer.Write(builder.ToString());
            writer.Close();            

            return MediatR.Unit.Value;
        }
    }
}
