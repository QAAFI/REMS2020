using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF.Organs
{
    public class ReproductiveOrgan : Node
    {
        public double GrowthRespiration { get; set; } = default;

        public double MaintenanceRespiration { get; set; } = default;

        public double DMDemand { get; set; } = default;

        public double NDemand { get; set; } = default;

        public double DMSupply { get; set; } = default;

        public double NSupply { get; set; } = default;

        public string RipeStage { get; set; } = default;

        public double potentialDMAllocation { get; set; } = default;

        public Biomass Live { get; set; } = default;

        public Biomass Dead { get; set; } = default;


    }
}
