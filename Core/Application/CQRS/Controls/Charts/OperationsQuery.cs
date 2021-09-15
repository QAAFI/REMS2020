using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on fertilization operations for a treatment
    /// </summary>
    public class OperationsQuery : ContextQuery<DataSet>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<OperationsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataSet Run()
        {
            var treatment = _context.Treatments.Find(TreatmentId);

            var f = treatment.Fertilizations.Select(f => (f.Date, f.Amount));
            var i = treatment.Irrigations.Select(i => (i.Date, i.Amount));
            var t = treatment.Tillages.Select(t => (t.Date, t.Depth));

            var set = new DataSet("Operations");
            set.Tables.Add(ToTable(f, "Fertilizations", "Amount"));
            set.Tables.Add(ToTable(i, "Irrigations", "Amount"));
            set.Tables.Add(ToTable(t, "Tillages", "Depth"));
            
            return set;
        }

        private DataTable ToTable(IEnumerable<(DateTime, double)> data, string name, string subname)
        {
            var table = new DataTable(name);
            table.ExtendedProperties["Subname"] = subname;

            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Value", typeof(double));

            foreach (var point in data)
            {
                var row = table.NewRow();
                row["Date"] = point.Item1;
                row["Value"] = point.Item2;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
