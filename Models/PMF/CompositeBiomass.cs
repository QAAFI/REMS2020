using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF
{
    public class CompositeBiomass : Node
    {
        public List<string> Propertys { get; set; } = default;

        public double DMDOfStructural { get; set; } = default;

        public CompositeBiomass()
        { }
    }
}
