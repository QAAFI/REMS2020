using MediatR;
using Rems.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Tables.Queries
{
    public class GetTableListQueryHandler : IRequestHandler<GetTableListQuery, IEnumerable<string> >
    {
        private readonly IRemsDbContext _context;

        public GetTableListQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<string> > Handle(GetTableListQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                return _context.Names;
            });
        }
    }

}
