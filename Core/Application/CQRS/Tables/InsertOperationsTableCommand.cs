using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a table of operations data to the database
    /// </summary>
    public class InsertOperationsTableCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The source data
        /// </summary>
        public DataTable Table { get; set; }

        public Type Type { get; set; }

        public Action IncrementProgress { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<InsertOperationsTableCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            // Find the properties of each columnm assuming the first two columns do not contain property data
            var infos = Table.Columns.Cast<DataColumn>()
                    .Skip(2)
                    .Select(c => c.FindProperty())
                    .Where(i => i != null)
                    .ToList();

            var info = Type.GetProperty("TreatmentId");

            // Insert a treatment to the database
            void insertTreatment(DataRow row, Treatment treatment)
            {
                var result = row.ToEntity(_context, Type, infos.ToArray());
                info.SetValue(result, treatment.TreatmentId);
                _context.Attach(result);
            }

            var entities = new List<IEntity>();

            foreach (DataRow row in Table.Rows)
            {
                // Assume that the second column is the treatment name
                var name = row[1].ToString();

                var treatments = _context.Treatments.Where(t => t.Experiment.Name == row[0].ToString()).AsNoTracking();

                if (name.ToLower() == "all")
                {
                    foreach (var treatment in treatments)
                        insertTreatment(row, treatment);
                }
                else
                {
                    var treatment = treatments.Where(t => t.Name == name).FirstOrDefault();

                    // TODO: This is a lazy approach that simply skips bad data, try to find a better solution
                    if (treatment is null) continue;

                    insertTreatment(row, treatment);
                }

                IncrementProgress();
            }

            _context.SaveChanges();

            return Unit.Value;            
        }
    }
}
