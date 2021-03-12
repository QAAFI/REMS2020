using System;

namespace Rems.Application.Common.Interfaces
{
    /// <summary>
    /// Indicates an object that can have its properties parameterised
    /// </summary>
    public interface IParameterised
    {
        /// <summary>
        /// Sets the object properties using the given args
        /// </summary>
        void Parameterise(params object[] args);
    }
}
