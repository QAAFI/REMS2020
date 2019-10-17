using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Weather : ApsimNode
    {
        public string FileName { get; set; } = default;

        public string ExcelWorkSheetName { get; set; } = default;

        public Weather()
        { }
    }
}
