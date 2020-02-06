using MediatR;
using Microsoft.AspNetCore.Components;
using Rems.Application.Experiments.Queries.GetExperimentDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rems.UI.Pages.Trials.TrialDetail
{
    public class TrialDetailBase : ComponentBase
    {
        [Inject] public IMediator Mediator { get; set; }
        [Parameter] public int TrialId { get; set; }
        public ExperimentDetailVm Experiment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Experiment = await this.Mediator.Send(new GetExperimentDetailQuery { Id = TrialId });
            //this.Model = new WebApiStyleViewModel(selections.Roles, selections.Names);
        }
    }
}
