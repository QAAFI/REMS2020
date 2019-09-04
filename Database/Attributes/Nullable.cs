using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{  
    /// <summary>
    /// If a Property is nullable
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Nullable : Attribute
    {
        public Nullable()
        { }
    }

}
