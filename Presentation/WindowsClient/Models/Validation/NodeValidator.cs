using System;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages the validation of a node containing excel data
    /// </summary>
    public interface INodeValidator : IDisposable
    {
        /// <summary>
        /// Occurs when the data is modified
        /// </summary>
        event Action<string, object> StateChanged;

        /// <summary>
        /// Occurs when the advice provided to the user is changed
        /// </summary>
        event Action<Advice> SetAdvice;

        /// <summary>
        /// Checks if the node is ready to be imported and updates the state accordingly
        /// </summary>
        Task Validate();
    }

    public abstract class BaseValidator<TDisposable> : INodeValidator
        where TDisposable : IDisposable
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        public TDisposable Component { get; set; }

        protected void InvokeStateChanged(string state, object value)
            => StateChanged?.Invoke(state, value);

        protected void InvokeSetAdvice(Advice advice)
            => SetAdvice?.Invoke(advice);

        public abstract Task Validate();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Component?.Dispose();
                }

                StateChanged = null;
                SetAdvice = null;

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

    /// <summary>
    /// A default implementation of <see cref="BaseValidator{T}"/> that always successfully validates
    /// </summary>
    public class NullValidator<TDisposable> : BaseValidator<TDisposable>
        where TDisposable : IDisposable
    {
        /// <inheritdoc/>
        public async override Task Validate()
        {
            InvokeStateChanged("Valid", true);
        }
    }
}
