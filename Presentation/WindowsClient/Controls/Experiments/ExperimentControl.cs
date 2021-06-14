using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Controls
{
    public interface IExperimentControl
    {
        /// <summary>
        /// The current experiment ID
        /// </summary>
        int Experiment { get; set; }

        /// <summary>
        /// The current treatment ID
        /// </summary>
        int Treatment { get; set; }
    }

    public class ControlNode<TControl> : TreeNode where TControl : UserControl, IExperimentControl, new()
    {
        public ControlNode(string text) : base(text)
        { }

        public TControl Create() => new();
    }
}
