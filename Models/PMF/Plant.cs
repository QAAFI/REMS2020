using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PMF
{
    public class Plant : Node
    {
        public string CropType { get; set; } = "";

        public bool IsEnding { get; set; } = false;

        public int DaysAfterEnding { get; set; } = 0;

        public string ResourceName { get; set; } = "";

        public Plant()
        {

        }
    }
}
