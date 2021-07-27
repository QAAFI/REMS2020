using System;
using Models.Soils;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Soil model for an experiment
    /// </summary>
    public class SoilQuery : ContextQuery<Soil>
    {   
        /// <summary>
        /// The source experiment
        /// </summary>
        public int ExperimentId { get; set; }

        public Markdown Report { get; set; }

        /// <inheritdoc/>
        public class Handler : BaseHandler<SoilQuery>
        {
            public Handler(IRemsDbContextFactory factory) : base(factory) { }
        }

        /// <inheritdoc/>
        protected override Soil Run()
        {
            var field = _context.Experiments.Find(ExperimentId).Field;

            var valid = Report.ValidateItem(field.Soil.SoilType, nameof(Soil.Name))
                & Report.ValidateItem(field.Latitude.GetValueOrDefault(), nameof(Soil.Latitude))
                & Report.ValidateItem(field.Longitude.GetValueOrDefault(), nameof(Soil.Longitude))
                & Report.ValidateItem(field.Site.Name, nameof(Soil.Site))
                & Report.ValidateItem(field.Site.Region.Name, nameof(Soil.Region));

            Report.CommitValidation(nameof(Soil), !valid);

            var soil = new Soil
            {
                Name = field.Soil.SoilType,
                Latitude = field.Latitude.GetValueOrDefault(),
                Longitude = field.Longitude.GetValueOrDefault(),
                Site = field.Site.Name,
                Region = field.Site.Region.Name
            };

            return soil;
        }
    }
}
