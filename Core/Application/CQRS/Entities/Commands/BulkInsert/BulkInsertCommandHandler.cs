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
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommandHandler : IRequestHandler<BulkInsertCommand, bool>
    {
        private readonly IRemsDbFactory _factory;
        private readonly IRemsDbContext context;

        public BulkInsertCommandHandler(IRemsDbFactory factory)
        {
            _factory = factory;
            context = _factory.Context;
        }

        public async Task<bool> Handle(BulkInsertCommand request, CancellationToken token)
        {
            foreach (DataTable table in request.Data.Tables)
            {               
                if (table.TableName == "Notes") continue;
                
                var entity = FindEntity(table.TableName);
                
                if (entity == null) continue;
                
                ImportTable(entity, table);          
            }

            return true;
        }

        private IEntityType FindEntity(string name)
        {
            var types = context.Model.GetEntityTypes();
            var filtered = types.Where(e => name.Contains(e.ClrType.Name));

            if (filtered.Count() == 1) return filtered.Single();

            var args = new ItemNotFoundArgs() { Name = name };

            if (filtered.Count() == 0)
                args.Options = types.Select(t => t.ClrType.Name).ToArray();
            else
                args.Options = filtered.Select(t => t.ClrType.Name).ToArray();

            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled) return null;

            return filtered.First(t => t.ClrType.Name == args.Selection);
        }

        private void ImportTable(IEntityType entity, DataTable data)
        {
            // Anything this catches will definitely be a trait table, but it may miss
            // tables with small numbers of additional traits.
            // TODO: Find a better solution
            if(entity.ClrType.GetProperties().Count() < data.Columns.Count)
                AddTableWithTraitsToContext(entity, data);
            else
                AddTableToContext(entity, data);
            
            context.SaveChanges();
        }

        private void AddTableToContext(IEntityType entity, DataTable data)
        {
            var infos = data.Columns.Cast<DataColumn>()
                .Select(c => GetColumnInfo(entity, c))
                .Where(c => c != null)
                .ToList();

            foreach (DataRow r in data.Rows) AddDataRowToContext(r, entity.ClrType, infos);
        }

        private void AddTableWithTraitsToContext(IEntityType entity, DataTable data)
        {
            var columns = data.Columns.Cast<DataColumn>().Where(c => !c.ColumnName.Contains("Column"));

            // Find the relation between the entity type and the trait table
            var type = entity.ClrType;

            var relation = FindEntity(type.Name + "Trait").ClrType;
            var foreignInfo = relation.GetProperties().First(p => p.PropertyType == type);
            var traitInfo = relation.GetProperties().First(p => p.PropertyType == typeof(Trait));
            var valueInfo = relation.GetProperty("Value");

            foreach (DataRow r in data.Rows) 
                AddTraitDataRowToContext(r, columns, type, foreignInfo, traitInfo, valueInfo);
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
                    .Where(n => !n.Contains("Id"))
                    .ToList();

                col.ColumnName = GetColumnName(col, options.ToArray());
                
                if (col.ColumnName == null || col.ColumnName == "None") 
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

        private void AddDataRowToContext(DataRow row, Type type, IEnumerable<PropertyInfo> infos)
        {           
            IEntity entity = Activator.CreateInstance(type) as IEntity;

            foreach (var info in infos)
            {
                SetEntityValue(entity, row[info.Name], info);     
            }

            context.Add(entity);
        }

        private void AddTraitDataRowToContext(
            DataRow row, 
            IEnumerable<DataColumn> cols, 
            Type type, 
            PropertyInfo foreignInfo,
            PropertyInfo traitInfo,
            PropertyInfo valueInfo
        )
        {
            IEntity result = Activator.CreateInstance(type) as IEntity;
            context.Add(result);            

            foreach (DataColumn col in cols)
            {
                var value = row[col];

                // Check if the column is a property
                if (FindMatchingProperties(type, col) is PropertyInfo info)
                {
                    SetEntityValue(result, value, info);
                    continue;
                }

                // If the column is not a property, it must be a trait
                // Search the database for the corresponding trait
                Trait trait = context.Traits.FirstOrDefault(t => t.Name == col.ColumnName);

                // If it does not exist, add it
                if (trait == null)
                {
                    trait = new Trait()
                    {
                        Name = col.ColumnName,
                        Type = type.Name
                    };

                    context.Traits.Add(trait);                    
                }
                context.SaveChanges();

                var entity = Activator.CreateInstance(foreignInfo.DeclaringType) as IEntity;
                context.Add(entity);
                SetEntityValue(entity, result, foreignInfo);
                SetEntityValue(entity, trait, traitInfo);
                SetEntityValue(entity, value, valueInfo);
                context.SaveChanges();                
            }
        }

        private PropertyInfo FindMatchingProperties(Type type, DataColumn col)
        {
            var name = col.ColumnName;
            var props = type.GetProperties();

            var ps = props.Where(p => p.Name.Contains(name) || name.Contains(p.Name));

            if (ps.Count() <= 1) return ps.SingleOrDefault();

            var args = new ItemNotFoundArgs()
            {
                Name = name,
                Options = ps.Select(p => p.Name).ToArray()
            };
            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled || args.Selection == "None")
                return null;
            else
                return ps.First(p => p.Name == args.Selection);
        }

        private void SetEntityValue(IEntity entity, object value, PropertyInfo info)
        {
            // Entity will use default values for null or 0 entries
            if (value is DBNull) return;
            if (value is double d && d == 0) return;
            
            /* 
             * TODO: Fix this line.
             * 
             * This line is a temporary hack that only works because presently the
             * only context in which the value will also be an IEntity is in DataRowWithTraitsToEntity(),
             * where the value is also already a known property of the entity.
             * 
             * This may cause issues down the line if attempting to add an IEntity value that is not
             * already guaranteed to have a matching property on the entity.
            */
            if (value is IEntity) info.SetValue(entity, value);

            // Check if the property is an entity, if yes, search the context for the DbSet
            // which owns it, and search to see if there is a matching item in the table
            var type = info.PropertyType;
            
            // Handle strings
            if (type == typeof(string))
            {
                info.SetValue(entity, value);
            }
            // Handle entities
            else if (type.IsClass)
            {
                var ps = type.GetProperties();
                var query = context.Query(type);

                foreach (IEntity e in query)
                {
                    if (e.HasValue(value, ps))
                    {
                        info.SetValue(entity, e);
                        break;
                    }
                    else
                    {
                        // TODO: Implement handling for when the depedent entity does not exist
                    }
                }
            }
            // Handle nullable numerics
            else if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlying = Nullable.GetUnderlyingType(type);
                var v = Convert.ChangeType(value, underlying);
                info.SetValue(entity, v);
            }
            // Handle numerics
            else
            {
                var v = Convert.ChangeType(value, type);
                info.SetValue(entity, v);
            }
        }
    }
}
