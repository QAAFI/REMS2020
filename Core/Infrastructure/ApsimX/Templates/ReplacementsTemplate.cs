using System;
using System.Threading.Tasks;
using Rems.Application.Common.Interfaces;
using Models.Core;

namespace Rems.Infrastructure.ApsimX
{
    public class ReplacementsTemplate : ITemplate
    {
        readonly IFileManager manager = FileManager.Instance;

        public IModel Create()
        {
            var info = manager.GetFileInfo("SorghumReplacements");
            return JsonTools.LoadJson<Replacements>(info);
        }

        public async Task<IModel> AsyncCreate() => await Task.Run(Create);
    }
}
