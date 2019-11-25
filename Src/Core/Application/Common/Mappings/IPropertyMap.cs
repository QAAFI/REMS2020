using System;
using System.Collections.Generic;
using System.Text;

using Rems.Application.Common.Models;

namespace Rems.Application.Common.Mappings
{
    /// <summary>
    /// Contains a collection of parameters that may be mapped to some other value
    /// </summary>
    public interface IPropertyMap : IEquatable<IPropertyMap>, IEnumerable<KeyValuePair<string, string>>
    {
        /// <summary>
        /// The name of the mapping
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Test if a property is mapped to a value
        /// </summary>
        bool IsMapped(string property);

        /// <summary>
        /// Test if there is a property mapped to a value 
        /// </summary>
        bool HasMapping(string value);

        /// <summary>
        /// Find the property which is mapped to the value
        /// </summary>
        string MappedFrom(string value);

        /// <summary>
        /// Add a new property to the mapping
        /// </summary>
        void AddMapping(string property);

        /// <summary>
        /// Add a new property with a value to the mapping
        /// </summary>
        void AddMapping(string property, string value);

        /// <summary>
        /// Remove a property from the mapping
        /// </summary>
        bool RemoveMapping(string property);
        
        /// <summary>
        /// Get or Set the value the property is mapped to
        /// </summary>
        string this[string property] { get; set; }

    }
}
