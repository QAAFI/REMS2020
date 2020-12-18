using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Controls;

namespace WindowsClient.Models
{
    public class Session
    {
        public string DB { get; set; }

        [JsonIgnore]
        public TabPage Experiments { get; } = new TabPage("Experiment details");

        [JsonIgnore]
        public ExperimentDetailer Detailer { get; } = new ExperimentDetailer();

        public Session()
        {
            Detailer.Dock = DockStyle.Fill;
            Experiments.Controls.Add(Detailer);
        }

        public override string ToString() => Path.GetFileName(DB);
    }
}
