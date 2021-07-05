using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;

namespace Rems.Application.CQRS
{
    public abstract class ContextQuery<TResponse> : IRequest<TResponse>
    {
        protected CancellationToken token;

        protected IRemsDbContext _context;

        protected abstract TResponse Run();

        public ProgressReporter Progress { get; set; }

        /// <summary>
        /// Manages a single unit of work
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        public class BaseHandler<TRequest> : IRequestHandler<TRequest, TResponse>
            where TRequest : ContextQuery<TResponse>, IRequest<TResponse>
        {
            private readonly IRemsDbContextFactory _factory;

            public BaseHandler(IRemsDbContextFactory factory)
            {
                _factory = factory;
            }

            public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
                => Task.Run(() => TryRun(request, cancellationToken));

            private TResponse TryRun(TRequest request, CancellationToken cancellationToken)
            {
                request.token = cancellationToken;
                using var context = _factory.Create();
                request._context = context;
                return request.Run();
            }
        }
    }    
}
