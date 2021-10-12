using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the plots in a treatment, represented by the pairing of their ID and Name
    /// </summary>
    public class PlotsRepsQuery : ContextQuery<DataTable>
    {
        public int ExperimentId { get; set; }

        /// <summary>
        /// The source treatment
        /// </summary>
        public string TreatmentName { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<PlotsRepsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataTable Run()
        {
            var plots = _context.Plots
                .Where(p => p.Treatment.Name == TreatmentName)
                .Where(p => p.Treatment.ExperimentId == ExperimentId);

            var table = new DataTable("Plots / Reps");

            table.Columns.Add("Plot");
            table.Columns.Add("Repetition");

            foreach (var plot in plots)
            {
                var row = table.NewRow();
                row["Plot"] = plot.Column;
                row["Repetition"] = plot.Repetition;                
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
