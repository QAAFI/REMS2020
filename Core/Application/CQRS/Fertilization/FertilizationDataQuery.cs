using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find data on fertilization operations for a treatment
    /// </summary>
    public class FertilizationDataQuery : IRequest<SeriesData>
    {
        /// <summary>
        /// The source treatment
        /// </summary>
        public int TreatmentId { get; set; }
    }

    public class FertilizationDataQueryHandler : IRequestHandler<FertilizationDataQuery, SeriesData>
    {
        private readonly IRemsDbContext _context;

        public FertilizationDataQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<SeriesData> Handle(FertilizationDataQuery request, CancellationToken token) => Task.Run(() => Handler(request, token));

        private SeriesData Handler(FertilizationDataQuery request, CancellationToken token)
        {
            var fertilizations = _context.Fertilizations
                 .Where(i => i.TreatmentId == request.TreatmentId)
                 .ToArray();

            var data = new SeriesData()
            {
                X = Array.CreateInstance(typeof(DateTime), fertilizations.Count()),
                Y = Array.CreateInstance(typeof(double), fertilizations.Count()),
                XName = "Date",
                YName = "Amount",
                Name = "Fertilizations"
            };

            for (int i = 0; i < fertilizations.Length; i++)
            {
                var item = fertilizations[i];

                data.X.SetValue(item.Date, i);
                data.Y.SetValue(item.Amount, i);
            }

            return data;
        }
    }
}
