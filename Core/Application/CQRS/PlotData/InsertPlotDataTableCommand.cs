using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertPlotDataTableCommand : IRequest<Unit>
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }
    }

    public class InsertPlotDataTableCommandHandler : IRequestHandler<InsertPlotDataTableCommand, Unit>
    {
        private readonly IRemsDbContext _context;

        public InsertPlotDataTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertPlotDataTableCommand request, CancellationToken token) => Task.Run(() => Handler(request));

        private Unit Handler(InsertPlotDataTableCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            foreach (DataRow row in request.Table.Rows)
            {
                // Assume that the first column is the experiment ID
                var id = row[0].ConvertDBValue<int>();

                //  Assume the second column is the plot column
                var col = row[1].ConvertDBValue<int>();

                var plot = _context.Plots
                    .Where(p => p.Treatment.ExperimentId == id)
                    .Where(p => p.Column == col)
                    .FirstOrDefault();

                if (plot is null) continue;

                for (int i = 4; i < row.ItemArray.Length; i++)
                {
                    if (row.ItemArray[i] is DBNull) continue;

                    var trait = traits[i - 4];

                    var data = new PlotData()
                    {
                        PlotId = plot.PlotId,
                        TraitId = trait.TraitId,
                        Date = row[2].ConvertDBValue<DateTime>(),
                        Sample = row[3].ToString(),
                        Value = row[i].ConvertDBValue<double>(),
                        UnitId = trait.UnitId
                    };
                    _context.Add(data);
                }

                EventManager.InvokeProgressIncremented(null, EventArgs.Empty);
            }
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
