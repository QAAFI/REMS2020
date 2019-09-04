using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Core
{
    /// <summary>
    /// Generic container for models
    /// </summary>
    public class Folder : Node
    {
        public bool ShowPageOfGraphs { get; set; } = true;

        public Folder()
        { }
    }
}
