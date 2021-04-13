using System;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Insert a trait to the database
    /// </summary>
    public class AddTraitCommand : ContextQuery<Unit>
    {
        /// <summary>
        /// The trait name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The trait type
        /// </summary>
        public string Type { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<AddTraitCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var trait = new Trait()
            {
                Name = Name,
                Type = Type
            };

            _context.Add(trait);
            _context.SaveChanges();

            return Unit.Value;            
        }
    }
}
