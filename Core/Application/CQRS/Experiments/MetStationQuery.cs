using System;
using System.Threading.Tasks;
using System.Threading;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    public class MetStationQuery : IRequest<string>
    {
        public int ExperimentId { get; set; }
    }

    public class MetStationQueryHandler : IRequestHandler<MetStationQuery, string>
    {
        private readonly IRemsDbContext _context;

        public MetStationQueryHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<string> Handle(MetStationQuery request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));
        
        private string Handler(MetStationQuery request)
        {
            return _context.Experiments.Find(request.ExperimentId)
                .MetStation
                .Name;
        }
    }
}
