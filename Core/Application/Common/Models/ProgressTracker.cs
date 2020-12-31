using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public abstract class ProgressTracker : IProgressTracker
    {
        public abstract int Items { get; }
        public abstract int Steps { get; }

        public event Action IncrementProgress;
        public event Action TaskFinished;

        public event Action<string> NextItem;
        public event Action<Exception> TaskFailed;
        public event Func<object, Task<object>> Query;

        protected async Task<T> InvokeQuery<T>(IRequest<T> query) 
            => (T)await Query(query);        

        protected void OnNextItem(string item) 
            => NextItem?.Invoke(item);

        protected void OnIncrementProgress() 
            => IncrementProgress?.Invoke();

        protected void OnTaskFinished()
            => TaskFinished.Invoke();

        protected void OnTaskFailed(Exception error) 
            => TaskFailed?.Invoke(error);

        public abstract Task Run();
    }
}
