using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Rems.Application.Entities.Commands
{
    public class BulkInsertCommandHandler : IRequestHandler<BulkInsertCommand, bool>
    {
        private readonly IRemsDbContext _context;

        private CancellationToken _token;

        public BulkInsertCommandHandler(IRemsDbContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(BulkInsertCommand request, CancellationToken token)
        {
            _token = token;

            // TODO: This handler has grown larger than originally planned, structurally
            // it might be better to try reorganise some of the methods into other classes
            return Task.Run(() =>
            {
                foreach (DataTable table in request.Data.Tables)
                {
                    if (table.Rows.Count == 0) continue;
                    ImportTable(table);
                }

                return true;
            });
        }

        /// <summary>
        /// Adds the given data table to the context
        /// </summary>
        private void ImportTable(DataTable table)
        {
            // These IF statements are awful code, but my hand has been forced since the data is
            // coming from poorly designed excel templates. I decided devising a better solution 
            // is currently not worth the effort.

            if (table.TableName == "Notes") return;
            if (table.TableName == "Design") { ImportDesignTable(table); return; }

            // TODO: Refactor these three functions into one ImportDataTable function
            if (table.TableName == "PlotData") { ImportPlotDataTable(table); return; }
            if (table.TableName == "MetData") { ImportMetDataTable(table); return; }
            if (table.TableName == "SoilLayerData") { ImportSoilLayerDataTable(table); return; }

            var entity = FindEntity(table.TableName);
            if (entity == null) return;

            // Remove any duplicate rows from the table
            var rows = table.DistinctRows().Select(r => r.ItemArray).ToArray();
            table.Rows.Clear();
            foreach (var row in rows) table.Rows.Add(row);

            // TODO: Find a better catch for treatment tables
            // Sheet names are not a good way of identifying anything.
            if (table.TableName == "Irrigation" || table.TableName == "Fertilization")
                AddTableWithTreatmentsToContext(entity, table);
            
            // TODO: Find a better catch for trait tables
            // This only filters true negatives, not false positives or false negatives
            else if (entity.ClrType.GetProperties().Count() < table.Columns.Count)
                AddTableWithTraitsToContext(entity, table);
            else
                AddTableToContext(entity, table);

            _context.SaveChangesAsync(_token);
        }

        /// <summary>
        /// Searches the context for an entity type by name
        /// </summary>
        private IEntityType FindEntity(string name)
        {
            var types = _context.Model.GetEntityTypes();
            
            // Names might not match exactly - look for contains, not equality
            var filtered = types.Where(e => name.Contains(e.ClrType.Name));

            // Assume that if only a single result is returned, that is the matching entity
            if (filtered.Count() == 1) return filtered.Single();

            var args = new ItemNotFoundArgs() { Name = name };

            // If there is no match, get a list of the possible types
            if (filtered.Count() == 0)
                args.Options = types.Select(t => t.ClrType.Name).ToArray();
            // If there is an exact name match, use that (TODO: This test should probably be run first)
            else if (filtered.FirstOrDefault(t => t.ClrType.Name == name) is IEntityType e)
                return e;
            // If there are multiple options left, get a list of those options
            else
                args.Options = filtered.Select(t => t.ClrType.Name).ToArray();

            // Ask the user to pick an entity type from the list of options
            EventManager.InvokeItemNotFound(null, args);

            if (args.Cancelled || args.Selection == "None") return null;

            return types.First(t => t.ClrType.Name == args.Selection);
        }

        /// <summary>
        /// Import a table, assuming its schema matches the known design table layout
        /// </summary>
        /// <param name="table"></param>
        private void ImportDesignTable(DataTable table)
        {
            // This method is hardcoded because I decided it wasn't worth the time
            // to develop a stable general solution for a single badly designed excel table.
            // Apologies to whoever has to fix it when it eventually breaks.

            var eGroup = table.Rows.Cast<DataRow>()
                .GroupBy(row => ConvertDBValue<int>(row[0])); // Group the experiment rows together

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

                    _context.Add(treatment);
                    _context.SaveChanges();

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

            _context.AddRange(plots.ToArray());
            _context.SaveChangesAsync(_token);
        }

        /// <summary>
        /// Add a row to the context, assuming it is a row belonging to the known 'Design' table
        /// </summary>
        private void AddDesignRowToContext(DataRow row, int treatmentId)
        {
            var columns = row.Table.Columns;

            // Assume that every column after the 4th represents a factor, and 
            // the value in that column is a level belonging to said factor
            for (int i = 4; i < row.Table.Columns.Count; i++)
            {
                if (row[i] is DBNull) continue;

                var level = _context.Levels.FirstOrDefault(l => l.Name == row[i].ToString());

                // If we can't find the level in the context, add it
                if (level == null)
                {
                    var factor = _context.Factors.FirstOrDefault(f => f.Name == columns[i].ColumnName);

                    // If we can't find the factor in the context, add it
                    if (factor == null)
                    {
                        factor = new Factor()
                        {
                            Name = columns[i].ColumnName
                        };
                        _context.Add(factor);
                        _context.SaveChangesAsync(_token);
                    }

                    level = new Level()
                    {
                        Name = row[i].ToString(),
                        FactorId = factor.FactorId
                    };
                    _context.Add(level);
                    _context.SaveChangesAsync(_token);
                }

                // Add the design to the context
                var design = new Design()
                {
                    TreatmentId = treatmentId,
                    LevelId = level.LevelId
                };
                _context.Add(design);
                _context.SaveChangesAsync(_token);
            }
        }

        /// <summary>
        /// Import a table to the context, assuming its schema matches the known PlotData layout
        /// </summary>
        private void ImportPlotDataTable(DataTable table)
        {
            // Assume that every column after the 4th is a trait column
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(4)
                .Select(c => GetTrait(c.ColumnName, "PlotData"))
                .ToArray();

            foreach (DataRow row in table.Rows)
            {
                // Assume that the first column is the experiment ID
                var id = ConvertDBValue<int>(row[0]);

                //  Assume the second column is the plot column
                var col = ConvertDBValue<int>(row[1]);

                var plot = _context.Plots.FirstOrDefault(p => p.Treatment.ExperimentId == id && p.Column == col);

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
                    _context.Add(data);
                }                
            }
            _context.SaveChangesAsync(_token);
        }

        /// <summary>
        /// Import a table to the context, assuming its schema matches the known MetData layout
        /// </summary>
        private void ImportMetDataTable(DataTable table)
        {
            // Assume every column after the 2nd is a trait column
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(2)
                .Select(c => GetTrait(c.ColumnName, "MetData"))
                .ToArray();

            foreach (DataRow row in table.Rows)
            {
                // Look for the station which sourced the data, create one if it isn't found
                var station = _context.MetStations.FirstOrDefault(m => m.Name == row[0].ToString());
                if (station is null)
                {
                    station = new MetStation() { Name = row[0].ToString() };
                    _context.Add(station);
                    _context.SaveChangesAsync(_token);
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
                    _context.Add(data);
                }
            }
            _context.SaveChangesAsync(_token);
        }

        /// <summary>
        /// Add a table to the context, assuming that its schema matches the known soil layer data layout
        /// </summary>
        /// <param name="table"></param>
        private void ImportSoilLayerDataTable(DataTable table)
        {
            // Assume that every column after the 5th is a trait column
            var traits = table.Columns.Cast<DataColumn>()
                .Skip(5)
                .Select(c => GetTrait(c.ColumnName, "MetData"))
                .ToArray();

            foreach (DataRow row in table.Rows)
            {
                var id = ConvertDBValue<int>(row[0]);
                var col = ConvertDBValue<int>(row[1]);

                var plot = _context.Plots.FirstOrDefault(p => p.Treatment.ExperimentId == id && p.Column == col);

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
                    _context.Add(data);
                }
            }
            _context.SaveChangesAsync(_token);
        }

        /// <summary>
        /// Add a generic table to the context
        /// </summary>
        /// <remarks>
        /// This assumes that the table is 1:1 with a table in the schema. There are tables that this does
        /// not hold true for, which must be imported using a different method.
        /// </remarks>
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
                    var treatments = _context.Treatments.Where(t => t.ExperimentId == id);

                    foreach(var treatment in treatments)
                        AddTreatmentDataRowToContext(row, entity.ClrType, infos, treatment.TreatmentId);
                }
                else
                {
                    var treatment = _context.Treatments.FirstOrDefault(t => t.ExperimentId == id && t.Name == name);

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

            _context.Add(entity);
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
            _context.Add(result);            

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
                _context.Add(entity);
                SetEntityValue(entity, result, foreignInfo);
                SetEntityValue(entity, trait, traitInfo);
                SetEntityValue(entity, value, valueInfo);
                _context.SaveChangesAsync(_token);                
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
            Trait trait = _context.Traits.FirstOrDefault(t => t.Name == name);

            // If it does not exist, add it
            if (trait == null)
            {
                var unit = _context.Units.FirstOrDefault(u => u.Name == "-");

                if (unit is null)
                {
                    unit = new Domain.Entities.Unit() { Name = "-" };
                    _context.Add(unit);
                    _context.SaveChanges();
                }

                trait = new Trait()
                {
                    Name = name,
                    Type = type,
                    UnitId = unit.UnitId
                };

                _context.Traits.Add(trait);
            }
            _context.SaveChangesAsync(_token);

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

            _context.Add(entity);
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
                var query = _context.Query(type);

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

                _context.Add(item);
                _context.SaveChangesAsync(_token);

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
