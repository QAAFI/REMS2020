using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Models
{
    public class Operations : ApsimNode
    {       
        public List<Operation> Operation { get; } = new List<Operation>();

        public Operations()
        {
            Name = "Operations";
        }
    }
}
