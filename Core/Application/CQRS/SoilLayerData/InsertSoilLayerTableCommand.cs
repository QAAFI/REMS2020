using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Inserts a table of soil layer data into the database
    /// </summary>
    public class InsertSoilLayerTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// A table of soil layer data.
        /// </summary>
        /// <remarks>
        /// The data within must map onto <see cref="SoilLayerData"/> entities.
        /// </remarks>
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertSoilLayerTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var traits = _context.GetTraitsFromColumns(Table, Skip, Type);

            IEnumerable<SoilLayerData> convertRow(DataRow row)
            {
                var exp = _context.Experiments.FirstOrDefault(e => e.Name == row[0].ToString());

                foreach (var plot in _context.FindPlots(row[1], exp))
                {
                    // Convert all values from the 6th column onwards into SoilLayerData entities
                    for (int i = Skip; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var value = Convert.ToDouble(row[i]);
                        
                        yield return new SoilLayerData
                        {
                            PlotId = plot.PlotId,
                            TraitId = traits[i - Skip].TraitId,
                            Date = Convert.ToDateTime(row[2]),
                            DepthFrom = Convert.ToInt32(row[3]),
                            DepthTo = Convert.ToInt32(row[4]),
                            Value = value
                        };
                    }
                }

                Progress.Increment(1);
            }

            var datas = Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.SoilLayerDatas.Any())
                datas = datas.Except(_context.SoilLayerDatas, new SoilLayerComparer());

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
        
    }

    /// <summary>
    /// Compares two <see cref="SoilLayerData"/> across their measured values
    /// </summary>
    internal class SoilLayerComparer : IEqualityComparer<SoilLayerData>
    {
        public bool Equals(SoilLayerData x, SoilLayerData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId
                && x.DepthFrom == y.DepthFrom;
        }

        public int GetHashCode(SoilLayerData obj)
            => Utilities.GenerateHash(obj.Date.GetHashCode(), obj.TraitId, obj.PlotId, obj.DepthFrom);
    }
}
