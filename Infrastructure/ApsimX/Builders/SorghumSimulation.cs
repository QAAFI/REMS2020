using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Models;
using Models.Core;
using Models.Core.ApsimFile;
using Models.Core.Run;
using Models.Graph;
using Models.PMF;
using Models.PostSimulationTools;
using Models.Report;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Storage;
using Models.Surface;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Simulation> BuildSorghumSimulation(int treatmentId)
        {
            var simulation = new Simulation()
            {
                //Name = $"{treatment.Experiment.Crop.Name}_{treatment.Experiment.Name}_{treatment.TreatmentId}"
            };

            simulation.Children.Add(new Clock()
            {
                Name = "Clock",
                //StartDate = (DateTime)treatment.Experiment.BeginDate,
                //EndDate = (DateTime)treatment.Experiment.EndDate
            });

            simulation.Children.Add(new Summary()
            {
                Name = "Summary"
            });

            simulation.Children.Add(new Weather()
            {
                Name = "Weather",
                //FileName = treatment.Experiment.MetStation?.Name + ".met"
            });

            simulation.Children.Add(new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            });

            simulation.Children.Add(new SoilArbitrator() { Name = "SoilArbitrator" });

            simulation.Children.Add(await BuildField(1));

            return simulation;
        }

    }
}
