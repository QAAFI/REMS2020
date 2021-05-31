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

        public Action IncrementProgress { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertPlotDataTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var traits = _context.GetTraitsFromColumns(Table, Skip, Type);

            // Converts all the trait values in a row to their own PlotData entities
            IEnumerable<PlotData> convertRow(DataRow row)
            {
                var exp = _context.Experiments.FirstOrDefault(e => e.Name == row[0].ToString());
                var date = Convert.ToDateTime(row[2]);
                var sample = row[3].ToString();

                foreach (var plot in _context.FindPlots(row[1], exp))
                {
                    for (int i = Skip; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var trait = traits[i - Skip];
                        var value = Convert.ToDouble(row[i]);

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

                IncrementProgress();
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
