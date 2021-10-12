using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using MediatR;
using Rems.Application.Common.Interfaces;
using Unit = MediatR.Unit;
using Rems.Domain.Entities;
using System.Collections.Generic;

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

        public Task<Unit> Handle(WriteObservedCommand request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Unit Handler(WriteObservedCommand request, CancellationToken token)
        {
            var info = Directory.CreateDirectory(_file.ExportPath + "\\obs");

            using var stream = new FileStream(info.FullName + $"\\{request.FileName}_Observed.csv", FileMode.Create);
            using var writer = new StreamWriter(stream);

            var builder = new StringBuilder();

            using var _context = _factory.Create();
            var data = _context.PlotData.Where(p => request.IDs.Contains(p.Plot.Treatment.ExperimentId))
                .OrderBy(p => p.Date);
            var traits = data.Select(p => p.Trait).Distinct().ToArray();

            builder.AppendLine($"SimulationName,Date,{string.Join(",", traits.Select(t => t.Name))}");

            foreach (int id in request.IDs)
                WriteExperimentData(_context.Experiments.Find(id), data.AsEnumerable(), traits, builder);

            writer.Write(builder.ToString());
            writer.Close();

            return Unit.Value;
        }

        private void WriteExperimentData(Experiment exp, IEnumerable<PlotData> data, Trait[] traits, StringBuilder builder)
        {
            foreach (var day in data.GroupBy(d => d.Date))
            {
                foreach (var treat in exp.Treatments)
                {
                    var values = traits.Select(t => day.Where(d => d.Trait == t).Select(d => d.Value));
                    var averages = values.Select(e => e.Any() ? e.Average().ToString("F2") : "");

                    string date = day.Key.ToShortDateString();
                    string name = exp.Name + treat.Name;
                    builder.AppendLine($"{name},{date},{string.Join(",", averages)}");
                }
            }
        }
    }
}
