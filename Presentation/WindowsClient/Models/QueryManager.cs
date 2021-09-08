using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;
using Rems.Infrastructure;
using System.Threading;
using WindowsClient.Forms;

namespace WindowsClient.Models
{
    public class QueryManager : IQueryHandler
    {
        #region Singleton implementation
        private static readonly QueryManager instance = new();

        public static QueryManager Instance => instance;

        static QueryManager()
        { }

        private QueryManager()
        { }
        #endregion

        public IServiceProvider Provider { get; set; }

        public CancellationTokenSource TokenSource { get; set; } = new();

        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="request">The request object</param>
        public Task<T> Query<T>(IRequest<T> request)
        {
            var mediator = Provider.GetService<IMediator>();
            return mediator.Send(request, TokenSource.Token);
        }

        public static Task<T> Request<T>(IRequest<T> request) => instance.Query(request);

        public bool Confirm(string message)
                => AlertBox.Show(message, AlertType.Ok, cancel: true) == DialogResult.OK;
    }
}
