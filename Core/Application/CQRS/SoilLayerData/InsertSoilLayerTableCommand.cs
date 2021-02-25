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

            SoilLayerData data;

            foreach (DataRow row in request.Table.Rows)
            {
                var all = _context.Plots
                        .Where(p => p.Treatment.Experiment.Name == row[0].ToString());

                if (row[1].ToString().ToLower() != "all")
                    all = all.Where(p => p.Column == Convert.ToInt32(row[1]));

                foreach (var plot in all)
                {
                    for (int i = 5; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var value = Convert.ToDouble(row[i]);
                        data = new SoilLayerData()
                        {
                            PlotId = plot.PlotId,
                            TraitId = traits[i - 5].TraitId,
                            Date = Convert.ToDateTime(row[2]),
                            DepthFrom = Convert.ToInt32(row[3]),
                            DepthTo = Convert.ToInt32(row[4]),
                            Value = value
                        };

                        Expression<Func<SoilLayerData, bool>> comparer = e =>
                            e.Date == data.Date
                            && e.TraitId == data.TraitId
                            && e.PlotId == data.PlotId
                            && e.DepthFrom == data.DepthFrom;

                        _context.InsertData(comparer, data, value);
                    }
                }
                request.IncrementProgress();
            }
            _context.SaveChanges();

            return Unit.Value;
        }

    }
}
