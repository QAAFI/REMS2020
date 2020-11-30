using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;
using System.IO;
using WindowsClient.Forms;
using Rems.Infrastructure.ApsimX;

namespace WindowsClient.Controls
{
    public partial class HomeScreen : UserControl
    {
        public event QueryHandler Query;
        public event Action DBCreated;
        public event Action DBOpened;
        public event EventHandler ImportRequested;

        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public HomeScreen()
        {
            InitializeComponent();

            infoLink.Clicked += LinkClicked;
            expsLink.Clicked += LinkClicked;
            dataLink.Clicked += LinkClicked;
        }

        private void LinkClicked(object sender, EventArgs e)
        {
            ImportRequested?.Invoke(sender, e);
        }

        private async void OnCreateClick(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = folder;
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() != DialogResult.OK) return;

                folder = Path.GetDirectoryName(save.FileName);

                await Query.Invoke(new CreateDBCommand() { FileName = save.FileName });
                await Query.Invoke(new OpenDBCommand() { FileName = save.FileName });

                DBCreated?.Invoke();
            }
        }

        private async void OnOpenClick(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = folder;
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() != DialogResult.OK) return;

                folder = Path.GetDirectoryName(open.FileName);

                await Query.Invoke(new OpenDBCommand() { FileName = open.FileName });

                DBOpened?.Invoke();
            }
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
                save.InitialDirectory = folder;
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var exporter = new ApsimXporter();
                    exporter.Query += Query;
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
