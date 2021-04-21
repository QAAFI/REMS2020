using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common;

namespace WindowsClient.Models
{
    public class QueryManager
    {
        public static IServiceProvider Provider { get; set; }

        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="request">The request object</param>
        public static async Task<T> Request<T>(IRequest<T> request) 
            => (T)await TryQuery(request);

        public static void Request(object sender, RequestArgs<object, Task<object>> args)
            => args.Result = TryQuery(args.Item);

        /// <summary>
        /// Sends a query to the mediator
        /// </summary>
        /// <param name="query">The query object</param>
        private static async Task<object> TryQuery(object query)
        {
            Application.UseWaitCursor = true;
            List<Exception> errors = new List<Exception>();
            try
            {
                var mediator = Provider.GetService<IMediator>();
                var result = await mediator.Send(query);

                return result;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                errors.Add(error);
            }
            finally
            {
                Application.UseWaitCursor = false;
            }

            var builder = new StringBuilder();

            foreach (var error in errors)
                builder.AppendLine(error.Message + "at\n" + error.StackTrace + "\n");

            MessageBox.Show(builder.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return default;
        }
    }
}
