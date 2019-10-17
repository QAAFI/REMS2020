using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Models the simulation clock
    /// </summary>
    public class Clock : ApsimNode
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Clock()
        {
            Name = "Clock";
        }
    }
}
