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
    public class FertilizationsQueryHandler : IRequestHandler<FertilizationsQuery, Operation[]>
    {
        private readonly IRemsDbContext _context;

        public FertilizationsQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Operation[]> Handle(FertilizationsQuery request, CancellationToken token)
        {
            return Task.Run(() => Handler(request));
        }

        private Operation[] Handler(FertilizationsQuery request)
        {
            var ops = _context.Fertilizations
                .Where(f => f.TreatmentId == request.TreatmentId)
                .Select(f => new Operation()
                {
                    Action = $"[Fertiliser].Apply({f.Amount}, Fertiliser.Types.Urea, {f.Depth});",
                    Date = f.Date.ToString("yyyy-MM-dd")
                })
                .ToArray();

            return ops;
        }
    }
}