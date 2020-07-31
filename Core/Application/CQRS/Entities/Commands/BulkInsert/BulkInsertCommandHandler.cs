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
            // TODO: This handler has grown larger than originally planned, structurally
            // it might be better to try reorganise some of the methods into other classes

            foreach (DataTable table in request.Data.Tables)
            {               
                // These IF statements are awful code, but my hand has been forced since the data is
                // coming from poorly designed excel templates. Devising a better solution is currently
                // not worth the effort.

                if (table.TableName == "Notes") continue;                
                if (table.TableName == "Design") { ImportDesignTable(table); continue; }

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
            else if (filtered.FirstOrDefault(t => t.ClrType.Name == name) is IEntityType e)
                return e;
            else
                args.Options = filtered.Select(t => t.ClrType.Name).ToArray();

            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled || args.Selection == "None") return null;

            return filtered.First(t => t.ClrType.Name == args.Selection);
        }

        private void ImportDesignTable(DataTable table)
        {
            // This method is hardcoded because I decided it wasn't worth the time
            // to develop a stable general solution for a single badly designed excel table.
            // Apologies to whoever has to fix it when it eventually breaks.
            foreach (DataRow row in table.Rows)
            {
                var treatment = new Treatment()
                {
                    ExperimentId = (int)ConvertDBValue(row[0], typeof(int)),
                    Name = row[1].ToString()
                };
                context.Add(treatment);
                context.SaveChanges();

                var plot = new Plot()
                {
                    TreatmentId = treatment.TreatmentId,
                    Repetition = (int)ConvertDBValue(row[2], typeof(int)),
                    Column = (int)ConvertDBValue(row[3], typeof(int))
                };
                context.Add(plot);
                context.SaveChanges();

                for (int i = 4; i < 8; i++)
                {
                    if (row[i] is DBNull) continue;

                    var level = context.Levels.FirstOrDefault(l => l.Name == row[i].ToString());
                    if (level == null)
                    {
                        var factor = context.Factors.FirstOrDefault(f => f.Name == table.Columns[i].ColumnName);

                        if (factor == null)
                        {
                            factor = new Factor()
                            {
                                Name = table.Columns[i].ColumnName
                            };
                            context.Add(factor);
                            context.SaveChanges();
                        }

                        level = new Level() 
                        { 
                            Name = row[i].ToString(),
                            FactorId = factor.FactorId
                        };
                        context.Add(level);
                        context.SaveChanges();
                    }

                    var design = new Design()
                    {
                        TreatmentId = treatment.TreatmentId,
                        LevelId = level.LevelId
                    };
                    context.Add(design);
                    context.SaveChanges();
                }
            }
            
        }

        private void ImportTable(IEntityType entity, DataTable table)
        {
            var rows = table.DistinctRows().Select(r => r.ItemArray).ToArray();
            table.Rows.Clear();
            foreach (var row in rows) table.Rows.Add(row);

            // TODO: Find a better catch for treatment tables
            // Sheet names are not a good way of identifying anything.
            if (table.TableName == "Irrigation" || table.TableName == "Fertilization")
                AddTableWithTreatmentsToContext(entity, table);
            // TODO: Find a better catch for trait tables
            // This only prevents true negatives, not false positives or false negatives
            else if (entity.ClrType.GetProperties().Count() < table.Columns.Count)
                AddTableWithTraitsToContext(entity, table);
            else
                AddTableToContext(entity, table);
            
            context.SaveChanges();
        }

        private void AddTableToContext(IEntityType entity, DataTable data)
        {
            var infos = data.Columns.Cast<DataColumn>()
                .Select(c => GetColumnInfo(entity, c))
                .Where(c => c != null)
                .ToList();

            foreach (DataRow row in data.Rows)
                AddDataRowToContext(row, entity.ClrType, infos);
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

        private void AddTableWithTreatmentsToContext(IEntityType entity, DataTable data)
        {
            var infos = data.Columns.Cast<DataColumn>()
                .Skip(2)
                .Select(c => GetColumnInfo(entity, c))
                .Where(c => c != null)
                .ToList();

            foreach (DataRow row in data.Rows)
            {
                // Assume that in a 'treatment' row, the first column is the experiment ID
                // and the second column is the treatment name

                var id = (int)ConvertDBValue(row[0], typeof(int));
                var name = row[1].ToString();

                if (name == "ALL" || name == "All" || name == "all") // Blame the spreadsheet, I don't like it either
                {
                    var treatments = context.Treatments.Where(t => t.ExperimentId == id);

                    foreach(var treatment in treatments)
                        AddTreatmentDataRowToContext(row, entity.ClrType, infos, treatment.TreatmentId);
                }
                else
                {
                    var treatment = context.Treatments.FirstOrDefault(t => t.ExperimentId == id && t.Name == name);

                    // TODO: This is a lazy approach that simply skips bad data, try to find a better solution
                    if (treatment == null) continue;

                    AddTreatmentDataRowToContext(row, entity.ClrType, infos, treatment.TreatmentId);
                }
                
            }             
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

                var name = GetColumnName(col, options.ToArray());                
                if (name == null || name == "None") return null;

                col.ColumnName = name;

                // TODO: Rather than use recursion, this method can probably be separated into smaller methods
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

            if (args.Cancelled) 
                return null;
            else 
                return args.Selection;
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

        private void AddTreatmentDataRowToContext(DataRow row, Type type, IEnumerable<PropertyInfo> infos, int treatmentId)
        {
            ITreatment entity = Activator.CreateInstance(type) as ITreatment;

            entity.TreatmentId = treatmentId;

            foreach (var info in infos)
            {
                SetEntityValue(entity, row[info.Name], info);
            }

            context.Add(entity);
        }

        private PropertyInfo FindMatchingProperties(Type type, DataColumn col)
        {
            var name = col.ColumnName;
            var props = type.GetProperties();

            if (props.SingleOrDefault(p => p.Name == name) is PropertyInfo i && i != null) return i;

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
            if (value is IEntity)
            {
                info.SetValue(entity, value);
                return;
            }

            // Check if the property is an entity, if yes, search the context for the DbSet
            // which owns it, and search to see if there is a matching item in the table
            var type = info.PropertyType;
            
            // Handle strings
            if (type == typeof(string))
            {
                info.SetValue(entity, value);
                return;
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
                        return;
                    }
                }

                // If the entity was not found in the query

                // Assume that the entity was referred to by name, and create
                // a new entity using that value
                INamed item = Activator.CreateInstance(type) as INamed;
                item.Name = value.ToString();

                context.Add(item);
                context.SaveChanges();

                info.SetValue(entity, item);
            }
            else
            {
                var v = ConvertDBValue(value, type);
                info.SetValue(entity, v);
            }
        }

        private object ConvertDBValue(object value, Type type)
        {
            // Convert nullable numerics
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlying = Nullable.GetUnderlyingType(type);
                return Convert.ChangeType(value, underlying);
            }
            
            // Convert normal numerics
            return Convert.ChangeType(value, type);
        }
    }
}
