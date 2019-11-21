using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Models;
using Models.Core;

using Rems.Application.Queries;
using Rems.Infrastructure.Met;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public async Task<Folder> BuildValidations(string path)
        {
            var validations = new Folder() { Name = "Validations" };

            validations.Children.Add(BuildCombinedResults());

            foreach (var experiment in await _mediator.Send(new ExperimentsQuery()))
            {
                var folder = new Folder() { Name = experiment.Name };

                foreach (var treatment in experiment.Treatments)
                {
                    var simulation = await BuildSorghumSimulation(treatment);
                    folder.Children.Add(simulation);
                }                
                validations.Children.Add(folder);

                MetWriter.GenerateMetFile(_mediator, experiment.MetStationId, path);
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
