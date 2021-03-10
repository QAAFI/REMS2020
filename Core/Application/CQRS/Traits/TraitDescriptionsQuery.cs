using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find the description for each of the given traits
    /// </summary>
    public class TraitDescriptionsQuery : IRequest<Dictionary<string, string>>
    {
        /// <summary>
        /// The traits to find descriptions for
        /// </summary>
        public IEnumerable<string> Traits { get; set; }
    }

    public class TraitDescriptionsQueryHandler : IRequestHandler<TraitDescriptionsQuery, Dictionary<string, string>>
    {
        private readonly IRemsDbContext _context;

        public TraitDescriptionsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Dictionary<string, string>> Handle(TraitDescriptionsQuery request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Dictionary<string, string> Handler(TraitDescriptionsQuery request, CancellationToken token)
        {
            var result = new Dictionary<string, string>();

            foreach (var trait in request.Traits)
            {
                var text = _context.Traits.First(t => t.Name == trait).Description;
                result.Add(trait, text);
            }

            return result;
        }
    }
}
