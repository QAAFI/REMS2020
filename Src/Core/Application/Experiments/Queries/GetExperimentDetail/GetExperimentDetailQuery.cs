using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments.Queries.GetExperimentDetail
{
    public class GetExperimentDetailQuery : IRequest<ExperimentDetailVm>
    {
        public string Id { get; set; }
    }
}
