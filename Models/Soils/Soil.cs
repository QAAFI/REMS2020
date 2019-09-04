using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Soil : Node
    {
        public int RecordNumber { get; set; } = default;

        public string ASCOrder { get; set; } = default;

        public string ASCSubOrder { get; set; } = default;

        public string SoilType { get; set; } = default;

        public string LocalName { get; set; } = default;

        public string Site { get; set; } = default;

        public string NearestTown { get; set; } = default;

        public string Region { get; set; } = default;

        public string State { get; set; } = default;

        public string Country { get; set; } = default;

        public string NaturalVegetation { get; set; } = default;

        public string ApsoilNumber { get; set; } = default;

        public double Latitude { get; set; } = default;

        public double Longitude { get; set; } = default;

        public string LocationAccuracy { get; set; } = default;

        public string DataSource { get; set; } = default;

        public string Comments { get; set; } = default;

        public Soil()
        { }
    }
}
