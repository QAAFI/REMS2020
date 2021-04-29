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
    public class ManagersQuery : IRequest<Folder>
    { }

    public class ManagersQueryHandler : IRequestHandler<ManagersQuery, Folder>
    {
        private readonly IFileManager _manager;

        public ManagersQueryHandler(IFileManager manager)
        {
            _manager = manager;
        }

        public Task<Folder> Handle(ManagersQuery request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Folder Handler(ManagersQuery request, CancellationToken token)
        {
            var folder = new Folder 
            { 
                Name = "Managers",
                Children = new List<IModel>
                {
                    new Manager { Name = "Sowing", Code = _manager.GetFile("Sowing", "cs") },
                    new Manager { Name = "Irrigation" },
                    new Manager { Name = "Fertilisation", Code = _manager.GetFile("Fertilisation", "cs") },
                    new Manager { Name = "Harvesting", Code = _manager.GetFile("Harvest", "cs") }
                }
            };

            return folder;
        }
    }
}