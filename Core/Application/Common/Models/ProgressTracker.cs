using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public abstract class ProgressTracker : IProgressTracker, IDisposable
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event Action IncrementProgress;

        /// <inheritdoc/>
        public event Action TaskFinished;

        /// <inheritdoc/>
        public event Action<string> NextItem;

        /// <inheritdoc/>
        public event Action<Exception> TaskFailed;

        /// <inheritdoc/>
        public event Func<object, Task<object>> Query;

        /// <inheritdoc/>
        public abstract int Items { get; }

        /// <inheritdoc/>
        public abstract int Steps { get; }

        /// <summary>
        /// Invokes the Query event
        /// </summary>
        protected async Task<T> InvokeQuery<T>(IRequest<T> query) 
            => (T)await Query(query);

        /// <summary>
        /// Invokes the NextItem event
        /// </summary>
        protected void OnNextItem(string item) 
            => NextItem?.Invoke(item);

        /// <summary>
        /// Invokes the IncrementProgress event
        /// </summary>
        protected void OnIncrementProgress() 
            => IncrementProgress?.Invoke();

        /// <summary>
        /// Invokes the TaskFinished event
        /// </summary>
        protected void OnTaskFinished()
            => TaskFinished?.Invoke();

        /// <summary>
        /// Invokes the TaskFailed event
        /// </summary>
        protected void OnTaskFailed(Exception error) 
            => TaskFailed?.Invoke(error);

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

                IncrementProgress = null;
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
