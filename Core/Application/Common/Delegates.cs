using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public delegate void ExceptionHandler(Exception exception);

    public delegate Task<object> QueryHandler(object query, CancellationToken token = default);
    public delegate T Query<T>(IRequest<T> query);

    public delegate void StringSender(string item);
}
