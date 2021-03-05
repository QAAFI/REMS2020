using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;
using Rems.Domain.Interfaces;

namespace Rems.Application.Common.Extensions
{
    public static class RemsDbContextExtensions
    {
        public static void Close(this IRemsDbContext context)
        {
            /// TODO: Implement this
            //throw new NotImplementedException();
        }

        internal static bool NameMatches(this Trait trait, string name)
        {
            if (trait.Name == name)
                return true;

            if (trait.Name.ToLower() == name.ToLower())
            {
                trait.Name = name;
                return true;
            }

            return false;
        }

        internal static Trait GetTraitByName(this IRemsDbContext context, string name)
        {
            var trait = context.Traits.ToArray().FirstOrDefault(t => t.NameMatches(name));

            if (trait is null)
            {
                trait = new Trait() { Name = name };
                context.Add(trait);
            }

            context.SaveChanges();
            return trait;
        }

        internal static SoilLayer[] GetSoilLayers(this IRemsDbContext context, int experimentId)
        {
            var soil = context.Experiments.Find(experimentId).Field.Soil;

            var layers = context.SoilLayers
                .Where(s => s.SoilId == soil.SoilId)
                .OrderBy(l => l.FromDepth)
                .ToArray();

            return layers;
        }

        internal static double[] GetSoilLayerTraitData(this IRemsDbContext context, SoilLayer[] layers, string name)
        {
            var trait = context.GetTraitByName(name);

            var data = layers.Select(l => l.SoilLayerTraits.FirstOrDefault(t => t.TraitId == trait.TraitId))
                .Where(v => v != null);
            return data.Select(v => v.Value.GetValueOrDefault()).ToArray();
        }

        internal static Trait AddTrait(this IRemsDbContext context, string name, string type)
        {
            var unit = context.Units.FirstOrDefault(u => u.Name == "-");

            if (unit is null) unit = new Domain.Entities.Unit() { Name = "-" };

            var trait = new Trait()
            {
                Name = name,
                Type = type,
                Unit = unit
            };
            context.Add(trait);
            context.SaveChanges();
            return trait;
        }

        internal static Trait[] GetTraitsFromColumns(this IRemsDbContext context, DataTable table, int skip, string type)
        {
            Trait getTrait(DataColumn c)
            {
                var trait = context.Traits.FirstOrDefault(t => t.Name.ToLower() == c.ColumnName.ToLower());
                if (trait is null)
                    trait = context.AddTrait(c.ColumnName, type);

                return trait;
            }

            return table.Columns.Cast<DataColumn>()
                .Skip(skip)
                .Select(c => getTrait(c))
                .ToArray();
        }

        public static IEnumerable<PropertyInfo> GetEntityProperties(this IRemsDbContext context, Type type)
        {
            // All the primary and foreign keys for an entity
            var entity = context.Model.GetEntityTypes()
                .First(e => e.ClrType == type);

            var primaries = entity
                .FindPrimaryKey()
                .Properties
                .Select(p => p.PropertyInfo);

            var foreigns = entity
                .GetForeignKeys()
                .SelectMany(k => k.Properties.Select(p => p.PropertyInfo));

            // All the non-primary fields of an entity
            var props = type
                .GetProperties()
                .Where(p => !p.PropertyType.IsGenericType)
                .Where(p => p.PropertyType.IsValueType || p.PropertyType.IsInstanceOfType(""))
                .Except(primaries)
                .Except(foreigns);

            return props;
        }

        /// <summary>
        /// Search the context for the first entity of the given type which has a property that matches
        /// the given value
        /// </summary>
        /// <remarks>
        /// This is intended to be used to find related entities by name
        /// </remarks>
        public static IEntity FindMatchingEntity(this IRemsDbContext context, Type type, object value)
        {
            var set = context.GetType()
                .GetMethod("GetSet")
                .MakeGenericMethod(type)
                .Invoke(context, new object[0]) 
                as IEnumerable<IEntity>;

            var infos = type.GetProperties();
            var test = value.ToString().ToLower();

            return set.FirstOrDefault(e => infos.Any(i => i.GetValue(e)?.ToString().ToLower() == test));
        }

        public static IEntity CreateEntity(this IRemsDbContext context, Type type, object value)
        {
            IEntity entity = Activator.CreateInstance(type) as IEntity;
            var name = type.GetProperty("Name");

            if (name is null)
                type.GetProperty("SoilType")?.SetValue(entity, value);
            else
                name?.SetValue(entity, value);
            
            context.Attach(entity);
            context.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Add a measurement to the context, overwriting the value if a matching entry already exists
        /// </summary>
        public static void InsertData<T>(this IRemsDbContext context, Expression<Func<T, bool>> comparer, T data, double value) 
            where T : class, IEntity, IValue
        {
            var set = context.GetSet<T>();
            var found = set
                .Where(comparer)
                .SingleOrDefault();

            if (found != null)
                found.Value = value;
            else
                set.Attach(data);
        }
    }
}
