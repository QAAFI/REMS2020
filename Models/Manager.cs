using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Manager : ApsimNode
    {
        public string Code { get; set; } = default;

        public Dictionary<string, string> Parameters { get; set; } = default;

        public Manager()
        { }
    }
}
