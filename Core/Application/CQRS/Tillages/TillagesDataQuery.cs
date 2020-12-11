using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class TillagesDataQuery : IRequest<SeriesData>
    {
        public int TreatmentId { get; set; }
    }

    public class TillagesDataQueryHandler : IRequestHandler<TillagesDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public TillagesDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(TillagesDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(TillagesDataQuery request, CancellationToken token)
        {
            var tillages = _context.Tillages
                .Where(i => i.TreatmentId == request.TreatmentId)
                .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), tillages.Count()),
                Y = Array.CreateInstance(typeof(double), tillages.Count()),
                XName = "Date",
                YName = "Depth",
                Name = "Tillages"
            };

            for (int i = 0; i < tillages.Length; i++)
            {
                var item = tillages[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Depth, i);
            }

            return data;
        }
    }
}
