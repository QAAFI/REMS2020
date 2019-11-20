using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Mappings;
using Rems.Domain;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommand : IRequest
    {
        public DataSet Data { get; set; }

        public IPropertyMap TableMap { get; set; }

        public class Handler : IRequestHandler<BulkInsertCommand>
        {
            private readonly IRemsDbContext _context;

            public Handler(IRemsDbContext context, IMediator mediator)
            {
                _context = context;
            }

            public async Task<MediatR.Unit> Handle(BulkInsertCommand request, CancellationToken token)
            {
                foreach (DataTable table in request.Data.Tables)
                {

                    var rows = table.Rows.Cast<DataRow>();
                    var columns = table.Columns.Cast<DataColumn>();

                    var pairs = rows.Select(r =>
                                new Dictionary<string, object>(columns.Select(c =>
                                    new KeyValuePair<string, object>(c.ColumnName, r.Field<object>(c)))));

                    var type = Type.GetType(request.TableMap[table.TableName]);
                    var entities = pairs.Select(d =>
                    {
                        //CreateInstance is likely to cause an exception on any typo - use northwind example for error handling 
                        IEntity entity = Activator.CreateInstance(type) as IEntity;
                        entity.Update(d);
                        return entity;
                    });

                    _context.AddRange(entities);
                    await _context.SaveChangesAsync(token);
                }

                return MediatR.Unit.Value;
            }
        }
    }
}
