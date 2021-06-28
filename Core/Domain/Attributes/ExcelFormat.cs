using System;

namespace Rems.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExcelFormat : Attribute
    {
        public string Format { get; private set; }

        public int Priority { get; private set; }

        public bool Required { get; private set; }

        public string[] Names { get; private set; }

        public ExcelFormat(string format, int priority, bool required, params string[] names)
        {
            Format = format;
            Priority = priority;
            Required = required;
            Names = names;
        }
    }
}
