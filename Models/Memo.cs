using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// A memorandum for the user
    /// </summary>
    public class Memo : ApsimNode
    {
        public string Text { get; set; }

        public Memo()
        { }
    }
}
