using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Inserts a table of soil layer data into the database
    /// </summary>
    public class InsertSoilLayerTableCommand : IRequest
    {
        /// <summary>
        /// A table of soil layer data.
        /// </summary>
        /// <remarks>
        /// The data within must map onto <see cref="SoilLayerData"/> entities.
        /// </remarks>
        public DataTable Table { get; set; }

        public int Skip { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// Alert the request sender that progress has been made on the command
        /// </summary>
        public Action IncrementProgress { get; set; }
    }

    public class InsertSoilLayerTableCommandHandler : IRequestHandler<InsertSoilLayerTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertSoilLayerTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertSoilLayerTableCommand request, CancellationToken token) => Task.Run(() => Handler(request));

        private Unit Handler(InsertSoilLayerTableCommand request)
        {
            var traits = _context.GetTraitsFromColumns(request.Table, request.Skip, request.Type);

            IEnumerable<SoilLayerData> convertRow(DataRow row)
            {
                // Find all plots in the treatment
                var plots = _context.Plots
                        .Where(p => p.Treatment.Experiment.Name == row[0].ToString());

                var cols = row[1].ToString().Split(',').Select(i => Convert.ToInt32(i));
                // If the treatment does not specify 'all' plots, filter based on the column
                if (row[1].ToString().ToLower() != "all")
                    plots = plots.Where(p => cols.Contains(p.Column.GetValueOrDefault()));

                request.IncrementProgress();

                foreach (var plot in plots)
                {
                    // Convert all values from the 6th column onwards into SoilLayerData entities
                    for (int i = 5; i < row.ItemArray.Length; i++)
                    {
                        if (row[i] is DBNull || row[i] is "") continue;

                        var value = Convert.ToDouble(row[i]);
                        yield return new SoilLayerData()
                        {
                            PlotId = plot.PlotId,
                            TraitId = traits[i - 5].TraitId,
                            Date = Convert.ToDateTime(row[2]),
                            DepthFrom = Convert.ToInt32(row[3]),
                            DepthTo = Convert.ToInt32(row[4]),
                            Value = value
                        };                        
                    }
                }
            }

            var datas = request.Table.Rows.Cast<DataRow>()
                .SelectMany(r => convertRow(r))
                .Distinct()
                .ToArray();

            if (_context.SoilLayerDatas.Any())
                datas = datas.Except(_context.SoilLayerDatas, new SoilLayerComparer()).ToArray();

            _context.AttachRange(datas.ToArray());
            _context.SaveChanges();

            return Unit.Value;
        }
    }

    /// <summary>
    /// Compares two <see cref="SoilLayerData"/> across their measured values
    /// </summary>
    internal class SoilLayerComparer : IEqualityComparer<SoilLayerData>
    {
        public bool Equals(SoilLayerData x, SoilLayerData y)
        {
            return x.Date == y.Date
                && x.TraitId == y.TraitId
                && x.PlotId == y.PlotId
                && x.DepthFrom == y.DepthFrom;
        }

        public int GetHashCode(SoilLayerData obj)
        {
            var a = obj.Date.GetHashCode();

            return a ^ obj.TraitId ^ obj.PlotId ^ obj.DepthFrom;
        }
    }
}
