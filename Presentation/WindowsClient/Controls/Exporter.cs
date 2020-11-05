using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Infrastructure.ApsimX;
using Rems.Application.Common;
using Rems.Application.CQRS;
using WindowsClient.Forms;

namespace WindowsClient.Controls
{
    public partial class Exporter : UserControl
    {
        public QueryHandler Query;

        public CommandHandler Command;

        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private ApsimXporter exporter;

        public Exporter()
        {
            InitializeComponent();
        }

        public void Initialise()
        {
            exporter = new ApsimXporter(Query, Command);
            exporter.ItemNotFound += exportValidater.HandleMissingItem;

            exportValidater.SendQuery = Query;

            exportValidater.FillRows();
        }

        private async void OnExportClick(object sender, EventArgs e)
        {
            bool connected = (bool)await Query(new ConnectionExists());
            if (!connected)
            {
                MessageBox.Show("A database must be opened before exporting.");
                return;
            }

            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Folder;
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() != DialogResult.OK) return;

                try
                {
                    exporter.FileName = save.FileName;
                    var dialog = new ProgressDialog(exporter, "Exporting...");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
