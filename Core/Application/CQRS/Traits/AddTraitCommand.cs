using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    public class AddTraitCommand : IRequest<bool>
    {
        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class AddTraitCommandHandler : IRequestHandler<AddTraitCommand, bool>
    {
        private readonly IRemsDbContext _context;

        public AddTraitCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(AddTraitCommand request, CancellationToken token) 
            => Task.Run(() => Handler(request, token));

        private bool Handler(AddTraitCommand request, CancellationToken token)
        {
            try
            {
                var trait = new Trait()
                {
                    Name = request.Name,
                    Type = request.Type
                };

                _context.Add(trait);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
