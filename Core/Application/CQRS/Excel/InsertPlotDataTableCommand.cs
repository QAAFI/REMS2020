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
    /// Inserts a table of PlotData into the database
    /// </summary>
    public class InsertPlotDataTableCommand : ContextQuery<Unit>
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
        public class Handler : BaseHandler<InsertPlotDataTableCommand>
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

            // Converts all the trait values in a row to their own PlotData entities
            IEnumerable<PlotData> convertRow(DataRow row)
            {                
                var date = row.GetDate("Date");
                var sample = row.GetText("Sample");
                var exp = row.GetText("Experiment");                
                var text = row.GetText("Plot").ToLower();
                var plots = (text == "all" || text == "avg") ? exps[exp] 
                    : exps[exp].Where(p => text.Split(',')
                        .Select(i => Convert.ToInt32(i))
                        .Contains(p.Column.Value));

                foreach (var plot in plots)
                {
                    foreach (var trait in traits)
                    {
                        if (row[trait.Name] is DBNull || row[trait.Name] is "") continue;
                        
                        var value = row.GetDouble(trait.Name);

                        yield return new PlotData
                        {
                            PlotId = plot.PlotId,
                            TraitId = trait.TraitId,
                            Date = date,
                            Sample = sample,
                            Value = value,
                            UnitId = trait.UnitId
                        };
                    }
                }

                Progress.Increment(1);
            }

            // Convert all the rows of the table
            var datas = Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct();

            if (_context.PlotData.Any())
                datas = datas.Except(_context.PlotData, new PlotDataComparer());

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }        
    }

    internal class PlotDataComparer : IEqualityComparer<PlotData>
    {
        public bool Equals(PlotData x, PlotData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId;
        }

        public int GetHashCode(PlotData obj)
            => Utilities.GenerateHash(obj.Date.GetHashCode(), obj.TraitId, obj.PlotId);
    }
}
