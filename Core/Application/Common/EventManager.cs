using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rems.Application.Common
{
    public delegate void ExceptionHandler(Exception exception);
    public delegate bool ItemNotFoundHandler(string item);
    public delegate string RequestItem(string item);

    public delegate Task CommandHandler(IRequest command);
    public delegate Task<object> QueryHandler(object query);
    public delegate T Query<T>(IRequest<T> query);

    public delegate void NextItemHandler(string item);

    // TODO: It might be safer to implement this as a singleton, as opposed to using static events
    public static class EventManager
    {
        /// <summary>
        /// 
        /// </summary>
        //public static event ItemNotFoundHandler ItemNotFound;        
        //public static IItemValidater InvokeItemNotFound(string item) 
        //    => ItemNotFound?.Invoke(item);

        /// <summary>
        /// 
        /// </summary>
        public static event Action ProgressIncremented;
        public static void InvokeProgressIncremented()
            => ProgressIncremented?.Invoke();
    }
}
