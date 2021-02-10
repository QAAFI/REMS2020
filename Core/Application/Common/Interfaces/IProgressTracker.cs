using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IProgressTracker
    {
        event Action<string> NextItem;

        event Action IncrementProgress;

        event Action TaskFinished;

        event Action<Exception> TaskFailed;

        event Func<object, Task<object>> Query;

        int Items { get; }

        int Steps { get; }

        Task Run();
    }
}
