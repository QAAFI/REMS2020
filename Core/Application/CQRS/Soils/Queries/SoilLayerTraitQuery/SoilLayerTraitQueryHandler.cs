using MediatR;

using Rems.Application.Common.Interfaces;

using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Rems.Domain.Entities;
using Rems.Application.Common.Extensions;

namespace Rems.Application.Soils.Queries
{
    public class SoilLayerTraitQueryHandler : IRequestHandler<SoilLayerTraitQuery, double[]>
    {
        private readonly IRemsDbContext _context;
        private readonly IMediator _mediator;

        public SoilLayerTraitQueryHandler(IRemsDbFactory factory, IMediator mediator)
        {
            _context = factory.Context;
            _mediator = mediator;
        }

        public async Task<double[]> Handle(SoilLayerTraitQuery request, CancellationToken cancellationToken)
        {
            var trait = _context.Traits.FirstOrDefault(t => t.Name == request.TraitName);

            if (trait == null) 
            {
                // TODO: Similar block of code is used in MetFileDataQueryHandler GetData(), should
                // find a way to reuse not repeat (might be needed in other places down the line as well)

                var args = new ItemNotFoundArgs()
                {
                    Options = _context.Traits.Select(t => t.Name).ToArray(),
                    Name = request.TraitName
                };

                EventManager.InvokeItemNotFound(null, args);
                
                trait = _context.Traits.FirstOrDefault(t => t.Name == args.Selection);                
                trait.Name = request.TraitName;
                _context.SaveChanges();
            }

            var layers = from layer in _context.SoilLayers
                         where layer.SoilId == request.SoilId       // Find the layers of the soil
                         orderby layer.DepthFrom                    // Sort the layers by depth
                         select (
                            from traits in layer.SoilLayerTraits    // Find all the traits in the soil layer
                            where traits.Trait == trait             // Filter by the requested trait
                            select traits.Value.Value               // Select the value of the trait
                         ).FirstOrDefault();                        // Return 0.0 if no value is found
            
            return layers.ToArray();
        }
    }
}