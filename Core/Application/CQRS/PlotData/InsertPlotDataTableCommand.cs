using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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

            var plots = rows.Select(r => new { key = r[0], col = r[1] })
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
                        PlotId = plots[row[1]].PlotId,
                        TraitId = trait.TraitId,
                        Date = date,
                        Sample = sample,
                        Value = value,
                        UnitId = trait.UnitId
                    };

                    Expression<Func<PlotData, bool>> comparer = e =>
                            e.Date == data.Date
                            && e.TraitId == data.TraitId
                            && e.PlotId == data.PlotId;

                    _context.InsertData(comparer, data, value);
                }

                request.IncrementProgress();
            }

            _context.SaveChanges();
            return Unit.Value;
        }
    }
}
