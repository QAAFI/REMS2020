using MediatR;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public abstract class ProgressTracker : IProgressTracker, IDisposable
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event EventHandler TaskFinished;

        /// <inheritdoc/>
        public event EventHandler<Args<string>> NextItem;

        /// <inheritdoc/>
        public event EventHandler<Args<Exception>> TaskFailed;

        /// <inheritdoc/>
        public event EventHandler<RequestArgs<object, Task<object>>> Query;

        public ProgressReporter Progress { get; set; }

        /// <inheritdoc/>
        public abstract int Items { get; }

        /// <inheritdoc/>
        public abstract int Steps { get; }

        /// <summary>
        /// Invokes the Query event
        /// </summary>
        protected async Task<T> InvokeQuery<T>(IRequest<T> query)
        {
            var args = new RequestArgs<object, Task<object>> { Item = query };
            Query?.Invoke(this, args);
            return (T)await args.Result;
        }

        /// <summary>
        /// Invokes the NextItem event
        /// </summary>
        protected void OnNextItem(string item) 
            => NextItem?.Invoke(this, new Args<string> { Item = item });

        /// <summary>
        /// Invokes the TaskFinished event
        /// </summary>
        protected void OnTaskFinished()
            => TaskFinished?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Invokes the TaskFailed event
        /// </summary>
        protected void OnTaskFailed(Exception error) 
            => TaskFailed?.Invoke(this, new Args<Exception> { Item = error });

        /// <inheritdoc/>
        public abstract Task Run();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                TaskFinished = null;
                NextItem = null;
                TaskFailed = null;
                Query = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
    }
}
