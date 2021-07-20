using System;

namespace WindowsClient.Models
{
    public class ListPair
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{Name} ({Description})";
    }
}
