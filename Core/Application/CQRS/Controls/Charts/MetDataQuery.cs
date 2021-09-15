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
    public class MetDataQuery : ContextQuery<DataTable>
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
        public class Handler : BaseHandler<MetDataQuery>
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

            var data = tmt.Experiment.MetStation.MetData
                .Where(p => p.Trait.Name == TraitName)
                .GroupBy(m => (m.Date.Day, m.Date.Month))
                .Select(g => (new DateTime(2020, g.Key.Month, g.Key.Day), g.Select(m => m.Value).Average()));

            // Construct the table
            var table = new DataTable($"{TreatmentId}_{TraitName}");            

            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Mean", typeof(double));

            // Populate the table
            foreach (var point in data)
            {
                var row = table.NewRow();
                row["Date"] = point.Item1;
                row["Mean"] = point.Item2;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
