using APSIM.Shared.Utilities;
using Models.Utilities;
using Models.Soils;
using Models.PMF;
using Models.Core;
using System;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Description("Sowing date (d-mmm)")]
        public string SowDate { get; set; }

        [Description("Crop")]
        public IPlant Crop { get; set; }

        [Description("Sowing depth (mm)")]
        public double SowingDepth { get; set; }

        [Description("Cultivar to be sown")]
        [Display(Type = DisplayType.CultivarName)]
        public string CultivarName { get; set; }       

        [Description("Row spacing (mm)")]
        public double RowSpacing { get; set; }

        [Description("Plant population (/m2)")]
        public double Population { get; set; }       

        [Link] Clock Clock;

        [EventSubscribe("DoManagement")]
        private void OnDoManagement(object sender, EventArgs e)
        {
            if (DateUtilities.WithinDates(SowDate, Clock.Today, SowDate))
            {
                Crop.Sow(
                    population: Population, 
                    cultivar: CultivarName, 
                    depth: SowingDepth, 
                    rowSpacing: RowSpacing);    
            }
        
        }
        
    }
}
