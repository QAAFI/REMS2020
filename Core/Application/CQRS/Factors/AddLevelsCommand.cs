using MediatR;
using Models;
using Models.Factorial;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using REMSFactor = Rems.Domain.Entities.Factor;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Generates an APSIM Permutation model for an experiment
    /// </summary>
    public class AddLevelsCommand : IRequest
    {
        public Factor Factor { get; set; }
    }

    public class AddLevelsCommandHandler : IRequestHandler<AddLevelsCommand>
    {
        private readonly IRemsDbContext _context;
        private readonly IFileManager _file;
        public AddLevelsCommandHandler(IRemsDbContext context, IFileManager file)
        {
            _context = context;
            _file = file;
        }

        public Task<Unit> Handle(AddLevelsCommand request, CancellationToken token)
            => Task.Run(() => Handler(request, token));

        private Unit Handler(AddLevelsCommand request, CancellationToken token)
        {
            var factor = _context.Factors.First(f => f.Name == request.Factor.Name);

            foreach (var level in factor.Level)
                request.Factor.Children.Add(new CompositeFactor{ Name = level.Name });

            return Unit.Value;
        }
    }
}