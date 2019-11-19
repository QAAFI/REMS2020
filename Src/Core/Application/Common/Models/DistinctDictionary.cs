using System;
using System.Collections.Generic;

namespace Rems.Application.Common.Models
{
    /// <summary>
    /// A dictionary where both the values and keys must be distinct
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DistinctDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new Exception($"Key already exists: {key}");
            }

            if (ContainsValue(value))
            {
                throw new Exception($"Value already exists: {value}");
            }

            base.Add(key, value);
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (ContainsValue(value)) throw new Exception($"Value already exists in DistinctDictionary: {value}");

                base[key] = value;
            }
        }
    }
}
