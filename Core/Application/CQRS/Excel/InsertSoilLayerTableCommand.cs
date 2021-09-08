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

            var exps = _context.Experiments.ToDictionary(
                e => e.Name,
                e => e.Treatments.SelectMany(t => t.Plots).Distinct().ToArray()
            );

            IEnumerable<SoilLayerData> convertRow(DataRow row)
            {
                var exp = row.GetText("Experiment");
                var text = row.GetText("Plot").ToLower();
                var plots = (text == "all" || text == "avg")
                    ? exps[exp]
                    : exps[exp].Where(p => text.Split(',')
                        .Select(i => Convert.ToInt32(i))
                        .Contains(p.Column.Value));

                foreach (var plot in plots)
                {
                    // Convert all values from the 6th column onwards into SoilLayerData entities
                    foreach (var trait in traits)
                    {
                        var value = row[trait.Name];
                        if (value is DBNull || value is "") continue;
                                                
                        yield return new SoilLayerData
                        {
                            PlotId = plot.PlotId,
                            TraitId = trait.TraitId,
                            Date = row.GetDate("Date"),
                            DepthFrom = row.GetInt32("DepthFrom"),
                            DepthTo = row.GetInt32("DepthTo"),
                            Value = row.GetDouble(trait.Name)
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
