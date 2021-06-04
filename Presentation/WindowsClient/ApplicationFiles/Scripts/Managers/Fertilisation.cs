using Models.PMF;
using Models.Core;
using System;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Link] Clock Clock;
        [Link] Fertiliser Fertiliser;
        [Link] Summary Summary; 

        public double CumulativeSowFert { get; set; } 

        [Description("Type of fertiliser to apply? ")]
        public Fertiliser.Types FertiliserType { get; set; }

        [Description("Amount of fertiliser to be applied (kg/ha)")]
        public double Amount { get; set; }
        
        [EventSubscribe("Sowing")]
        private void OnSowing(object sender, EventArgs e)
        {
                Fertiliser.Apply(Amount: Amount, Type: FertiliserType);
                CumulativeSowFert += Amount;
                Summary.WriteMessage(this, "Fertilising!! applying " + Amount.ToString()); 
        }
        
    }
}