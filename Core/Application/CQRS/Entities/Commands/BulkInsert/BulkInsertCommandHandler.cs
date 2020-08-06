using MediatR;
using Microsoft.EntityFrameworkCore;
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
                if (table.Rows.Count == 0) continue;
                ImportTable(table);
            }
            return true;
        }

        private void ImportTable(DataTable table)
        {
            // These IF statements are awful code, but my hand has been forced since the data is
            // coming from poorly designed excel templates. I decided devising a better solution 
            // is currently not worth the effort.

            if (table.TableName == "Notes") return;
            if (table.TableName == "Design") { ImportDesignTable(table); return; }

            // TODO: Refactor these three functions into one ImportDataTable
            if (table.TableName == "PlotData") { ImportPlotDataTable(table); return; }
            if (table.TableName == "MetData") { ImportMetDataTable(table); return; }
            if (table.TableName == "SoilLayerData") { ImportSoilLayerDataTable(table); return; }

            var entity = FindEntity(table.TableName);
            if (entity == null) return;

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

            var eGroup = table.Rows.Cast<DataRow>().GroupBy(row => ConvertDBValue<int>(row[0])); // Group the experiment rows together

            var plots = new List<Plot>();

            foreach (var e in eGroup)
            {
                var tGroup = e.GroupBy(row => row[1].ToString()); // Sub-group the experiment rows by treatment

                foreach (var t in tGroup)
                {
                    var treatment = new Treatment()
                    {
                        ExperimentId = e.Key,
                        Name = t.Key
                    };

                    context.Add(treatment);
                    context.SaveChanges();

                    AddDesignRowToContext(t.First(), treatment.TreatmentId);

                    foreach (var row in t)
                    {
                        var p = new Plot()
                        {
                            TreatmentId = treatment.TreatmentId,
                            Repetition = ConvertDBValue<int>(row[2]),
                            Column = ConvertDBValue<int>(row[3])
                        };
                        plots.Add(p);
                    }
                }
            }

            context.AddRange(plots);
            context.SaveChanges();

            //foreach (DataRow row in table.Rows)
            //{
            //    var treatment = new Treatment()
            //    {
            //        ExperimentId = ConvertDBValue<int>(row[0]),
            //        Name = row[1].ToString()
            //    };
            //    context.Add(treatment);
            //    context.SaveChanges();

            //    var plot = new Plot()
            //    {
            //        TreatmentId = treatment.TreatmentId,
            //        Repetition = ConvertDBValue<int>(row[2]),
            //        Column = ConvertDBValue<int>(row[3])
            //    };
            //    context.Add(plot);
            //    context.SaveChanges();

            //    AddDesignRowToContext(row, treatment.TreatmentId);
            //}
        }

        private void AddDesignRowToContext(DataRow row, int treatmentId)
        {
            var columns = row.Table.Columns;

            for (int i = 4; i < 8; i++)
            {
                if (row[i] is DBNull) continue;

                var level = context.Levels.FirstOrDefault(l => l.Name == row[i].ToString());
                if (level == null)
                {
                    var factor = context.Factors.FirstOrDefault(f => f.Name == columns[i].ColumnName);

                    if (factor == null)
                    {
                        factor = new Factor()
                        {
                            Name = columns[i].ColumnName
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
                    TreatmentId = treatmentId,
                    LevelId = level.LevelId
                };
                context.Add(design);
                context.SaveChanges();
            }
        }

        private void ImportPlotDataTable(DataTable table)
        {
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(4)
                .Select(c => GetTrait(c.ColumnName, "PlotData"))
                .ToArray();

            context.SaveChanges();

            foreach (DataRow row in table.Rows)
            {
                // Assume that the first column is the experiment ID and the second column is the plot column

                var id = ConvertDBValue<int>(row[0]);
                var col = ConvertDBValue<int>(row[1]);

                var plot = context.Plots.FirstOrDefault(p => p.Treatment.ExperimentId == id && p.Column == col);

                // TODO: This is a lazy approach that simply skips bad data, try to find a better solution
                if (plot == null) continue;

                for (int i = 4; i < table.Columns.Count; i++)
                {
                    if (row.ItemArray[i] is DBNull) continue;

                    var data = new PlotData()
                    {
                        PlotId = plot.PlotId,
                        TraitId = traits[i - 4].TraitId,
                        Date = ConvertDBValue<DateTime>(row[2]),
                        Sample = row[3].ToString(),
                        Value = ConvertDBValue<double>(row[i]),
                        UnitId = traits[i - 4].UnitId
                    };
                    context.Add(data);
                }                
            }
            context.SaveChanges();
        }

        private void ImportMetDataTable(DataTable table)
        {
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(2)
                .Select(c => GetTrait(c.ColumnName, "MetData"))
                .ToArray();

            foreach (DataRow row in table.Rows)
            {
                var station = context.MetStations.FirstOrDefault(m => m.Name == row[0].ToString());

                if (station is null)
                {
                    station = new MetStation() { Name = row[0].ToString() };
                    context.Add(station);
                    context.SaveChanges();
                }

                for (int i = 2; i < table.Columns.Count; i++)
                {
                    if (row.ItemArray[i] is DBNull) continue;
                                        
                    var data = new MetData()
                    {
                        MetStationId = station.MetStationId,
                        TraitId = traits[i - 2].TraitId,
                        Date = ConvertDBValue<DateTime>(row[1]),
                        Value = ConvertDBValue<double>(row[i])
                    };
                    context.Add(data);
                }
            }
            context.SaveChanges();
        }

        private void ImportSoilLayerDataTable(DataTable table)
        {
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(5)
                .Select(c => GetTrait(c.ColumnName, "MetData"))
                .ToArray();

            foreach (DataRow row in table.Rows)
            {
                var id = ConvertDBValue<int>(row[0]);
                var col = ConvertDBValue<int>(row[1]);

                var plot = context.Plots.FirstOrDefault(p => p.Treatment.ExperimentId == id && p.Column == col);

                // TODO: This is a lazy approach that simply skips bad data, try to find a better solution
                if (plot == null) continue;

                for (int i = 5; i < table.Columns.Count; i++)
                {
                    if (row.ItemArray[i] is DBNull) continue;

                    var data = new SoilLayerData()
                    {
                        PlotId = plot.PlotId,
                        TraitId = traits[i - 5].TraitId,
                        Date = ConvertDBValue<DateTime>(row[2]),
                        DepthFrom = ConvertDBValue<int>(row[3]),
                        DepthTo = ConvertDBValue<int>(row[4]),
                        Value = ConvertDBValue<double>(row[i])
                    };
                    context.Add(data);
                }
            }
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

                var id = ConvertDBValue<int>(row[0]);
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
                Trait trait = GetTrait(col.ColumnName, type.Name);

                var entity = Activator.CreateInstance(foreignInfo.DeclaringType) as IEntity;
                context.Add(entity);
                SetEntityValue(entity, result, foreignInfo);
                SetEntityValue(entity, trait, traitInfo);
                SetEntityValue(entity, value, valueInfo);
                context.SaveChanges();                
            }
        }

        /// <summary>
        /// Looks for a trait in the database, and creates one if it cannot be found
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Trait GetTrait(string name, string type = "")
        {
            Trait trait = context.Traits.FirstOrDefault(t => t.Name == name);

            // If it does not exist, add it
            if (trait == null)
            {
                var unit = context.Units.FirstOrDefault(u => u.Name == "-");

                if (unit is null)
                {
                    unit = new Domain.Entities.Unit() { Name = "-" };
                    context.Add(unit);
                    context.SaveChanges();
                }

                trait = new Trait()
                {
                    Name = name,
                    Type = type,
                    UnitId = unit.UnitId
                };

                context.Traits.Add(trait);
            }
            context.SaveChanges();

            return trait;
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

        private T ConvertDBValue<T>(object value)
        {
            Type type = typeof(T);

            // Convert nullable numerics
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                var underlying = Nullable.GetUnderlyingType(type);
                return (T)Convert.ChangeType(value, underlying);
            }

            // Convert normal numerics
            return (T)Convert.ChangeType(value, type);
        }
    }
}
