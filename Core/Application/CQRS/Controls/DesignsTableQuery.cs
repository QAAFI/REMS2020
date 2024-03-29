﻿using System;
using System.Data;
using System.Linq;

using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Searches the database for information to populate an experiment design table
    /// </summary>
    public class DesignsTableQuery : ContextQuery<DataTable>
    {
        /// <summary>
        /// The experiment to find design data for
        /// </summary>
        public int ExperimentId { get; set; }
        public class Handler : BaseHandler<DesignsTableQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        protected override DataTable Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            var table = new DataTable("Designs");

            var names = exp.Treatments
                .SelectMany(f => f.Designs)
                .Select(d => d.Level.Factor.Name)
                .Distinct();

            var type = typeof(string);

            var columns = names.Select(n => new DataColumn(n, type)).ToArray();

            table.Columns.Add("Name");
            table.Columns.AddRange(columns);

            foreach (var treatment in exp.Treatments)
            {
                var row = table.NewRow();
                row["Name"] = treatment.Name;
                foreach (var design in treatment.Designs) 
                    row[design.Level.Factor.Name] = design.Level.Name;

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
