using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using Unit = MediatR.Unit;

namespace Rems.Application.CQRS
{
    public class InsertOperationsTableCommand : IRequest
    {
        public DataTable Table { get; set; }

        public Type Type { get; set; }
    }

    public class InsertOperationsTableCommandHandler : IRequestHandler<InsertOperationsTableCommand>
    {
        private readonly IRemsDbContext _context;

        public InsertOperationsTableCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(InsertOperationsTableCommand request, CancellationToken cancellationToken) => Task.Run(() => Handler(request));

        private Unit Handler(InsertOperationsTableCommand request)
        {
            var infos = request.Table.Columns.Cast<DataColumn>()
                .Skip(2)
                .Select(c => c.FindProperty())
                .Where(i => i != null)
                .ToList();

            var info = request.Type.GetProperty("TreatmentId");

            var entities = new List<IEntity>();

            foreach (DataRow row in request.Table.Rows)
            {
                // Assume that in a 'treatment' row, the first column is the experiment ID
                // and the second column is the treatment name

                var id = row[0].ConvertDBValue<int>();
                var name = row[1].ToString();

                var treatments = _context.Treatments.AsNoTracking()
                    .Where(t => t.ExperimentId == id);

                if (name.ToLower() == "all")
                {
                    foreach (var t in treatments)
                    {
                        var result = row.ToEntity(_context, request.Type, infos.ToArray());
                        result.SetValue(info, t.TreatmentId);
                        _context.Attach(result);
                    }
                }
                else
                {
                    var treatment = treatments.Where(t => t.Name == name).FirstOrDefault();

                    // TODO: This is a lazy approach that simply skips bad data, try to find a better solution
                    if (treatment is null) continue;

                    var result = row.ToEntity(_context, request.Type, infos.ToArray());
                    result.SetValue(info, treatment.TreatmentId);
                    _context.Attach(result);
                }

                EventManager.InvokeProgressIncremented();
            }

            _context.SaveChanges();

            return Unit.Value;
        }
    }
}
