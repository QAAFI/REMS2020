using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF
{
    public class Cultivar : Node
    {
        public List<string> Command { get; set; } = default;

        public Cultivar()
        { }
    }
}
