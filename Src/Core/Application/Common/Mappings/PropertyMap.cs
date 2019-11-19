using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Rems.Application.Common.Models;

namespace Rems.Application.Common.Mappings
{
    public class PropertyMap : IPropertyMap
    {
        public string Name { get; }

        private readonly DistinctDictionary<string, string> maps;

        public PropertyMap(string name)
        {
            Name = name;
        }

        public PropertyMap(object item)
        {
            Name = item.GetType().Name;

            maps = new DistinctDictionary<string, string>();

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
