using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class FertilizationsQuery : IRequest<Operation[]>, IParameterised
    {   
        public int TreatmentId { get; set; }

        public void Parameterise(params object[] args)
        {
            if (args.Length != 1) 
                throw new Exception($"Invalid number of parameters. \n Expected: 1 \n Received: {args.Length}");

            if (args[0] is int id)
                TreatmentId = id;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(int)} \n Received: {args[0].GetType()}");
        }
    }

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
