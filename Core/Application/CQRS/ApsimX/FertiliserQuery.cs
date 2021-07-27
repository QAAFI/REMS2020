using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM clock model for an experiment
    /// </summary>
    public class FertiliserQuery : ContextQuery<Fertiliser>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<FertiliserQuery> 
        { 
            public Handler(IRemsDbContextFactory factory) : base(factory) { } 
        }

        /// <inheritdoc/>
        protected override Fertiliser Run()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            var types = _context.Fertilizers.Select(f => new FertiliserType
            {
                Name = f.Name,
                Description = f.Notes,
                FractionCa = f.Calcium.GetValueOrDefault()                
            }).ToList();

            // Ensure the default type exists
            if (!types.Any(f => f.Name == "NO3N"))
                types.Add(new FertiliserType { Name = "NO3N" });

            var fert = new Fertiliser
            {
                Definitions = types
            };

            return fert;
        }
    }
}
