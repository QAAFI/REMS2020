using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    public interface IProgressTracker
    {
        event NextItemHandler NextItem;

        event Action IncrementProgress;

        event Action TaskFinished;

        event ExceptionHandler TaskFailed;

        QueryHandler Query { get; set; }

        int Items { get; }

        int Steps { get; }

        Task Run();
    }
}
