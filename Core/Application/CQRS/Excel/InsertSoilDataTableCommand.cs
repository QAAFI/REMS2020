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
    /// Inserts a table of SoilData into the database
    /// </summary>
    public class InsertSoilDataTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        /// <summary>
        /// The number of skippable columns preceding trait columns in the table
        /// </summary>
        public int Skip { get; set; }

        public string Type { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertSoilDataTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            if (Skip < 0)
                throw new Exception("You cannot skip a negative number of columns");

            var traits = _context.GetTraitsFromColumns(Table, Skip, Type);
            var exps = _context.Experiments.ToDictionary(
                e => e.Name,
                e => e.Treatments.SelectMany(t => t.Plots).Distinct().ToArray()
            );

            IEnumerable<SoilData> convertRow(DataRow row)
            {
                var exp = row.GetText("Experiment");
                var text = row.GetText("Plot").ToLower();
                var plots = (text == "all" || text == "avg")
                    ? exps[exp] : exps[exp].Where(p => text.Split(',')
                        .Select(i => Convert.ToInt32(i))
                        .Contains(p.Column.Value));

                // For each plot specified in the PlotId column
                foreach (var plot in plots)
                {
                    // Convert all values from the 6th column onwards into SoilLayerData entities
                    foreach (var trait in traits)
                    {
                        var value = row[trait.Name];
                        if (value is DBNull || value is "" || value is "?") 
                            continue;
                                                
                        yield return new SoilData()
                        {
                            PlotId = plot.PlotId,
                            TraitId = trait.TraitId,
                            Date = row.GetDate("Date"),
                            Value = row.GetDouble(trait.Name)
                        };
                    }
                }

                Progress.Report(1);
            }

            var datas = Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.SoilLayerDatas.Any())
                datas = datas.Except(_context.SoilDatas, new SoilDataComparer());

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
        
    }

    internal class SoilDataComparer : IEqualityComparer<SoilData>
    {
        public bool Equals(SoilData x, SoilData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId;
        }

        public int GetHashCode(SoilData obj)
            => Utilities.GenerateHash(obj.Date.GetHashCode(), obj.TraitId, obj.PlotId);
    }
}
