using System;
using System.Collections.Generic;
using System.Text;

namespace REMS
{      
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Relation : Attribute
    {
        private string name;

        public virtual string Name
        {
            get { return name; }
        }

        public Relation(string name)
        {
            this.name = name;
        }
    }

}
