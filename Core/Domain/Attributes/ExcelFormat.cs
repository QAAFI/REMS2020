using System;

namespace Rems.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExcelFormat : Attribute
    {
        public string Format { get; private set; }

        public int Dependency { get; private set; }

        public bool Required { get; private set; }

        public string[] Names { get; private set; }

        public ExcelFormat(string format, int dependency, bool required, params string[] names)
        {
            Format = format;
            Dependency = dependency;
            Required = required;
            Names = names;
        }
    }
}
