using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Soil model for an experiment
    /// </summary>
    public class SoilQuery : IRequest<Soil>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }
    }

    public class SoilQueryHandler : IRequestHandler<SoilQuery, Soil>
    {
        private readonly IRemsDbContext _context;

        public SoilQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Soil> Handle(SoilQuery request, CancellationToken token) => Task.Run(() => Handler(request));

        private Soil Handler(SoilQuery request)
        {
            var field = _context.Experiments.Find(request.ExperimentId).Field;

            var soil = new Soil()
            {
                Name = field.Soil.SoilType,
                Latitude = field.Latitude.GetValueOrDefault(),
                Longitude = field.Longitude.GetValueOrDefault(),
                Site = field.Site.Name,
                Region = field.Site.Region.Name
            };

            return soil;
        }
    }
}
