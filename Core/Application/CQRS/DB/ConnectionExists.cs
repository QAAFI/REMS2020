using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Checks if there is a connection to a database
    /// </summary>
    public class ConnectionExists : IRequest<bool>
    {
    }

    public class ConnectionExistsHandler : IRequestHandler<ConnectionExists, bool>
    {
        private readonly IFileManager _manager;

        public ConnectionExistsHandler(IFileManager manager)
        {
            _manager = manager;
        }

        public Task<bool> Handle(ConnectionExists request, CancellationToken cancellationToken) 
            => Task.Run(() => File.Exists(_manager.DbConnection));
    }
}
