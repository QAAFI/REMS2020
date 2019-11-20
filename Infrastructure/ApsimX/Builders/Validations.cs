using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

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
        public static Folder BuildValidations(IMediator mediator, string path)
        {
            var validations = new Folder() { Name = "Validations" };

            validations.Children.Add(BuildCombinedResults());

            foreach (var experiment in dbContext.Experiments)
            {
                var simulations = experiment.Treatments.Select(t => NewSorghumSimulation(t, dbContext));
                var folder = new Folder() { Name = experiment.Name };
                folder.Children.AddRange(simulations);
                validations.Children.Add(folder);

                GenerateMetFile(dbContext, experiment.MetStation, path);
            }

            var predictedObserved = new Dictionary<string, IEnumerable<string>>()
            {
                { "Biomass", new List<string>(){ "BiomassWt", "GrainGreenWt" } },
                { "StemLeafWt", new List<string>(){ "BiomassWt", "StemGreenWt", "LeafGreenWt" } },
                { "Grain", new List<string>(){ "GrainNo", "GrainGreenNConc" } },
                { "LAI", new List<string>(){ "LAI" } },
                { "LeafNo", new List<string>(){ "LeafNo" } },
                { "Stage", new List<string>(){ "Stage" } },
                { "BiomassN", new List<string>(){ "BiomassN", "GrainGreenN" } },
                { "SteamLeafN", new List<string>(){ "BiomassN", "StemGreenN", "LeafGreenN" } },
                { "SLN", new List<string>(){ "NO3", "SLN" } },
            };

            validations.Children.Add(BuildPanel(predictedObserved, "PredictedObserved.cs.txt"));

            return validations;
        }

    }
}
