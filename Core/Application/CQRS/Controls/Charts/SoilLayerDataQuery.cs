using System;
using System.Data;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Finds the average value of a soil trait in a treatment for a given date
    /// </summary>
    public class SoilLayerDataQuery : ContextQuery<DataTable>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }

        public string TraitName { get; set; }

        /// <summary>
        /// The date
        /// </summary>
        public DateTime Date { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilLayerDataQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override DataTable Run()
        {
            var table = new DataTable($"{TreatmentId}_{TraitName}_{Date}");

            var tmt = _context.Treatments.Find(TreatmentId);

            // Add columns
            table.Columns.Add("Depth", typeof(int));
            foreach (var plot in tmt.Plots)
                table.Columns.Add(plot.Repetition.ToString(), typeof(double));
            table.Columns.Add("Mean", typeof(double));

            // Find the data
            var depths = _context.SoilLayerDatas
                .Where(p => p.Plot.TreatmentId == TreatmentId)
                .Where(p => p.Trait.Name == TraitName)                
                .Where(p => p.Date == Date)
                .ToArray()
                .GroupBy(p => p.DepthFrom)
                .OrderBy(g => g.Key)
                .ToArray();

            // Add rows
            foreach (var depth in depths)
            {
                var row = table.NewRow();
                row["Depth"] = depth.Key;

                foreach (var data in depth)
                    row[$"{data.Plot.Repetition}"] = data.Value;
                
                row["Mean"] = depth.Select(d => d.Value).Average();
            }            

            return table;
        }
    }
}
