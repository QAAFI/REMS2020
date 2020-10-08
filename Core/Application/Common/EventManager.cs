using MediatR;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public delegate void ExceptionHandler(Exception exception);
    public delegate void ItemNotFoundHandler(object sender, ItemNotFoundArgs args);
    public delegate void ProgressTrackingHandler(object sender, ProgressTrackingArgs args);
    public delegate void NextItemHandler(object sender, NextItemArgs args);
    public delegate Task CommandHandler(IRequest command);
    public delegate Task<object> QueryHandler(object query);
    public delegate string FileParser(string file);

    // TODO: It might be safer to implement this as a singleton, as opposed to using static events
    public static class EventManager
    {
        /// <summary>
        /// 
        /// </summary>
        public static event ItemNotFoundHandler ItemNotFound;        
        public static void InvokeItemNotFound(object sender, ItemNotFoundArgs args) 
            => ItemNotFound?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event ProgressTrackingHandler ProgressTracking;        
        public static void InvokeProgressTracking(object sender, ProgressTrackingArgs args) 
            => ProgressTracking?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event NextItemHandler NextItem;        
        public static void InvokeNextItem(object sender, NextItemArgs args)
            => NextItem?.Invoke(sender, args);

        /// <summary>
        /// 
        /// </summary>
        public static event Action ProgressIncremented;
        public static void InvokeProgressIncremented()
            => ProgressIncremented?.Invoke();

        /// <summary>
        /// 
        /// </summary>
        public static event Action StopProgress;
        public static void InvokeStopProgress()
            => StopProgress?.Invoke();

        public static event QueryHandler SendQuery;
        public static T OnSendQuery<T>(IRequest<T> query)
        {
            var task = SendQuery?.Invoke(query);
            task.Wait();
            return (T)task.Result;
        }

        public static event FileParser RequestRawData;
        public static string OnRequestRawData(string file)
            => RequestRawData?.Invoke(file);
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

    public class NextItemArgs : EventArgs
    {
        public int Maximum { get; set; }

        public string Item { get; set; }
    }
}
