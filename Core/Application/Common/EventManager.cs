using Microsoft.EntityFrameworkCore;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application
{
    // TODO: It might be safer to implement this as a singleton, as opposed to using static events
    public static class EventManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static event ItemNotFoundHandler ItemNotFound;
        public delegate void ItemNotFoundHandler(object sender, ItemNotFoundArgs args);
        public static void InvokeItemNotFound(object sender, ItemNotFoundArgs args) 
            => ItemNotFound?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event ProgressTrackingHandler ProgressTracking;
        public delegate void ProgressTrackingHandler(object sender, ProgressTrackingArgs args);
        public static void InvokeProgressTracking(object sender, ProgressTrackingArgs args) 
            => ProgressTracking?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event StartProgressHandler StartProgress;
        public delegate void StartProgressHandler(object sender, StartProgressArgs args);
        public static void InvokeStartProgress(object sender, StartProgressArgs args)
            => StartProgress?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler ProgressIncremented;
        public static void InvokeProgressIncremented(object sender, EventArgs args)
            => ProgressIncremented?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler StopProgress;
        public static void InvokeStopProgress(object sender, EventArgs args)
            => StopProgress?.Invoke(sender, args);
    }

    public class ItemNotFoundArgs : EventArgs
    {
        public string Name { get; set; }

        public string[] Options { get; set; }

        public string Selection { get; set; }

        public bool Cancelled { get; set; } = false;
    }

    public class ProgressTrackingArgs : EventArgs
    {
        public int Items { get; set; }

        public string Title { get; set; }
    }

    public class StartProgressArgs : EventArgs
    {
        public int Maximum { get; set; }

        public string Item { get; set; }
    }
}
