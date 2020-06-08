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
        public delegate string EntityNotFoundHandler(object sender, EntityNotFoundArgs args);
        public static event EntityNotFoundHandler EntityNotFound;

        public static string InvokeEntityNotFound(object sender, EntityNotFoundArgs args)
        {
            return EntityNotFound?.Invoke(sender, args);
        }
    }

    public class EntityNotFoundArgs : EventArgs
    {
        public string[] Options;        

        public string Name;
    }
}
