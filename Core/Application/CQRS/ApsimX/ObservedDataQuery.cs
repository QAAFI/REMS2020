using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Data;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Writes the met files for each weather station in the given experiments
    /// </summary>
    public class ObservedDataQuery : ContextQuery<DataTable>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int[] IDs { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ObservedDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataTable Run()
        {
            // Construct the table
            var table = new DataTable("Observed");
            table.Columns.Add(new DataColumn("SimulationName", typeof(string)));
            table.Columns.Add(new DataColumn("Date", typeof(DateTime)));

            // Find the data
            var data = _context.PlotData.Where(p => IDs.Contains(p.Plot.Treatment.ExperimentId))
                .OrderBy(p => p.Date);

            // Setup the columns
            var traits = data.Select(p => p.Trait).Distinct();
            foreach (var trait in traits)
                table.Columns.Add(new DataColumn(trait.Name, typeof(double)));

            foreach (var id in IDs)
            {
                var exp = _context.Experiments.Find(id);

                foreach (var day in data.AsEnumerable().GroupBy(d => d.Date))
                {
                    var date = day.Key;
                    foreach (var treat in exp.Treatments)
                    {
                        var row = table.NewRow();

                        row["SimulationName"] = exp.Name + treat.Name;
                        row["Date"] = date;

                        foreach (var trait in traits)
                        {
                            var values = day.Where(d => d.Trait == trait)
                                .Select(d => d.Value);

                            row[trait.Name] = values.Any() ? Math.Round(values.Average(), 2) : DBNull.Value;
                        }

                        table.Rows.Add(row);
                    }
                }
            }

            return table;
        }
    }
}
