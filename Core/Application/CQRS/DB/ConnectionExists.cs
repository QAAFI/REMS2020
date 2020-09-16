using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    public class ConnectionExists : IRequest<bool>
    {
    }

    public class ConnectionExistsHandler : IRequestHandler<ConnectionExists, bool>
    {
        private readonly IRemsDbFactory _factory;

        public ConnectionExistsHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public Task<bool> Handle(ConnectionExists request, CancellationToken cancellationToken) => Task.Run(() => File.Exists(_factory.Connection));
    }
}
