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

        QueryHandler Query { get; set; }

        int Items { get; }

        int Steps { get; }

        Task Run();
    }
}
