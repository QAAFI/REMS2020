using MediatR;
using Microsoft.AspNetCore.Components;
using Rems.Application.Experiments.Queries.GetExperimentList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rems.UI.Pages.Trials
{
    public class TrialsBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }
        public bool LoadingData { get; set; } = true;

        public ExperimentsListVm ViewModel { get; private set; }
        protected override async Task OnInitializedAsync()
        {
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ViewModel = await this.Mediator.Send(new GetExperimentsListQuery());
                LoadingData = false;
                this.StateHasChanged();
            }
        }
    }
}
