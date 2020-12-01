using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class IrrigationDataQuery : IRequest<SeriesData>
    {
        public int TreatmentId { get; set; }
    }

    public class IrrigationDataQueryHandler : IRequestHandler<IrrigationDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public IrrigationDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(IrrigationDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(IrrigationDataQuery request, CancellationToken token)
        {
            var irrigations = _context.Irrigations
                .Where(i => i.TreatmentId == request.TreatmentId)
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), irrigations.Count()),
                Y = Array.CreateInstance(typeof(double), irrigations.Count()),
                XLabel = "Date",
                YLabel = "Amount",
                Title = "Irrigations"                
            };

            for (int i = 0; i < irrigations.Length; i++)
            {
                var item = irrigations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}
