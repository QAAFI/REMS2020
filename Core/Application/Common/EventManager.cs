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
        public delegate void ItemNotFoundHandler(object sender, ItemNotFoundArgs args);
        public static event ItemNotFoundHandler ItemNotFound;
        public static void InvokeItemNotFound(object sender, ItemNotFoundArgs args) 
            => ItemNotFound?.Invoke(sender, args);

        public delegate void ProgressTrackingActivatedHandler(object sender, ProgressTrackingActivatedArgs args);
        public static event ProgressTrackingActivatedHandler ProgressTrackingActivated;
        public static void InvokeProgressTrackingActivated(object sender, ProgressTrackingActivatedArgs args) 
            => ProgressTrackingActivated?.Invoke(sender, args);
    }

    public class ItemNotFoundArgs : EventArgs
    {
        public string Name { get; set; }

        public string[] Options { get; set; }        

        public string Selection { get; set; }

        public bool Cancelled { get; set; } = false;
    }

    public class ProgressTrackingActivatedArgs : EventArgs
    {
        public int Count { get; set; }
    }
}
