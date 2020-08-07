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
    public class MeanTreatmentDataByTraitQueryHandler : IRequestHandler<MeanTreatmentDataByTraitQuery, DataTable>
    {
        private readonly IRemsDbFactory factory;

        public MeanTreatmentDataByTraitQueryHandler(IRemsDbFactory _factory)
        {
            factory = _factory;
        }

        public async Task<DataTable> Handle(MeanTreatmentDataByTraitQuery request, CancellationToken token)
        {
            var data = factory.Context.PlotData
                .Where(p => p.Plot.TreatmentId == request.TreatmentId)
                .Where(p => p.Trait.Name == request.TraitName)
                .ToArray() // Have to cast to an array to support the following GroupBy
                .GroupBy(p => p.Date)
                .OrderBy(g => g.Key);

            var table = new DataTable(request.TraitName + " plot data");
            table.Columns.Add(new DataColumn("Date", typeof(DateTime)));
            table.Columns.Add(new DataColumn("Value", typeof(double)));

            foreach (var item in data)
            {
                var value = item.Average(p => p.Value);

                var row = table.NewRow();
                row["Date"] = item.Key;
                row["Value"] = value;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
