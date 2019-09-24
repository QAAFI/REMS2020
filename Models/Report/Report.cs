using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Report
{
    public class Report : Node
    {
        public List<string> VariableNames { get; set; } = default;

        public List<string> EventNames { get; set; } = default;

        public Report()
        { }
    }
}
