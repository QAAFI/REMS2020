using System;
using System.Data;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;
using System.Linq;
using Models.Soils;
using Rems.Application.Common.Extensions;
using Models.WaterModel;
using Models.Core;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class ZoneQueryHandler : IRequestHandler<ZoneQuery, Zone>
    {
        private readonly IRemsDbContext _context;

        public ZoneQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Zone> Handle(ZoneQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Zone Handler(ZoneQuery request)
        {
            var field = _context.Experiments.Find(request.ExperimentId).Field;

            var zone = new Zone()
            {
                Name = "Field",
                Slope = field.Slope.GetValueOrDefault()
            };

            return zone;
        }
    }
}