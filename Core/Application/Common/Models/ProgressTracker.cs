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

        public event StringSender NextItem;
        public event ExceptionHandler TaskFailed;
        public QueryHandler Query { get; set; }        

        public abstract Task Run();

        protected void OnNextItem(string item) =>
            NextItem?.Invoke(item);        

        protected void OnIncrementProgress() =>
            IncrementProgress?.Invoke();

        protected void OnTaskFinished() =>
            TaskFinished.Invoke();

        protected void OnTaskFailed(Exception error) =>
            TaskFailed?.Invoke(error);

        protected T OnQuery<T>(IRequest<T> query)
        {
            var task = Query.Invoke(query);
            task.Wait();
            return (T)task.Result;
        }
    }
}
