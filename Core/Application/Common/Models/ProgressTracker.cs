using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public abstract class ProgressTracker : IProgressTracker
    {
        public abstract int Items { get; protected set; }
        public abstract int Steps { get; protected set; }

        public event ProgressTrackingHandler StartProgress;
        public event NextItemHandler NextItem;
        public event Action IncrementProgress;
        public event Action TaskFinished;
        public event ExceptionHandler TaskFailed;
        public event CommandHandler SendCommand;
        public event QueryHandler SendQuery;

        public ProgressTracker()
        {
            EventManager.ProgressIncremented += OnIncrementProgress;
        }

        public abstract Task Run();

        protected void OnStartProgress(object sender, ProgressTrackingArgs args)
        {
            StartProgress?.Invoke(sender, args);
        }

        protected void OnNextItem(string item)
        {
            NextItem?.Invoke(item);
        }

        protected void OnIncrementProgress()
        {
            IncrementProgress?.Invoke();
        }

        protected void OnTaskFinished()
        {
            TaskFinished.Invoke();
        }

        protected void OnTaskFailed(Exception error)
        {
            TaskFailed?.Invoke(error);
        }

        protected Task OnSendCommand(IRequest command)
        {
            return SendCommand?.Invoke(command);
        }

        protected T OnSendQuery<T>(IRequest<T> query)
        {
            var task = SendQuery?.Invoke(query);
            task.Wait();
            return (T)task.Result;
        }
    }
}
