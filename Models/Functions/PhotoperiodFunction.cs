using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Functions
{
    public class PhotoperiodFunction : ApsimNode
    {
        public double Twilight { get; set; } = default;

        public double DayLength { get; set; } = default;

        public PhotoperiodFunction()
        { }
    }
}
