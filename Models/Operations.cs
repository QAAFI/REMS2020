using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Operations : Node
    {
        public List<Operation> Operation { get; } = new List<Operation>();

        public Operations()
        {
            Name = "Operations";
        }
    }
}
