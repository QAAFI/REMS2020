using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertPlotDataTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertPlotDataTableCommandHandler : IRequestHandler<InsertPlotDataTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertPlotDataTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertPlotDataTableCommand request, CancellationToken token) 
            => Task.Run(() => Handler(request));

        private Unit Handler(InsertPlotDataTableCommand request)
        {
            PlotData findMatch (PlotData data)
            {
                var result = _context.PlotData
                    .Where(p => p.TraitId == data.TraitId)
                    .Where(p => p.PlotId == data.PlotId)
                    .Where(p => p.Date == data.Date);

                if (result.Any())
                    return result.Single();
                else
                    return null;
            }

            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            var rows = request.Table.Rows.Cast<DataRow>();

            var exps = rows.Select(r => r[0])
                .Distinct()
                .ToDictionary
                (
                    o => o,
                    o => _context.Experiments.Single(e => e.Name == o.ToString())
                );

            Plot findPlot(object key, int col)
            {
                var plot = _context.Plots
                .Where(p => p.Treatment.ExperimentId == exps[key].ExperimentId)
                .Where(p => p.Column == col)
                .FirstOrDefault();

                return plot;
            }

            var plts = rows.Select(r => new { key = r[0], col = r[1] })
                .Distinct()
                .ToDictionary
                (
                    a => a.col,
                    a => findPlot(a.key, Convert.ToInt32(a.col))
                );

            foreach (var row in rows)
            {
                var date = Convert.ToDateTime(row[2]);
                var sample = row[3].ToString();

                for (int i = 4; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var trait = traits[i - 4];
                    var value = Convert.ToDouble(row[i]);

                    var data = new PlotData()
                    {
                        Plot = plts[row[1]],
                        TraitId = trait.TraitId,
                        Date = date,
                        Sample = sample,
                        Value = value,
                        UnitId = trait.UnitId
                    };

                    if (findMatch(data) is PlotData pd)
                        pd.Value = value;
                    else
                        _context.Attach(data);
                }

                request.IncrementProgress();
            }

            _context.SaveChanges();
            return Unit.Value;
        }
    }
}
