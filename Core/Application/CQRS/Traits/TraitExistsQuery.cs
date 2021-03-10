using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Check if a trait exists in the database
    /// </summary>
    public class TraitExistsQuery : IRequest<bool>
    {
        /// <summary>
        /// The trait name
        /// </summary>
        public string Name { get; set; }
    }

    public class TraitExistsQueryHandler : IRequestHandler<TraitExistsQuery, bool>
    {
        private readonly IRemsDbContext _context;

        public TraitExistsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(TraitExistsQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private bool Handler(TraitExistsQuery request, CancellationToken token)
        {
            if (_context.FileName is null)
                return false;

            return _context.Traits.Any(t => t.Name == request.Name);
        }
    }
}
