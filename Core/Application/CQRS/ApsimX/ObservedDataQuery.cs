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

        public string FileName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<ObservedDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataTable Run()
        {
            // Construct the table
            var table = new DataTable(FileName + "_Observed");
            table.Columns.Add(new DataColumn("SimulationName", typeof(string)));
            table.Columns.Add(new DataColumn("Date", typeof(DateTime)));

            // Find the traits
            var traits = _context.PlotData
                .Where(p => IDs.Contains(p.Plot.Treatment.ExperimentId))
                .Select(p => p.Trait)
                .Distinct();

            foreach (var trait in traits)
                table.Columns.Add(new DataColumn(trait.Name, typeof(double)));

            foreach (var id in IDs)
            {
                var exp = _context.Experiments.Find(id);

                foreach (var treat in exp.Treatments)
                {
                    var dates = treat.Plots
                        .SelectMany(p => p.PlotData)
                        .GroupBy(d => d.Date);

                    foreach (var date in dates)
                    {
                        var row = table.NewRow();

                        row["SimulationName"] = exp.Name + treat.Name;
                        row["Date"] = date.Key;

                        foreach (var trait in date.GroupBy(d => d.Trait))                        
                            row[trait.Key.Name] = trait.Average(d => d.Value);                        
                    }
                }
            }

            return table;
        }
    }
}
