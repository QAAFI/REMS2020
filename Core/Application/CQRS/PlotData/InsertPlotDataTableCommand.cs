using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
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
                    o => o.ToString(),
                    o => _context.Experiments.Single(e => e.Name == o.ToString())
                );

            Plot findPlot(string key, int col)
            {
                var plot = _context.Plots
                .Where(p => p.Treatment.ExperimentId == exps[key].ExperimentId)
                .Where(p => p.Column == col)
                .FirstOrDefault();

                return plot;
            }

            var plots = rows.Select(r => new { key = r[0].ToString(), col = r[1] })
                .Distinct()
                .ToDictionary
                (
                    a => a,
                    a => findPlot(a.key, Convert.ToInt32(a.col))
                );

            IEnumerable<PlotData> convertRow(DataRow row)
            {
                var date = Convert.ToDateTime(row[2]);
                var sample = row[3].ToString();

                request.IncrementProgress();

                for (int i = 4; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var trait = traits[i - 4];
                    var value = Convert.ToDouble(row[i]);

                    var x = new { key = row[0].ToString(), col = row[1] };

                    yield return new PlotData()
                    {
                        PlotId = plots[x].PlotId,
                        TraitId = trait.TraitId,
                        Date = date,
                        Sample = sample,
                        Value = value,
                        UnitId = trait.UnitId
                    };
                }
            }

            var datas = rows.SelectMany(r => convertRow(r))
                .Distinct();                
            
            if (_context.PlotData.Any())
                datas = datas.Except(_context.PlotData, new PlotDataComparer());

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }

    internal class PlotDataComparer : IEqualityComparer<PlotData>
    {
        public bool Equals(PlotData x, PlotData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId;
        }

        public int GetHashCode(PlotData obj)
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.PlotId;
        }
    }
}
