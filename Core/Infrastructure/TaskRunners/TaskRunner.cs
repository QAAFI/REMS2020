using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    public abstract class TaskRunner : ITaskRunner, IDisposable
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event EventHandler<Args<string>> NextItem;

        /// <inheritdoc/>
        public event EventHandler<Args<Exception>> TaskFailed;

        public IQueryHandler Handler { get; set; }

        /// <inheritdoc/>
        public ProgressReporter Reporter { get; set; }

        /// <inheritdoc/>
        public abstract int Items { get; }

        /// <inheritdoc/>
        public abstract int Steps { get; }

        /// <summary>
        /// Invokes the NextItem event
        /// </summary>
        protected void OnNextItem(string item) 
            => NextItem?.Invoke(this, new Args<string> { Item = item });

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

                NextItem = null;
                TaskFailed = null;

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
