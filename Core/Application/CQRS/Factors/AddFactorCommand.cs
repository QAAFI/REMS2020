using System;
using System.Linq;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class AddFactorCommand : ContextQuery<Unit>
    { 
        /// <summary>
        /// The name of the factor to look for
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<AddFactorCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var factor = new Factor { Name = Name };
            _context.Add(factor);
            _context.SaveChanges();

            return Unit.Value;
        }
    }
}