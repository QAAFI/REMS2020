using MediatR;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Rems.Application.Common.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace Rems.Application.DB.Queries.GetDataTable
{
    public class GetDataTableQuery : IRequest<DataTable>
    {
        public string TableName { get; set; }

        public class Handler: IRequestHandler<GetDataTableQuery, DataTable>
        {
            private readonly IRemsDbFactory _factory;

            public Handler(IRemsDbFactory factory)
            {
                _factory = factory;
            }

            public async Task<DataTable> Handle(GetDataTableQuery request, CancellationToken cancellationToken)
            {
                if (_factory == null) return null;
                return _factory.getDataTable(request.TableName);
            }
        }
    }
}
