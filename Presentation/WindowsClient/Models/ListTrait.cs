using System;

namespace WindowsClient.Models
{
    public class ListTrait
    {
        /// <summary>
        /// Trait name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Trait description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Displays the trait with it's description
        /// </summary>
        /// <remarks>
        /// This is intended to aid display in a ListBox
        /// </remarks>
        public override string ToString() => Description is null ? Name : $"{Name} ({Description})";
    }
}
