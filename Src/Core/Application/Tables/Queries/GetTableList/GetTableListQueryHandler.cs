using MediatR;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries.GetTableList
{
    class GetTableListQueryHandler : IRequestHandler<GetTableListQuery, IEnumerable<string> >
    {
        private readonly IRemsDbFactory _factory;
        public GetTableListQueryHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<string> > Handle(GetTableListQuery request, CancellationToken cancellationToken)
        {
            if (_factory.Context == null) return null;

            return _factory.Context.Names;
        }
    }

}
