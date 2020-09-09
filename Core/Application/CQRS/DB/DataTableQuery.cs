using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Microsoft.Data.Sqlite;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class DataTableQuery : IRequest<DataTable>
    {
        public string TableName { get; set; }        
    }

    public class DataTableQueryHandler : IRequestHandler<DataTableQuery, DataTable>
    {
        private readonly IRemsDbFactory _factory;

        public DataTableQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<DataTable> Handle(DataTableQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() => GetDataTable(request.TableName));
        }

        private DataTable GetDataTable(string name)
        {
            string text = $"SELECT * FROM {name}";
            DataTable table = null;

            using (var connection = new SqliteConnection("Data Source=" + _factory.Connection))
            {
                connection.Open();
                using (var command = new SqliteCommand(text, connection))
                using (var reader = command.ExecuteReader())
                {
                    table = new DataTable(name);
                    table.BeginLoadData();
                    table.Load(reader);
                    table.EndLoadData();
                }
            };

            return table;
        }
    }
}
