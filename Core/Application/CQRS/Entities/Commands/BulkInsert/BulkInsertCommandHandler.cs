using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommandHandler : IRequestHandler<BulkInsertCommand, bool>
    {
        private readonly IRemsDbFactory _factory;

        public BulkInsertCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
        }

        public async Task<bool> Handle(BulkInsertCommand request, CancellationToken token)
        {
            foreach (DataTable table in request.Data.Tables)
            {               
                if (table.TableName == "Notes") continue;

                var types = _factory.Context.Model.GetEntityTypes();
                var filtered = types.Where(e => table.TableName.Contains(e.ClrType.Name));

                if (filtered.Count() == 1)
                {
                    AddToTable(filtered.Single(), table);
                }
                else
                {
                    var args = new ItemNotFoundArgs() { Name = table.TableName };

                    if (filtered.Count() == 0) 
                        args.Options = types.Select(t => t.Name).ToArray();
                    else
                        args.Options = filtered.Select(t => t.Name).ToArray();

                    EventManager.InvokeItemNotFound(null, args);

                    if (args.Cancelled) continue;

                    AddToTable(filtered.First(t => t.Name == args.Selection), table);
                }                
            }

            return true;
        }

        private void AddToTable(IEntityType entity, DataTable data)
        {
            var infos = data.Columns.Cast<DataColumn>().Select(c => GetColumnInfo(entity, c)).ToList();

            var entities = data.Rows.Cast<DataRow>().Select(r => DataRowToEntity(r, entity.ClrType, infos)).ToArray();            

            //if (data.TableName == "PlotData")
            //    entities = ImportPlotData(data);
            //else
            //    entities = ImportTableData(data);
            
            _factory.Context.AddRange(entities);
            _factory.Context.SaveChanges();
        }

        private PropertyInfo GetColumnInfo(IEntityType entity, DataColumn col)
        {
            if (entity.ClrType.GetProperty(col.ColumnName) is PropertyInfo i && i != null)
            {
                return i;
            }
            else if (col.ColumnName == entity.ClrType.Name)
            {
                // Guess that there is a column/property called Name.
                // If not, defer back to user selection
                col.ColumnName = "Name";
                return GetColumnInfo(entity, col);
            }
            else
            {
                var options = entity.ClrType.GetProperties()
                    .Where(p => !(p.PropertyType is ICollection))
                    .Select(e => e.Name)
                    .ToList();

                col.ColumnName = GetColumnName(col, options.ToArray());
                
                if (col.ColumnName == null) 
                    return null;
                else
                    return GetColumnInfo(entity, col);
            }
        }

        private string GetColumnName(DataColumn col, string[] options)
        {
            var args = new ItemNotFoundArgs()
            {
                Name = col.ColumnName,
                Options = options
            };
            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled) return null;
            else return args.Selection;
        }

        private IEntity DataRowToEntity(DataRow row, Type type, IEnumerable<PropertyInfo> props)
        {           
            IEntity entity = Activator.CreateInstance(type) as IEntity;

            foreach (var prop in props)
            {
                var value = row[prop.Name];

                // Entity will use default values for null entries
                if (value is DBNull) continue;

                // Check if the property is owned by a different entity type,
                // if yes, search the context for the DbSet which owns it
                var t = prop.PropertyType;
                if (t.IsClass && t != typeof(string))
                {
                    var ps = t.GetProperties();
                    var query = _factory.Context.Query(t);//.Cast<IEntity>();
                    //var result = query.FirstOrDefault(e => e.HasValue(value, ps));

                    foreach(IEntity e in query)
                    {
                        if (e.HasValue(value, ps))
                        {
                            prop.SetValue(entity, e);
                            break;
                        }
                    }

                    //prop.SetValue(entity, result);
                }
                else
                {
                    prop.SetValue(entity, value);
                }                
            }

            return entity;
        }

        private IEntity[] ImportTableData(DataTable table)
        {
            var rows = table.Rows.Cast<DataRow>();              // The rows in the data table
            var columns = table.Columns.Cast<DataColumn>();     // The columns in the data table

            var pairs = rows.Select(r => columns.ToDictionary(c => c.ColumnName, c => r[c]));

            foreach (var r in rows)
            {
                var items = columns.ToDictionary(c => c.ColumnName, c => r[c]);
            }

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
            var map = table.Columns.Cast<DataColumn>().ToDictionary(c => c, c => _factory.Context.Traits.FirstOrDefault(t => t.Name == c.ColumnName));

            var rows = table.Rows.Cast<DataRow>();

            var data = rows.SelectMany(r => map.Select(k => 
            {               
                var trait = k.Value;
                var column = k.Key;

                if (trait == null) return null;            

                return new PlotData()
                {
                    PlotId = Convert.ToInt32(r["Plot"]),
                    PlotDataDate = (DateTime?)r["date"],
                    Sample = (string)r["Sample"],
                    TraitId = trait.TraitId,
                    Value = (double?)r[column],
                    UnitId = trait.UnitId
                };
            }));

            return data.Where(d => d != null && d.Value != null).ToArray();
        }
    }
}
