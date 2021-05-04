using MediatR;
using Models;
using Models.Core;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Rems.Application.CQRS
{
    public class ManagersQuery : ContextQuery<Folder>
    {
        private static IFileManager _manager;

        public int ExperimentId { get; set; }

        public class Handler : BaseHandler<ManagersQuery>
        { 
            public Handler(IRemsDbContextFactory factory, IFileManager manager) : base(factory)
            {
                _manager = manager;
            }
        }

        protected override Folder Run()
        {            
            //var irrig = new Manager { Name = "Irrigation" },
            //var fert = new Manager { Name = "Fertilisation", Code = _manager.GetFile("Fertilisation", "cs") },
            var harvest = new Manager { Name = "Harvesting", Code = _manager.GetFile("Harvest", "cs") };            

            var folder = new Folder 
            { 
                Name = "Managers",
                Children = new List<IModel> { GetSowing(), harvest }
            };

            return folder;
        }

        private Manager GetSowing()
        {
            var exp = _context.Experiments.Find(ExperimentId);

            var sowing = new Manager { Name = "Sowing", Code = _manager.GetFile("Sowing", "cs") };

            sowing.Parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Date", exp.Sowing.Date.ToString("dd/MM/yyyy")),
                new KeyValuePair<string, string>("Density", exp.Sowing.Population.ToString()),
                new KeyValuePair<string, string>("Depth", exp.Sowing.Depth.ToString()),
                new KeyValuePair<string, string>("Cultivar", exp.Sowing.Cultivar?.Replace('/', 'x')),
                new KeyValuePair<string, string>("RowSpacing", exp.Sowing.RowSpace.ToString())
            };

            return sowing;
        }
    }
}