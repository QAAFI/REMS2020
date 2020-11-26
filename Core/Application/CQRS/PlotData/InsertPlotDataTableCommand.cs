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
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            // Group into experiments
            var exps = request.Table.Rows
                .Cast<DataRow>()
                .GroupBy(r => r.ItemArray[0]);

            foreach (var exp in exps)
            {
                var id = Convert.ToInt32(exp.Key);

                // Group into plots
                var plts = exp.GroupBy(r => r.ItemArray[1]);

                foreach (var plt in plts)
                {
                    var col = Convert.ToInt32(plt.Key);

                    var plot = _context.Plots
                    .Where(p => p.Treatment.ExperimentId == id)
                    .Where(p => p.Column == col)
                    .FirstOrDefault();

                    if (plot is null) continue;

                    // Add the data in each plot
                    foreach (var row in plt)
                    {
                        var date = Convert.ToDateTime(row[2]);
                        var sample = row[3].ToString();

                        for (int i = 4; i < row.ItemArray.Length; i++)
                        {
                            if (row[i] is DBNull || row[i] is "") continue;

                            var trait = traits[i - 4];

                            var data = new PlotData()
                            {
                                Plot = plot,
                                Trait = trait,
                                Date = date,
                                Sample = sample,
                                Value = Convert.ToDouble(row[i]),
                                UnitId = trait.UnitId
                            };
                            _context.Attach(data);
                        }

                        request.IncrementProgress();
                        //EventManager.InvokeProgressIncremented();
                    }
                }
            }

            _context.SaveChanges();
            return Unit.Value;
        }
    }
}
