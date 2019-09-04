using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF.Organs
{
    public class SorghumLeaf : Node
    {
        public List<object> Culms { get; set; } = default;

        public string StartLive { get; set; } = default;

        public double Albedo { get; set; } = default;

        public double Gsmax350 { get; set; } = default;

        public double R50 { get; set; } = default;

        public double BaseHeight { get; set; } = default;

        public double Width { get; set; } = default;

        public double FRGR { get; set; } = default;

        public double PotentialEP { get; set; } = default;

        public string LightProfile { get; set; } = default;

        public double DltLAI { get; set; } = default;

        public double dltPotentialLAI { get; set; } = default;

        public double dltStressedLAI { get; set; } = default;

        public double LAI { get; set; } = default;

        public double SLN { get; set; } = default;

        public double CoverGreen { get; set; } = default;

        public double CoverDead { get; set; } = default;

        public double Height { get; set; } = default;

        public double WaterDemand { get; set; } = default;

        public bool MicroClimatePresent { get; set; } = default;

        public double BiomassRUE { get; set; } = default;

        public double BiomassTE { get; set; } = default;

        public string LeafInitialisationStage { get; set; } = default;

        public double KDead { get; set; } = default;

        public double LAIDead { get; set; } = default;

        public double NitrogenPhotoStress { get; set; } = default;

        public double NitrogenPhenoStress { get; set; } = default;

        public double PhosphorusStress { get; set; } = default;

        public double FinalLeafNo { get; set; } = default;

        public double SowingDensity { get; set; } = default;

        public double senRadnCrit { get; set; } = default;

        public double senLightTimeConst { get; set; } = default;

        public double frostKill { get; set; } = default;

        public double senThreshold { get; set; } = default;

        public double senWaterTimeConst { get; set; } = default;

        public double LossFromExpansionStress { get; set; } = default;

        public double SenescedLai { get; set; } = default;

        public double DltRetranslocatedN { get; set; } = default;

        public double DltSenescedN { get; set; } = default;

        public double DltSenescedLaiN { get; set; } = default;

        public double DltSenescedLai { get; set; } = default;

        public double DltSenescedLaiLight { get; set; } = default;

        public double DltSenescedLaiWater { get; set; } = default;

        public double DltSenescedLaiFrost { get; set; } = default;

        public double DMSupply { get; set; } = default;

        public double NSupply { get; set; } = default;

        public double DMDemand { get; set; } = default;

        public double NDemand { get; set; } = default;

        public double potentialDMAllocation { get; set; } = default;

        public SorghumLeaf()
        { }
    }
}
