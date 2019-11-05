using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsClient
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
                if (ContainsKey(key))
                {
                    if (ContainsValue(value)) 
                        throw new Exception($"Value already exists: {value}");
                    else
                    {
                        base[key] = value;
                    }                    
                }
                else
                    Add(key, value);
            }
        }
    }
}
