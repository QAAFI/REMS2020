﻿using MediatR;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common.Interfaces
{
    /// <summary>
    /// Enables a process to report its progress and request data
    /// </summary>
    public interface IProgressTracker
    {
        /// <summary>
        /// Occurs when the tracker switches to the next item in its task
        /// </summary>
        event Action<string> NextItem;

        /// <summary>
        /// Occurs when the tracker makes progress with its task
        /// </summary>
        event Action IncrementProgress;

        /// <summary>
        /// Occurs when the tracker completes its task
        /// </summary>
        event Action TaskFinished;

        /// <summary>
        /// Occurs if the tracker fails to complete its task
        /// </summary>
        event Action<Exception> TaskFailed;

        /// <summary>
        /// Occurs when the tracker requests data
        /// </summary>
        event Func<object, Task<object>> Query;

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
