using System;
using System.Collections.Generic;
using System.Linq;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find the description for each of the given traits
    /// </summary>
    public class TraitDescriptionsQuery : ContextQuery<Dictionary<string, string>>
    {
        /// <summary>
        /// The traits to find descriptions for
        /// </summary>
        public IEnumerable<string> Traits { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<TraitDescriptionsQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> Run()
        {
            var result = new Dictionary<string, string>();

            foreach (var trait in Traits)
            {
                var text = _context.Traits.First(t => t.Name == trait).Description;
                result.Add(trait, text);
            }

            return result;
        }
    }
}
