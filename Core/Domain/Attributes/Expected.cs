using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Expected : Attribute
    {
        public string[] Names { get; private set; }

        public Expected(params string[] names)
        {
            Names = names;
        }
    }
}
