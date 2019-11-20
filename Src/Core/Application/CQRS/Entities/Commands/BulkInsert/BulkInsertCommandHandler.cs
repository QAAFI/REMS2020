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
    public class BulkInsertCommandHandler : IRequestHandler<BulkInsertCommand>
    {
        private readonly IRemsDbContext _context;

        public BulkInsertCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public async Task<MediatR.Unit> Handle(BulkInsertCommand request, CancellationToken token)
        {
            foreach (DataTable table in request.Data.Tables)
            {
                IEnumerable<IEntity> entities;

                if (table.TableName == "PlotData")
                    entities = ImportPlotData(table);
                else
                    entities = ImportTableData(table, request.TableMap);

                _context.AddRange(entities);
                await _context.SaveChangesAsync(token);
            }

            return MediatR.Unit.Value;
        }

        private IEnumerable<IEntity> ImportTableData(DataTable table, IPropertyMap map)
        {
            var rows = table.Rows.Cast<DataRow>();
            var columns = table.Columns.Cast<DataColumn>();

            var pairs = rows.Select(r =>
                        new Dictionary<string, object>(columns.Select(c =>
                            new KeyValuePair<string, object>(c.ColumnName, r.Field<object>(c)))));

            var type = Type.GetType(map[table.TableName]);

            return pairs.Select(d =>
            {
                //CreateInstance is likely to cause an exception on any typo - use northwind example for error handling 
                IEntity entity = Activator.CreateInstance(type) as IEntity;
                entity.Update(d);
                return entity;
            });
        }

        private IEnumerable<IEntity> ImportPlotData(DataTable table)
        {
            var kvps = table.Columns.Cast<DataColumn>().Select(c =>
                new KeyValuePair<DataColumn, Trait>(c, _context.Traits.FirstOrDefault(t => t.Name == c.ColumnName)));

            var map = new Dictionary<DataColumn, Trait>(kvps);

            var rows = table.Rows.Cast<DataRow>();

            var data = rows.SelectMany(r => map.Select(k => 
            {               
                var trait = k.Value;
                var column = k.Key;

                if (trait == null) return null;

                return new PlotData()
                {
                    PlotId = Convert.ToInt32(r.Field<double>("Plot")),
                    PlotDataDate = r.Field<DateTime>("date"),
                    Sample = r.Field<string>("Sample"),
                    TraitId = trait.TraitId,
                    Value = (double?)r.Field<object>(column),
                    UnitId = trait.UnitId
                };
            }));

            return data.Where(d => d != null && d.Value != null);
        }
    }
}
