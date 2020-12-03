using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClient.Controls;

namespace WindowsClient.Models
{
    public class Session
    {
        public string DB { get; set; }

        public Stage Info { get; set; } = Stage.Missing;

        public Stage Exps { get; set; } = Stage.Missing;

        public Stage Data { get; set; } = Stage.Missing;

        public bool Experiments { get; set; } = false;

        public override string ToString() => Path.GetFileName(DB);
    }
}
