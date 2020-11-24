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
    public class InsertSoilLayerTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }
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

            foreach (DataRow row in request.Table.Rows)
            {
                List<Plot> plots = new List<Plot>(); 

                var id = row[0].ConvertDBValue<int>();

                if (row[1].ToString() == "ALL")
                {
                    var all = _context.Plots
                        .Where(p => p.Treatment.ExperimentId == id);

                    plots.AddRange(all);
                }
                else
                {
                    var col = row[1].ConvertDBValue<int>();
                    var plot = _context.Plots
                        .Where(p => p.Treatment.ExperimentId == id)
                        .Where(p => p.Column == col)
                        .Single();

                    plots.Add(plot);
                }

                foreach (var plot in plots)
                {
                    for (int i = 5; i < row.ItemArray.Length; i++)
                    {
                        if (row.ItemArray[i] is DBNull) continue;

                        var data = new SoilLayerData()
                        {
                            Plot = plot,
                            Trait = traits[i - 5],
                            Date = row[2].ConvertDBValue<DateTime>(),
                            DepthFrom = row[3].ConvertDBValue<int>(),
                            DepthTo = row[4].ConvertDBValue<int>(),
                            Value = row[i].ConvertDBValue<double>()
                        };
                        _context.Attach(data);
                    }
                }
                EventManager.InvokeProgressIncremented();
            }
            _context.SaveChanges();

            return Unit.Value;
        }

    }
}
