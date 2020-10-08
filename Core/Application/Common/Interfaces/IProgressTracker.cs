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

        event CommandHandler SendCommand;

        event QueryHandler SendQuery;

        int Items { get; }

        int Steps { get; }

        Task Run();
    }
}
