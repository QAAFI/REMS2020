using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a table of MetData into the database
    /// </summary>
    public class InsertMetDataTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source table
        /// </summary>
        public DataTable Table { get; set; }

        /// <summary>
        /// The number of skippable columns preceding trait columns in the table
        /// </summary>
        public int Skip { get; set; }

        public string Type { get; set; }

        public Action IncrementProgress { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertMetDataTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var traits = _context.GetTraitsFromColumns(Table, Skip, Type);

            IEnumerable<MetData> convertRow(DataRow row)
            {
                // Look for the station which sourced the data, create one if it isn't found
                var station = _context.MetStations.FirstOrDefault(e => e.Name == row[0].ToString());                

                for (int i = 2; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var trait = traits[i - 2];
                    var date = Convert.ToDateTime(row[1]);
                    var value = Convert.ToDouble(row[i]);

                    yield return new MetData
                    {
                        MetStationId = station?.MetStationId ?? 0,
                        TraitId = trait.TraitId,
                        Date = date,
                        Value = value
                    };
                }

                IncrementProgress();
            }

            var datas = Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.MetDatas.Any())
                datas = datas.Except(_context.MetDatas, new MetDataComparer());

            _context.AddRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
        
    }

    /// <summary>
    /// Compares MetData entities based on the values of some properties
    /// </summary>
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
