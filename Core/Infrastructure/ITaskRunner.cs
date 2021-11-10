using Rems.Application.Common;
using System;
using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    /// <summary>
    /// Enables a task to report its progress and request data
    /// </summary>
    public interface ITaskRunner
    {
        /// <summary>
        /// Occurs when the task beigns the next item
        /// </summary>
        public event EventHandler<string> NextItem;

        /// <summary>
        /// Allows the task to invoke queries
        /// </summary>
        public IQueryHandler Handler { get; set; }

        /// <summary>
        /// Used to report the progress of the task
        /// </summary>
        public IProgress<int> Progress { get; set; }

        /// <summary>
        /// The number of items the tracker is processing
        /// </summary>
        public int Items { get; }

        /// <summary>
        /// The number of steps required to process all the items
        /// </summary>
        public int Steps { get; }

        /// <summary>
        /// Initiates the tracker task
        /// </summary>
        public Task Run();     
    }
}
