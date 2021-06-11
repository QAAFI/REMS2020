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
        /// The advice provided to the user
        /// </summary>
        Advice Advice { get; set; }

        bool Valid { get; set; }

        /// <summary>
        /// Checks if the node is ready to be imported and updates the state accordingly
        /// </summary>
        void Validate();
    }

    public abstract class BaseValidator<TComponent> : INodeValidator
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public Advice Advice { get; set; } = new Advice();

        public TComponent Component { get; set; }

        public bool Valid { get; set; } = false;

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
        public override void Validate()
        {
            Valid = true;
        }
    }
}
