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

            IEnumerable<MetData> convertRow(DataRow row)
            {
                // Look for the station which sourced the data, create one if it isn't found
                var station = _context.MetStations.FirstOrDefault(e => e.Name == row[0].ToString());

                request.IncrementProgress();

                for (int i = 2; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var trait = traits[i - 2];
                    var date = Convert.ToDateTime(row[1]);
                    var value = Convert.ToDouble(row[i]);

                    yield return new MetData
                    {
                        MetStationId = station?.MetStationId ?? 0,
                        Trait = trait,
                        Date = date,
                        Value = value
                    };
                }
            }

            var datas = request.Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.MetDatas.Any())
                datas = datas.Except(_context.MetDatas);

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }

    internal class MetDataComparer : IEqualityComparer<MetData>
    {
        public bool Equals(MetData x, MetData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.MetStationId == y.MetStationId;
        }

        public int GetHashCode(MetData obj)
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.MetStationId;
        }
    }
}
