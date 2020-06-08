using MediatR;

using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommandHandler : IRequestHandler<BulkInsertCommand>
    {
        private readonly IRemsDbFactory _factory;

        public BulkInsertCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<MediatR.Unit> Handle(BulkInsertCommand request, CancellationToken token)
        {
            foreach (DataTable table in request.Data.Tables)
            {
               IEntity[] entities;

                if (table.TableName == "PlotData")
                    entities = ImportPlotData(table);
                else
                    entities = ImportTableData(table, request.TableMap);

                _factory.Context.AddRange(entities);
                //await _context.SaveChangesAsync(token);
                _factory.Context.SaveChanges();
            }

            return MediatR.Unit.Value;
        }

        private IEntity[] ImportTableData(DataTable table, IPropertyMap map)
        {
            var rows = table.Rows.Cast<DataRow>();
            var columns = table.Columns.Cast<DataColumn>();

            var pairs = rows.Select(r =>
                        new Dictionary<string, object>(columns.Select(c =>
                            new KeyValuePair<string, object>(c.ColumnName, r[c]))));
            // TODO: Look into r.Field<object>(c) method to replace r[c]. It may eliminate the need for the ConvertNullableObject 
            // function used later in the import process, simplifying the procedure / debugging.

            //if (!map.HasMapping(table.TableName)) throw new Exception("The imported table is not mapped to any known destination.");
            //var tableName = map.MappedFrom(table.TableName);
            
            var typeName = "Rems.Domain.Entities." + table.TableName.Remove(table.TableName.Length - 1) + ", Rems.Domain";
            var type = Type.GetType(typeName);
            var result = pairs.Select(d => Test(d, type));
            return result.ToArray();
        }

        private IEntity Test(Dictionary<string, object> d, Type type)
        {
            //CreateInstance is likely to cause an exception on any typo - use northwind example for error handling
            IEntity entity = Activator.CreateInstance(type) as IEntity;
            entity.Update(d);
            return entity;
        }

        private IEntity[] ImportPlotData(DataTable table)
        {
            var kvps = table.Columns.Cast<DataColumn>().Select(c =>
                new KeyValuePair<DataColumn, Trait>(c, _factory.Context.Traits.FirstOrDefault(t => t.Name == c.ColumnName)));

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

            return data.Where(d => d != null && d.Value != null).ToArray();
        }
    }
}
