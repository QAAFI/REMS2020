using System;
using System.Collections.Generic;
using System.Text;

namespace REMS
{  
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKey : Attribute
    {
        public PrimaryKey()
        { }
    }

}
