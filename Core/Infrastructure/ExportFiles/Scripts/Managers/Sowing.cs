using APSIM.Shared.Utilities;
using Models.PMF;
using Models.Core;
using System;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Link]
        private Zone paddock;

        [Link]
        private Clock clock;

        [Link]
        private IPlant crop;

        [Description("Enter sowing date (dd/mm/yyyy) : ")]
        public DateTime Date { get; set; }

        [Description("Enter sowing density  (plants/m2) : ")]
        public double Density { get; set; }

        [Description("Enter sowing depth  (mm) : ")]
        public double Depth { get; set; }

        [Description("Enter cultivar : ")]
        [Display(Type = DisplayType.CultivarName)]
        public string Cultivar { get; set; }

        [Description("Enter row spacing (mm) : ")]
        public double RowSpacing { get; set; }

        [Description("Enter skip row configuration : ")]
        public RowConfigurationType RowConfiguration { get; set; }

        [Description("Enter Fertile Tiller No. : ")]
        public double Ftn { get; set; }

        public enum RowConfigurationType 
        {
            solid, single, twin /*replaces double*/
        }

        [EventSubscribe("DoManagement")]
        private void OnDoManagement(object sender, EventArgs e)
        {
            if (clock.Today == Date)
            {
                double population = Density * paddock.Area;
                crop.Sow(Cultivar, population, Depth, RowSpacing, budNumber: Ftn, rowConfig: (double)RowConfiguration + 1);
            }
        }
    }
}
