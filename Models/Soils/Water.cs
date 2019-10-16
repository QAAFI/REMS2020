using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Water : Node
    {
        public List<double> Thickness { get; set; } = default;

        public List<double> BD { get; set; } = default;

        public List<double> AirDry { get; set; } = default;

        public List<double> LL15 { get; set; } = default;

        public List<double> DUL { get; set; } = default;

        public List<double> SAT { get; set; } = default;

        public List<double> KS { get; set; } = default;

        public string BDMetaData { get; set; } = default;

        public string AirDryMetaData { get; set; } = default;

        public string LL15MetaData { get; set; } = default;

        public string DULMetaData { get; set; } = default;

        public string SATMetaData { get; set; } = default;

        public string KSMetaData { get; set; } = default;

        public Water()
        { }
    }
}
