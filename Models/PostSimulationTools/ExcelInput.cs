using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PostSimulationTools
{
    public class ExcelInput : ApsimNode
    {
        public string FileName { get; set; } = default;

        public string FileNameMetaData { get; set; } = default;

        public List<string> SheetNames { get; set; } = default;

        public ExcelInput()
        { }
    }
}
