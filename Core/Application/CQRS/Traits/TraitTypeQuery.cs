using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TraitTypeQuery : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class TraitTypeQueryHandler : IRequestHandler<TraitTypeQuery, string>
    {
        private readonly IRemsDbContext _context;

        public TraitTypeQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string> Handle(TraitTypeQuery request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private string Handler(TraitTypeQuery request, CancellationToken token)
        {            
            return _context.Traits.FirstOrDefault(t => t.Name == request.Name).Type;
        }
    }
}
