using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF.Phen
{
    public class EmergingPhase : Node
    {
        public double ShootLag { get; set; } = default;

        public double ShootRate { get; set; } = default;

        public string Start { get; set; } = default;

        public string End { get; set; } = default;

        public double TTForTimeStep { get; set; } = default;

        public EmergingPhase()
        { }
    }
}
