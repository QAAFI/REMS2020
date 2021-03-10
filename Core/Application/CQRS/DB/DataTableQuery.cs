using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Microsoft.Data.Sqlite;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Accesses a table in the database by its name
    /// </summary>
    public class DataTableQuery : IRequest<DataTable>
    {
        /// <summary>
        /// The table name
        /// </summary>
        public string TableName { get; set; }
    }

    public class DataTableQueryHandler : IRequestHandler<DataTableQuery, DataTable>
    {
        private readonly IRemsDbFactory _factory;

        public DataTableQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<DataTable> Handle(DataTableQuery request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private DataTable Handler(DataTableQuery request)
        {
            string text = $"SELECT * FROM {request.TableName}";
            DataTable table = null;

            using (var connection = new SqliteConnection("Data Source=" + _factory.Connection))
            {
                connection.Open();
                using (var command = new SqliteCommand(text, connection))
                using (var reader = command.ExecuteReader())
                {
                    table = new DataTable(request.TableName);
                    table.BeginLoadData();
                    table.Load(reader);
                    table.EndLoadData();
                }
            };

            return table;
        }
    }
}
