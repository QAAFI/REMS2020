using System;
using System.Collections.Generic;
using System.Text;

namespace ApsimService
{
    public class SimulationProperties
    {
        public SimulationProperties()
        {
            ReportVariables = new List<string>();
        }
        public string SimulationName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MetFile { get; set; }
        public string Crop { get; set; }
        public string CropCultivar { get; set; }

        public DateTime SowingDate { get; set; }
        public int SowingDensity { get; set; }
        public int SowingDepth { get; set; }
        public int RowSpacing { get; set; }
        public string RowConfiguration { get; set; }
        public double FTN { get; set; }

        public string ReportFile { get; set; }
        public List<string> ReportVariables { get; set; }
    }
}
