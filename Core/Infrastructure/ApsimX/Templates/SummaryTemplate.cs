using System;
using System.Threading.Tasks;
using Rems.Application.Common.Interfaces;
using Models.Core;
using Models;

namespace Rems.Infrastructure.ApsimX
{
    public class SummaryTemplate : ITemplate
    {
        readonly string text;

        public SummaryTemplate(string text)
        {
            this.text = text;
        }

        public IModel Create() => new Memo { Name = "ExportSummary", Text = text };       

        public async Task<IModel> AsyncCreate() => await Task.Run(Create);
    }
}
