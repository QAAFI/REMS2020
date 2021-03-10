using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Opens a connection to a local database
    /// </summary>
    public class OpenDBCommand : IRequest<Unit>
    {
        /// <summary>
        /// The database to connect to
        /// </summary>
        public string FileName { get; set; }        
    }

    public class OpenDBCommandHandler : IRequestHandler<OpenDBCommand, Unit>
    {
        private readonly IRemsDbFactory _factory;

        public OpenDBCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<Unit> Handle(OpenDBCommand request, CancellationToken cancellationToken) 
            => Task.Run(() => Handler(request));

        private Unit Handler(OpenDBCommand request)
        {
            _factory.Connection = request.FileName;
            return Unit.Value;
        }
    }
}
