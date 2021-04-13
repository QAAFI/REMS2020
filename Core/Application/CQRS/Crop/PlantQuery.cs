using System;
using Models.PMF;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Requests an Apsim Plant model from the specified experiment
    /// </summary>
    public class PlantQuery : ContextQuery<Plant>
    {
        public class Handler : BaseHandler<PlantQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <summary>
        /// The experiment to generate the plant model from
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        protected override Plant Run()
        {
            var crop = _context.Experiments.Find(ExperimentId).Crop;

            var valid = Report.ValidateItem(crop.Name, nameof(Plant.Name));

            Report.CommitValidation(nameof(Plant), !valid);

            var plant = new Plant
            {
                PlantType = crop.Name,
                Name = crop.Name,
                ResourceName = crop.Name
            };

            return plant;
        }
    }
}
