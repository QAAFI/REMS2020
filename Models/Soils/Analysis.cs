using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Soils
{
    public class Analysis : Node
    {
        public List<double> Thickness { get; set; } = default;

        public List<string> Rocks { get; set; } = default;

        public string RocksMetadata { get; set; } = default;

        public List<string> Texture { get; set; } = default;

        public string TextureMetadata { get; set; } = default;

        public List<string> MunsellColour { get; set; } = default;

        public string MunsellColourMetadata { get; set; } = default;

        public List<double> EC { get; set; } = default;

        public string ECMetadata { get; set; } = default;

        public List<double> PH { get; set; } = default;

        public string PHMetadata { get; set; } = default;

        public List<double> CL { get; set; } = default;

        public string CLMetadata { get; set; } = default;

        public List<double> Boron { get; set; } = default;

        public string BoronMetadata { get; set; } = default;

        public List<double> CEC { get; set; } = default;

        public string CECMetadata { get; set; } = default;

        public List<double> Ca { get; set; } = default;

        public string CaMetadata { get; set; } = default;

        public List<double> Mg { get; set; } = default;

        public string MgMetadata { get; set; } = default;

        public List<double> Na { get; set; } = default;

        public string NaMetadata { get; set; } = default;

        public List<double> K { get; set; } = default;

        public string KMetadata { get; set; } = default;

        public List<double> ESP { get; set; } = default;

        public string ESPMetadata { get; set; } = default;

        public List<double> Mn { get; set; } = default;

        public string MnMetadata { get; set; } = default;

        public List<double> Al { get; set; } = default;

        public string AlMetadata { get; set; } = default;

        public List<double> ParticleSizeSand { get; set; } = default;

        public string ParticleSizeSandMetadata { get; set; } = default;

        public List<double> ParticleSizeSilt { get; set; } = default;

        public string ParticleSizeSiltMetadata { get; set; } = default;

        public List<double> ParticleSizeClay { get; set; } = default;

        public string ParticleSizeClayMetadata { get; set; } = default;

        public int PHUnits { get; set; } = default;

        public int BoronUnits { get; set; } = default;

        public Analysis()
        { }

    }
}
