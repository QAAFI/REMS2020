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
        /// <summary>
        /// Tests if the trait name matches the given string
        /// </summary>
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

        /// <summary>
        /// Searches the context for a trait that matches the given name, creating one if none are found
        /// </summary>
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

        /// <summary>
        /// Gets all the soil layers from a given experiment
        /// </summary>
        internal static SoilLayer[] GetSoilLayers(this IRemsDbContext context, int experimentId)
        {
            var soil = context.Experiments.Find(experimentId).Field.Soil;

            var layers = context.SoilLayers
                .Where(s => s.SoilId == soil.SoilId)
                .OrderBy(l => l.FromDepth)
                .ToArray();

            return layers;
        }

        /// <summary>
        /// Gets the requested trait data for the given soil layers
        /// </summary>
        internal static double[] GetSoilLayerTraitData(this IRemsDbContext context, SoilLayer[] layers, string name)
        {
            var trait = context.GetTraitByName(name);

            var traits = layers.Select(l => l.SoilLayerTraits.FirstOrDefault(t => t.TraitId == trait.TraitId))
                .Where(t => t != null)
                .Select(v => v.Value.GetValueOrDefault());

            var data = context.SoilLayerDatas.Where(d => d.TraitId == trait.TraitId).ToArray();

            var datas = layers.Select(l => data.Where(d => d.DepthFrom * 10 == l.FromDepth)
                    .OrderBy(d => d.Date)
                    .FirstOrDefault()?.Value ?? 0
            );

            var values = traits ?? datas;

            return values.Any() ? values.ToArray() : new double[] { };
        }

        /// <summary>
        /// Adds a trait to the database
        /// </summary>
        /// <param name="name">Trait name</param>
        /// <param name="type">Trait type</param>
        /// <returns></returns>
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

        /// <summary>
        /// Attempts to find a database trait which matches the columns in a data table
        /// </summary>
        /// <param name="context">The database context to search</param>
        /// <param name="table">The table containing the columns</param>
        /// <param name="skip">The number of initial columns to skip</param>
        /// <param name="type">The trait type</param>
        /// <returns></returns>
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

        /// <summary>
        /// Find the properties on an entity that contain data
        /// </summary>
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

        /// <summary>
        /// Create an entity of the given type, and attach it to the database after attempting to assign 
        /// one of its properties the given value
        /// </summary>
        /// <param name="context">The context to attach the entity to</param>
        /// <param name="type">The CLR type of the <see cref="IEntity"/></param>
        /// <param name="value">The value to attempt to assign</param>
        /// <returns></returns>
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
