using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average values for a trait across a treatment
    /// </summary>
    public class TraitDataQuery : ContextQuery<DataTable>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TraitDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataTable Run()
        {   
            // Check sensibility
            if (_context.Traits.FirstOrDefault(t => t.Name == TraitName) is not Trait trait)
                return null;
            
            // Find the data
            var tmt = _context.Treatments.Find(TreatmentId);

            IEnumerable<(DateTime Date, double Value, int Rep)> arr = trait.Type switch
            {
                "Soil" => tmt.Plots.SelectMany(p => p.SoilData
                        .Where(d => d.Trait.Name == TraitName)
                        .Select(d => (d.Date, d.Value, p.Repetition))),

                _ => tmt.Plots.SelectMany(p => p.PlotData
                        .Where(d => d.Trait.Name == TraitName)
                        .Select(d => (d.Date, d.Value, p.Repetition)))
            };

            // Construct the table
            var table = new DataTable($"{TreatmentId}_{TraitName}");

            table.Columns.Add("Date", typeof(DateTime));
            foreach (var plot in tmt.Plots)
                table.Columns.Add(plot.Repetition.ToString(), typeof(double));
            table.Columns.Add("Mean", typeof(double));

            // Populate the table
            var dates = arr.GroupBy(p => p.Date).OrderBy(g => g.Key);
            foreach (var date in dates)
            {
                var row = table.NewRow();
                row["Date"] = date.Key;

                foreach (var (Date, Value, Rep) in date)                
                    row[Rep.ToString()] = Value;

                row["Mean"] = date.Select(d => d.Value).Average();

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
