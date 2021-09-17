using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    /// <summary>
    /// Enables a process to report its progress and request data
    /// </summary>
    public abstract class TaskRunner : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Occurs when the tracker switches to the next item in its task
        /// </summary>
        public event EventHandler<string> NextItem;

        /// <summary>
        /// Occurs if the tracker fails to complete its task
        /// </summary>
        public event EventHandler<Exception> TaskFailed;

        public IQueryHandler Handler { get; set; }

        /// <summary>
        /// Used to report the progress of the task
        /// </summary>
        public ProgressReporter Reporter { get; set; }

        /// <summary>
        /// The number of items the tracker is processing
        /// </summary>
        public abstract int Items { get; }

        /// <summary>
        /// The number of steps required to process all the items
        /// </summary>
        public abstract int Steps { get; }

        /// <summary>
        /// Invokes the NextItem event
        /// </summary>
        protected void OnNextItem(string item) 
            => NextItem?.Invoke(this, item);

        /// <summary>
        /// Invokes the TaskFailed event
        /// </summary>
        protected void OnTaskFailed(Exception error) 
            => TaskFailed?.Invoke(this, error);

        /// <summary>
        /// Initiates the tracker task
        /// </summary>
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
