using System;
using System.Threading.Tasks;
using Rems.Application.Common.Interfaces;
using Models.Core;
using Models;

namespace Rems.Infrastructure.ApsimX
{
    public class PanelGraphTemplate : ITemplate
    {
        readonly IFileManager manager = FileManager.Instance;

        public IModel Create()
        {
            var file = manager.GetFileInfo("PredictedObserved.apsimx");
            return JsonTools.LoadJson<GraphPanel>(file);
        }

        public async Task<IModel> AsyncCreate() => await Task.Run(Create);
    }
}
