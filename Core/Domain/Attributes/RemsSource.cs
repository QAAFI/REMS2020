using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExcelSource : Attribute
    {
        public string Format { get; private set; }

        public string[] Names { get; private set; }

        public ExcelSource(string format, params string[] names)
        {
            Format = format;
            Names = names;
        }
    }
}
