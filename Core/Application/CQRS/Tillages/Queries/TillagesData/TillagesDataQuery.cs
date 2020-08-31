using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System;
using System.Data;
using System.Collections.Generic;
using Rems.Application.Common;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class TillagesDataQuery : IRequest<SeriesData>
    {
        public int TreatmentId { get; set; }
    }
}
