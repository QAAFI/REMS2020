using MediatR;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries.GetTableList
{
    class GetTableListQueryHandler : IRequestHandler<GetTableListQuery, IEnumerable<string> >
    {
        private readonly IRemsDbContext _context;
        public GetTableListQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string> > Handle(GetTableListQuery request, CancellationToken cancellationToken)
        {
            if (_context == null) return null;

            return _context.Names;
        }
    }

}
