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
                    .Where(p => p.Trait == data.Trait)
                    .Where(p => p.Plot == data.Plot)
                    .Where(p => p.Date == data.Date);

                if (result.Any())
                    return result.Single();
                else
                    return null;
            }

            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            Experiment FindExperiment(string name) => _context.Experiments.Single(e => e.Name == name);

            // Group into experiments
            var exps = request.Table.Rows
                .Cast<DataRow>()
                .GroupBy(r => FindExperiment(r.ItemArray[0].ToString()));

            foreach (var exp in exps)
            {                
                // Group into plots
                var plts = exp.GroupBy(r => r.ItemArray[1]);

                foreach (var plt in plts)
                {
                    var col = Convert.ToInt32(plt.Key);

                    var plot = _context.Plots
                    .Where(p => p.Treatment.Experiment == exp.Key)
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
                            var value = Convert.ToDouble(row[i]);

                            var data = new PlotData()
                            {
                                Plot = plot,
                                Trait = trait,
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
                }
            }

            _context.SaveChanges();
            return Unit.Value;
        }
    }
}
