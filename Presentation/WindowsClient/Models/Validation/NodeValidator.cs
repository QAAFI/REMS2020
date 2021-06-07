using System;
using Rems.Application.Common;

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
        event EventHandler<Args<string, object>> StateChanged;

        /// <summary>
        /// Occurs when the advice provided to the user is changed
        /// </summary>
        event EventHandler<Args<Advice>> SetAdvice;

        /// <summary>
        /// Checks if the node is ready to be imported and updates the state accordingly
        /// </summary>
        void Validate();
    }

    public abstract class BaseValidator<TComponent> : INodeValidator
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event EventHandler<Args<string, object>> StateChanged;

        /// <inheritdoc/>
        public event EventHandler<Args<Advice>> SetAdvice;

        public TComponent Component { get; set; }

        protected void InvokeStateChanged(string state, object value)
            => StateChanged?.Invoke(this, new Args<string, object> { Item1 = state, Item2 = value });

        protected void InvokeSetAdvice(Advice advice)
            => SetAdvice?.Invoke(this, new Args<Advice> { Item = advice });

        public abstract void Validate();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Component is IDisposable disposable)
                        disposable.Dispose();
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
    public class NullValidator : BaseValidator<object>
    {
        /// <inheritdoc/>
        public override void Validate() => InvokeStateChanged("Valid", true);        
    }
}
