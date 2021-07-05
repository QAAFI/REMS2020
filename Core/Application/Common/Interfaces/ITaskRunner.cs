using Rems.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    /// <summary>
    /// Enables a process to report its progress and request data
    /// </summary>
    public interface ITaskRunner
    {
        /// <summary>
        /// Occurs when the tracker switches to the next item in its task
        /// </summary>
        event EventHandler<Args<string>> NextItem;

        /// <summary>
        /// Occurs if the tracker fails to complete its task
        /// </summary>
        event EventHandler<Args<Exception>> TaskFailed;

        /// <summary>
        /// Used to report the progress of the task
        /// </summary>
        ProgressReporter Reporter { get; set; }

        /// <summary>
        /// The number of items the tracker is processing
        /// </summary>
        int Items { get; }

        /// <summary>
        /// The number of steps required to process all the items
        /// </summary>
        int Steps { get; }

        /// <summary>
        /// Initiates the tracker task
        /// </summary>
        Task Run();
    }
}
