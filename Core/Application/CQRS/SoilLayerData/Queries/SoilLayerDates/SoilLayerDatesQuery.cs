using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System;
using System.Data;
using System.Collections.Generic;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class SoilLayerDatesQuery : IRequest<DateTime[]>
    {
        public int TreatmentId { get; set; }
    }
}
