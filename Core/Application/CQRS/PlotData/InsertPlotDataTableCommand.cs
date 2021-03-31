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

            var rows = Table.Rows.Cast<DataRow>();

            // Find the experiments
            var exps = rows.Select(r => r[0])
                .Distinct()
                .ToDictionary
                (
                    o => o.ToString(),
                    o => _context.Experiments.Single(e => e.Name == o.ToString())
                );

            // Search the context for a plot that matches the given criteria
            Plot findPlot(string key, int col)
            {
                var plot = _context.Plots
                .Where(p => p.Treatment.ExperimentId == exps[key].ExperimentId)
                .Where(p => p.Column == col)
                .FirstOrDefault();

                return plot;
            }

            // Find the plots
            var plots = rows.Select(r => new { key = r[0].ToString(), col = r[1] })
                .Distinct()
                .ToDictionary
                (
                    a => a,
                    a => findPlot(a.key, Convert.ToInt32(a.col))
                );

            // Converts all the trait values in a row to their own PlotData entities
            IEnumerable<PlotData> convertRow(DataRow row)
            {
                var date = Convert.ToDateTime(row[2]);
                var sample = row[3].ToString();

                IncrementProgress();

                for (int i = 4; i < row.ItemArray.Length; i++)
                {
                    if (row[i] is DBNull || row[i] is "") continue;

                    var trait = traits[i - 4];
                    var value = Convert.ToDouble(row[i]);

                    var x = new { key = row[0].ToString(), col = row[1] };

                    yield return new PlotData()
                    {
                        PlotId = plots[x].PlotId,
                        TraitId = trait.TraitId,
                        Date = date,
                        Sample = sample,
                        Value = value,
                        UnitId = trait.UnitId
                    };
                }
            }

            // Convert all the rows of the table
            var datas = rows.SelectMany(r => convertRow(r))
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
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.PlotId;
        }
    }
}
