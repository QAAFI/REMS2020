using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rems.Domain.Attributes
{
    public enum RemsSource
    {
        /// <summary>
        /// Sourced from the information spreadsheet
        /// </summary>
        Information,
        
        /// <summary>
        /// Sourced from the experiments spreadsheet
        /// </summary>
        Experiments,
        
        /// <summary>
        /// Sourced from the data spreadsheet
        /// </summary>
        Data
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExcelSource : Attribute
    {
        public RemsSource Source { get; private set; }

        public string[] Names { get; private set; }

        public ExcelSource(RemsSource source, params string[] names)
        {
            Source = source;
            Names = names;
        }
    }
}
