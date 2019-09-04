using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{  
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ForeignKey : Attribute
    {
        public ForeignKey()
        { }
    }

}
