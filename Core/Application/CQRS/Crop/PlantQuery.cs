using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.PMF;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Requests an Apsim Plant model from the specified experiment
    /// </summary>
    public class PlantQuery : IRequest<Plant>
    {   
        /// <summary>
        /// The experiment to generate the plant model from
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class PlantQueryHandler : IRequestHandler<PlantQuery, Plant>
    {
        private readonly IRemsDbContext _context;

        public PlantQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Plant> Handle(PlantQuery request, CancellationToken token) => Task.Run(() => Handler(request));
        
        private Plant Handler(PlantQuery request)
        {
            var crop = _context.Experiments.Find(request.ExperimentId).Crop;
            
            var valid = request.Report.ValidateItem(crop.Name, nameof(Plant.Name));

            request.Report.CommitValidation(nameof(Plant), !valid);

            var plant = new Plant
            {
                PlantType = crop.Name,
                Name = crop.Name,
                ResourceName = crop.Name
            };
            
            return plant;
        }
    }
}
