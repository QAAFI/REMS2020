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

        public int[] IDs { get; set; }
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

            using var stream = new FileStream(info.FullName + $"\\{request.FileName}HarvestData.csv", FileMode.Create);
            using var writer = new StreamWriter(stream);

            var builder = new StringBuilder();

            var traits = _context.PlotData.Select(p => p.Trait).Distinct().ToArray();

            builder.AppendLine($"Date, SimulationName, {string.Join(", ", traits.Select(t => t.Name))}");

            foreach (var date in _context.PlotData.AsEnumerable().GroupBy(p => p.Date))
            {
                var treatments = date.GroupBy(d => d.Plot.Treatment)
                    .Where(g => request.IDs.Contains(g.Key.ExperimentId));

                foreach (var treat in treatments)
                {   
                    var values = traits.Select(t => treat.Where(p => p.Trait == t))
                        .Select(e => e.Any() ? e.Select(e => e.Value).Average().ToString("F2") : "");

                    string day = date.Key.ToShortDateString();
                    string name = treat.Key.Experiment.Name + treat.Key.Name;
                    builder.AppendLine($"{day}, {name}, {string.Join(", ", values)}");
                }
            }

            writer.Write(builder.ToString());
            writer.Close();
            return MediatR.Unit.Value;
        }
    }
}
