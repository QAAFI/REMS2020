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

        public Action IncrementProgress { get; set; }

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

            IEnumerable<SoilData> convertRow(DataRow row)
            {
                // Find all plots in the treatment
                var plots = _context.Plots
                        .Where(p => p.Treatment.Experiment.Name == row[0].ToString());

                // The plot ids
                var ids = row[1].ToString().Split(',').Select(i => Convert.ToInt32(i));

                // If the treatment does not specify 'avg' plot data, filter based on the column
                if (row[1].ToString().ToLower() != "avg")
                    plots = plots.Where(p => ids.Contains(p.Column.GetValueOrDefault()));

                IncrementProgress();

                // For each plot specified in the PlotId column
                foreach (var plot in plots)
                {
                    // Convert all values from the 6th column onwards into SoilLayerData entities
                    for (int i = Skip; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var value = Convert.ToDouble(row[i]);
                        yield return new SoilData()
                        {
                            PlotId = plot.PlotId,
                            TraitId = traits[i - Skip].TraitId,
                            Date = Convert.ToDateTime(row[2]),
                            Value = value
                        };
                    }
                }
            }

            var datas = Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct()
                .ToArray();

            if (_context.SoilLayerDatas.Any())
                datas = datas.Except(_context.SoilDatas, new SoilDataComparer()).ToArray();

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
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.PlotId;
        }
    }
}
