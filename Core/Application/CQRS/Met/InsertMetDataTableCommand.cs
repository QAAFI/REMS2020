﻿using System;
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
    public class InsertMetDataTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }

        public Action IncrementProgress { get; set; }
    }

    public class InsertMetDataTableCommandHandler : IRequestHandler<InsertMetDataTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertMetDataTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertMetDataTableCommand request, CancellationToken token) => Task.Run(() => Handler(request));
        
        private Unit Handler(InsertMetDataTableCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            foreach (DataRow row in request.Table.Rows)
            {
                // Look for the station which sourced the data, create one if it isn't found
                var station = _context.MetStations.FirstOrDefault(e => e.Name == row[0].ToString());
                if (station is null) continue;

                for (int i = 2; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var data = new MetData()
                    {
                        MetStationId = station.MetStationId,
                        Trait = traits[i - 2],
                        Date = Convert.ToDateTime(row[1]),
                        Value = Convert.ToDouble(row[i])
                    };                    

                    _context.Attach(data);
                }

                request.IncrementProgress();
            }
            _context.SaveChanges();

            return Unit.Value;
        }

    }
}
