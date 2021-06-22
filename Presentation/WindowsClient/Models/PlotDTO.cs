using System;

namespace WindowsClient.Models
{
    public class PlotDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
