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
    public class InsertSoilLayerTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertSoilLayerTableCommandHandler : IRequestHandler<InsertSoilLayerTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertSoilLayerTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertSoilLayerTableCommand request, CancellationToken token) => Task.Run(() => Handler(request));

        private Unit Handler(InsertSoilLayerTableCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            IEnumerable<SoilLayerData> convertRow(DataRow row)
            {
                var all = _context.Plots
                        .Where(p => p.Treatment.Experiment.Name == row[0].ToString());

                if (row[1].ToString().ToLower() != "all")
                    all = all.Where(p => p.Column == Convert.ToInt32(row[1]));

                request.IncrementProgress();

                foreach (var plot in all)
                {
                    for (int i = 5; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var value = Convert.ToDouble(row[i]);
                        yield return new SoilLayerData()
                        {
                            PlotId = plot.PlotId,
                            TraitId = traits[i - 5].TraitId,
                            Date = Convert.ToDateTime(row[2]),
                            DepthFrom = Convert.ToInt32(row[3]),
                            DepthTo = Convert.ToInt32(row[4]),
                            Value = value
                        };                        
                    }
                }
            }

            var datas = request.Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.SoilLayerDatas.Any())
                datas = datas.Except(_context.SoilLayerDatas, new SoilLayerComparer());

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }

    internal class SoilLayerComparer : IEqualityComparer<SoilLayerData>
    {
        public bool Equals(SoilLayerData x, SoilLayerData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId
                && x.DepthFrom == y.DepthFrom;
        }

        public int GetHashCode(SoilLayerData obj)
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.PlotId ^ obj.DepthFrom;
        }
    }
}
