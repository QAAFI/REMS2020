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
using Models;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class IrrigationsQueryHandler : IRequestHandler<IrrigationsQuery, Operation[]>
    {
        private readonly IRemsDbContext _context;

        public IrrigationsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Operation[]> Handle(IrrigationsQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Operation[] Handler(IrrigationsQuery request)
        {
            var ops = _context.Irrigations
                .Where(i => i.TreatmentId == request.TreatmentId)
                .Select(i => new Operation()
                {
                    Action = $"[Irrigation].Apply({i.Amount});",
                    Date = i.Date.ToString("yyyy-MM-dd")
                })
                .ToArray();

            return ops;
        }
    }
}