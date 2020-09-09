using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertMetDataTableCommandHandler : IRequestHandler<InsertMetDataTableCommand, Unit>
    {
        private readonly IRemsDbContext _context;

        public InsertMetDataTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(InsertMetDataTableCommand request, CancellationToken token)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            foreach (DataRow row in request.Table.Rows)
            {
                // Look for the station which sourced the data, create one if it isn't found
                var station = _context.MetStations.FirstOrDefault(e => e.Name == row[0].ToString());
                if (station is null) continue;

                for (int i = 2; i < row.ItemArray.Length; i++)
                {
                    if (row.ItemArray[i] is DBNull) continue;

                    var data = new MetData()
                    {
                        MetStationId = station.MetStationId,
                        Trait = traits[i - 2],
                        Date = row[1].ConvertDBValue<DateTime>(),
                        Value = row[i].ConvertDBValue<double>()
                    };

                    _context.Attach(data);
                }
            }
            _context.SaveChanges();

            return Unit.Value;
        }

    }
}
