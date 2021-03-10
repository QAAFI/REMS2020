using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find the names of all the tables in the database
    /// </summary>
    public class GetTableNamesQuery : IRequest<string[]> 
    { }

    public class GetTableNamesQueryHandler : IRequestHandler<GetTableNamesQuery, string[]>
    {
        private readonly IRemsDbContext _context;

        public GetTableNamesQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string[]> Handle(GetTableNamesQuery request, CancellationToken cancellationToken) 
            => Task.Run(() => _context.Names.ToArray());
    }
}
