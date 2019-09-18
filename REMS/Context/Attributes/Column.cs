using System;
using System.Collections.Generic;
using System.Text;

namespace REMS
{      
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Column : Attribute
    {
        private string name;

        public virtual string Name
        {
            get { return name; }
        }

        public Column(string name)
        {
            this.name = name;
        }
    }

}
