using System;
using System.Threading.Tasks;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Models;
using Models.Core;

namespace Rems.Infrastructure.ApsimX
{
    public class ReportTemplate : ITemplate
    {
        readonly IQueryHandler Handler;

        readonly IFileManager Manager;

        readonly int ID;

        public ReportTemplate(int id, IQueryHandler handler, IFileManager manager)
        {
            ID = id;
            Handler = handler;
            Manager = manager;
        }

        public IModel Create() => AsyncCreate().Result;

        public async Task<IModel> AsyncCreate()
        {
            var crop = await Handler.Query(new CropQuery { ExperimentId = ID });
            var info = Manager.GetFileInfo($"{crop}Daily") ?? Manager.GetFileInfo("DefaultDaily");

            return JsonTools.LoadJson<Report>(info);
        }
    }
}
