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
    public class MetFileDataQuery : IRequest<DataSet>
    {
        /// <summary>
        /// The source experiment
        /// </summary>
        public int[] ExperimentIds { get; set; }
    }

    public class MetFileDataQueryHandler : IRequestHandler<MetFileDataQuery, DataSet>
    {
        private readonly IRemsDbContextFactory _factory;

        public MetFileDataQueryHandler(IRemsDbContextFactory context)
        {
            _factory = context;
        }

        public Task<DataSet> Handle(MetFileDataQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private DataSet Handler(MetFileDataQuery request, CancellationToken token)
        {
            using var _context = _factory.Create();

            var set = new DataSet("Met files");

            // Find the stations
            var stations = request.ExperimentIds.Select(id => _context.Experiments.Find(id))
                .Select(e => e.MetStation)
                .Distinct();

            // Create a table for each station
            foreach (var station in stations)
            {
                var table = new DataTable(station.Name);
                table.Columns.Add(new DataColumn("Year", typeof(int)));
                table.Columns.Add(new DataColumn("Day", typeof(int)));

                table.ExtendedProperties["Latitude"] = station.Latitude ?? 0.0;
                table.ExtendedProperties["Longitude"] = station.Longitude ?? 0.0;
                table.ExtendedProperties["TemperatureAverage"] = station.TemperatureAverage;
                table.ExtendedProperties["Amplitude"] = station.Amplitude;

                var dates = station.MetData.GroupBy(d => d.Date).OrderBy(g => g.Key);

                var traits = station.MetData.Select(m => m.Trait).Distinct();

                foreach (var trait in traits)
                    table.Columns.Add(new DataColumn(trait.Name, typeof(double)));

                foreach (var date in dates)
                {
                    var row = table.NewRow();
                    row["Year"] = date.Key.Year;
                    row["Day"] = date.Key.DayOfYear;

                    foreach (var data in date)
                        row[data.Trait.Name] = data.Value;

                    table.Rows.Add(row);
                }

                set.Tables.Add(table);
            }
            
            return set;
        }

    }
}
