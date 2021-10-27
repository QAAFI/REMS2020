using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    /// <summary>
    /// A template for creating an instance of a class
    /// </summary>
    public interface ITemplate<T> where T : class
    {
        public T Create();

        public Task<T> AsyncCreate();
    }
}
