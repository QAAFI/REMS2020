using MediatR;
using Microsoft.Data.Sqlite;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.DB.Queries
{
    public class GetDataTableQueryHandler : IRequestHandler<GetDataTableQuery, DataTable>
    {
        private readonly IRemsDbFactory _factory;

        public GetDataTableQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<DataTable> Handle(GetDataTableQuery request, CancellationToken cancellationToken)
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
