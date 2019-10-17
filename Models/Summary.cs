using System;

namespace Models
{
    /// <summary>
    /// A summary of the entire simulation
    /// </summary>
    public class Summary : ApsimNode
    {
        public bool CaptureErrors { get; set; } = true;

        public bool CaptureWarnings { get; set; } = true;

        public bool CaptureSummaryText { get; set; } = true;

        public Summary()
        {
            Name = "summaryfile";
        }
    }   
}
