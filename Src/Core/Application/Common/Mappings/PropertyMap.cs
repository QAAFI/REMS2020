using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using Rems.Application.Common.Models;

namespace Rems.Application.Common.Mappings
{
    [JsonObject]
    public class PropertyMap : IPropertyMap
    {
        [JsonProperty]
        public string Name { get; private set; }        
        
        [JsonProperty]
        private readonly DistinctDictionary<string, string> maps = new DistinctDictionary<string, string>();

        [JsonConstructor]
        private PropertyMap()
        {

        }

        public PropertyMap(string name)
        {
            Name = name;
        }

        public PropertyMap(object item)
        {
            Name = item.GetType().Name;

            foreach (var property in item.GetType().GetProperties())
            {
                maps.Add(property.Name, property.Name);
            }
        }

        public bool Equals(IPropertyMap other)
        {
            if (other.Name == Name)
                return true;
            else
                return false;
        }

        public bool IsMapped(string property)
        {
            if (maps.ContainsKey(property))
                return true;
            else
                return false;
        }

        public bool HasMapping(string value)
        {
            if (maps.ContainsValue(value))
                return true;
            else
                return false;
        }

        public string MappedFrom(string value)
        {
            return maps.Single(kvp => kvp.Value == value).Key;
        }

        public bool RemoveMapping(string property)
        {
            return maps.Remove(property);
        }

        public void AddMapping(string property)
        {
            maps.Add(property, property);
        }

        public void AddMapping(string property, string value)
        {
            maps.Add(property, value);
        }

        public string this[string property]
        {
            get
            {
                return maps[property];
            }
            set
            {
                maps[property] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var kvp in maps) yield return kvp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}
