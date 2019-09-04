using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Operation : Node
    {
        public DateTime Date { get; set; } = default;

        public string Action { get; set; } = default;
        
        public Operation()
        { }
    }
}
