using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Analysis : ApsimNode
    {
        public List<double> Thickness { get; set; } = new List<double>();

        public List<string> Rocks { get; set; } = new List<string>();

        public string RocksMetadata { get; set; } = default;

        public List<string> Texture { get; set; } = new List<string>();

        public string TextureMetadata { get; set; } = default;

        public List<string> MunsellColour { get; set; } = new List<string>();

        public string MunsellColourMetadata { get; set; } = default;

        public List<double> EC { get; set; } = new List<double>();

        public string ECMetadata { get; set; } = default;

        public List<double> PH { get; set; } = new List<double>();

        public string PHMetadata { get; set; } = default;

        public List<double> CL { get; set; } = new List<double>();

        public string CLMetadata { get; set; } = default;

        public List<double> Boron { get; set; } = new List<double>();

        public string BoronMetadata { get; set; } = default;

        public List<double> CEC { get; set; } = new List<double>();

        public string CECMetadata { get; set; } = default;

        public List<double> Ca { get; set; } = new List<double>();

        public string CaMetadata { get; set; } = default;

        public List<double> Mg { get; set; } = new List<double>();

        public string MgMetadata { get; set; } = default;

        public List<double> Na { get; set; } = new List<double>();

        public string NaMetadata { get; set; } = default;

        public List<double> K { get; set; } = new List<double>();

        public string KMetadata { get; set; } = default;

        public List<double> ESP { get; set; } = new List<double>();

        public string ESPMetadata { get; set; } = default;

        public List<double> Mn { get; set; } = new List<double>();

        public string MnMetadata { get; set; } = default;

        public List<double> Al { get; set; } = new List<double>();

        public string AlMetadata { get; set; } = default;

        public List<double> ParticleSizeSand { get; set; } = new List<double>();

        public string ParticleSizeSandMetadata { get; set; } = default;

        public List<double> ParticleSizeSilt { get; set; } = new List<double>();

        public string ParticleSizeSiltMetadata { get; set; } = default;

        public List<double> ParticleSizeClay { get; set; } = new List<double>();

        public string ParticleSizeClayMetadata { get; set; } = default;

        public int PHUnits { get; set; } = default;

        public int BoronUnits { get; set; } = default;

        public Analysis()
        { }

    }
}
