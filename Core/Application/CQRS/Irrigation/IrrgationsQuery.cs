using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Models;
using Rems.Application.Common.Interfaces;


namespace Rems.Application.CQRS
{ 
    public class IrrigationsQuery : IRequest<Operation[]>, IParameterised
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
