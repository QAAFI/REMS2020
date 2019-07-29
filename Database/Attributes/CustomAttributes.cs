using System;
using System.Collections.Generic;
using System.Text;

namespace Database
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

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Table : Attribute
    {
        private string name;

        public virtual string Name
        {
            get { return name; }
        }

        private Type relation;

        public virtual Type Relation
        {
            get { return relation; }
        }

        public Table(string name, Type relation)
        {
            this.name = name;
            this.relation = relation;
        }
    }


}
