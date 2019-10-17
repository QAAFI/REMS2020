using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Surface
{
    public class SurfaceOrganicMatter : ApsimNode
    {
        public string InitialResidueName { get; set; } = default;

        public string InitialResidueType { get; set; } = default;

        public double InitialResidueMass { get; set; } = default;

        public double InitialStandingFraction { get; set; } = default;

        public double InitialCPR { get; set; } = default;

        public double InitialCNR { get; set; } = default;

        public double FractionFaecesAdded { get; set; } = default;

        public string ResourceName { get; set; } = default;

        public SurfaceOrganicMatter()
        { }
    }
}
