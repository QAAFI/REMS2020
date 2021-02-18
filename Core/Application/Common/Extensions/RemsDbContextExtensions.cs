using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
                var trait = context.Traits.FirstOrDefault(t => t.Name == c.ColumnName);
                if (trait is null)
                    trait = context.AddTrait(c.ColumnName, type);

                return trait;
            }

            return table.Columns.Cast<DataColumn>()
                .Skip(skip)
                .Select(c => getTrait(c))
                .ToArray();
        }

        internal static IEntity FindMatchingEntity(this IRemsDbContext context, Type type, object value)
        {
            var set = context.GetType()
                .GetMethod("GetSet")
                .MakeGenericMethod(type)
                .Invoke(context, new object[0]) 
                as IEnumerable<IEntity>;

            var props = type.GetProperties();

            foreach (var entity in set)            
                if (entity.HasValue(value, props)) return entity;            

            return null;
        }

        /// <summary>
        /// Add a measurement to the context, overwriting the value if a matching entry already exists
        /// </summary>
        internal static void InsertData<T>(this IRemsDbContext context, Expression<Func<T, bool>> comparer, T data, double value) 
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
