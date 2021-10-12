using System;

using Models;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM report model for an experiment
    /// </summary>
    public class DailyReportQuery : ContextQuery<Report>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<DailyReportQuery> 
        { 
            public Handler(IRemsDbContextFactory factory) : base(factory) { } 
        }

        /// <inheritdoc/>
        protected override Report Run()
        {
            string crop = _context.Experiments.Find(ExperimentId).Crop.Name;
            
            var report = new Report
            {
                Name = "DailyReport",
                VariableNames = new string[]
                {
                    "[Clock].Today.Date as Date",
                    "[Clock].Today.Year as Year",
                    "[Clock].Today.DayOfYear as DoY",                    
                    "",
                    "// Biomass",
                    $"[{crop}].AboveGround.Wt as BiomassWt",
                    $"[{crop}].Grain.Live.Wt as GrainGreenWt",
                    "",
                    "// StemLeafWt",
                    $"[{crop}].Stem.Live.Wt as StemGreenWt",
                    $"[{crop}].Leaf.Live.Wt as LeafGreenWt",
                    "",
                    "// Grain",
                    $"[{crop}].Grain.NumberFunction as GrainNo",
                    $"[{crop}].Grain.Live.NConc * 100 as GrainGreenNConc",
                    "",
                    "// LAI",
                    "[Leaf].LAI as LAI",
                    "//[Leaf].SenescedLai as SLAI",
                    "",
                    "// LeafNo",
                    "//[Leaf].LeafNo as LeafNo",
                    "",
                    "// Stage",
                    $"[{crop}].Phenology.Stage as Stage",
                    "",
                    "// BiomassN",
                    $"[{crop}].AboveGround.N as BiomassN",
                    $"[{crop}].Grain.Live.N as GrainGreenN",
                    "",
                    "// StemLeafN",
                    $"[{crop}].Stem.Live.N as StemGreenN ",
                    $"[{crop}].Leaf.Live.N as LeafGreenN",
                    "",
                    "// SLN",
                    $"//sum([{crop}Soil].SoilNitrogen.NO3.kgha) as NO3",
                    "//[Leaf].SLN as SLN"
                },
                EventNames = new string[] { "[Clock].DoReport" }
            };

            return report;
        }
    }
}
