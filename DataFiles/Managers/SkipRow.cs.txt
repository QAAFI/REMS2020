using System;
using Models.Core;
using Models.PMF;

using APSIM.Shared.Utilities;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Description("Enter sowing date (dd/mm/yyyy) : ")]
        public DateTime Date { get; set; }

        [Description("Enter sowing density  (plants/m2) : ")]
        public double Density { get; set; }

        [Description("Enter sowing depth  (mm) : ")]
        public double Depth { get; set; }

        [Description("Enter cultivar : ")]
        [Display(Type = DisplayType.CultivarName)]
        public string Cultivar { get; set; } // QL41xQL36

        [Description("Enter row spacing (m) : ")]
        public double RowSpacing { get; set; }

        [Description("Enter skip row configuration : ")]
        public RowConfigurationType RowConfiguration { get; set; }

        [Description("Enter Fertile Tiller No. : ")]
        public double Ftn { get; set; }

        public enum RowConfigurationType 
        {
            solid, 
            single, 
            _double /*replaces double*/
        }

        [Link]
        private Zone paddock;

        [Link]
        private Clock clock;

        [Link]
        private IPlant crop;

        [EventSubscribe("DoManagement")]
        private void OnDoManagement(object sender, EventArgs e)
        {
            if (clock.Today == Date /* && isFallow */)
            {
                double population = Density * paddock.Area;
                crop.Sow(Cultivar, population, Depth, RowSpacing, budNumber: Ftn, rowConfig: (double)RowConfiguration + 1);
                /*
                if (paddock_is_fallow() = 1 and today = date('[date]')) then
                    [crop] sow plants =[density], sowing_depth = [depth], cultivar = [cultivar], row_spacing = [row_spacing], skip = [RowConfiguration], tiller_no_fertile = [ftn] ()
                endif
                */
            }
        }
    }
}
