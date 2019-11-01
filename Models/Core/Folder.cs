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
    public class Folder : ApsimNode
    {
        public bool ShowPageOfGraphs { get; set; } = true;

        public Folder() : base()
        { }

        public Folder(IEnumerable<ApsimNode> children) : base(children)
        { }
    }
}
