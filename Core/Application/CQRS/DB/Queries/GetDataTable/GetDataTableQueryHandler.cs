using MediatR;

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

        public async Task<DataTable> Handle(GetDataTableQuery request, CancellationToken cancellationToken)
        {
            if (_factory == null) return null;
            return _factory.getDataTable(request.TableName);
        }
    }
}
