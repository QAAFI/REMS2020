using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Domain.Entities;
using Rems.Application.Common.Interfaces;
using System.Linq;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlotDataByTraitQueryHandler : IRequestHandler<PlotDataByTraitQuery, DataTable>
    {
        private readonly IRemsDbFactory factory;

        public PlotDataByTraitQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<DataTable> Handle(PlotDataByTraitQuery request, CancellationToken token)
        {
            var data = factory.Context.PlotData
                .Where(p => p.Plot.PlotId == request.PlotId)
                .Where(p => p.Trait.Name == request.TraitName)
                .OrderBy(p => p.Date);

            var table = new DataTable(request.TraitName + " plot data");
            table.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Value", typeof(double)));

            foreach (var item in data)
            {
                var row = table.NewRow();
                row["Date"] = item.Date;
                row["Value"] = item.Value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
