using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common;
using System.Threading;

namespace WindowsClient.Models
{
    public class QueryManager
    {
        public static IServiceProvider Provider { get; set; }

        public static CancellationTokenSource TokenSource { get; set; } = new();

        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="request">The request object</param>
        public static Task<T> Request<T>(IRequest<T> request)
        {
            var mediator = Provider.GetService<IMediator>();
            return mediator.Send(request, TokenSource.Token);
        }
    }
}
