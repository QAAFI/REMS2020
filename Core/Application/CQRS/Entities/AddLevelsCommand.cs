using System;
using System.Linq;
using MediatR;
using Models.Factorial;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class AddLevelsCommand : ContextQuery<Unit>
    {
        public Factor Factor { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<AddLevelsCommand>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Unit Run()
        {
            var factor = _context.Factors.First(f => f.Name == Factor.Name);

            foreach (var level in factor.Level)
                Factor.Children.Add(new CompositeFactor{ Name = level.Name });

            return Unit.Value;
        }
    }
}