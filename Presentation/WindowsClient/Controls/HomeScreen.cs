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
using Rems.Infrastructure;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public delegate void PageAction(TabPage page);

    public partial class HomeScreen : UserControl
    {
        public event QueryHandler Query;
        public event Action<string> DBCreated;
        public event Action<string> DBOpened;

        public event EventHandler ImportRequested;
        public event LinkAction ImportCompleted;
        public event Action SessionChanging;
        public event PageAction PageCreated;

        /// <summary>
        /// All known sessions
        /// </summary>
        private List<Session> Sessions { get; }
        private List<Session> snoisses => Sessions.Reverse<Session>().ToList();


        private Session Session { get; set; }

        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public HomeScreen()
        {
            InitializeComponent();

            AttachLink(infoLink);
            AttachLink(expsLink);
            AttachLink(dataLink);

            Sessions = LoadSessions();
            recentList.DataSource = snoisses;

            recentList.DoubleClick += OnRecentListDoubleClick;
        }

        private void AddDetailerPage()
        {
            Session.Detailer.REMS += (s, e) => Query?.Invoke(s, e);            
            PageCreated?.Invoke(Session.Experiments);
            Session.Detailer.LoadNodes();

            LoadExportBox();
        }

        private void AttachLink(ImportLink link)
        {
            link.Clicked += (s, e) => ImportRequested?.Invoke(s, e);
            link.ImportComplete += OnImportComplete;            
        }

        private List<Session> LoadSessions()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;

            string file = Environment.GetFolderPath(local) + "\\REMS2020\\sessions.json";

            return JsonTools.LoadJson<List<Session>>(file, JsonTools.JsonLoad.New);
        }

        private void SaveSessions()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;

            string file = Environment.GetFolderPath(local) + "\\REMS2020\\sessions.json";

            //UpdateSession();
            JsonTools.SaveJson(file, Sessions);
        }

        /// <summary>
        /// Changes the current session
        /// </summary>
        /// <param name="session"></param>
        private async Task ChangeSession(Session session)
        {
            SessionChanging?.Invoke();

            // Open the DB from the new session
            await Query.Invoke(new OpenDBCommand() { FileName = session.DB });
            DBOpened?.Invoke(session.DB);
            folder = Path.GetDirectoryName(session.DB);

            Session = session;
            await CheckTables(session);

            // Reset the export box
            LoadExportBox();

            // Clear the links
            infoLink.Importer.Data = null;
            expsLink.Importer.Data = null;
            dataLink.Importer.Data = null;

            // Reorder the list
            Sessions.Remove(session);
            Sessions.Add(session);

            // Limit the number of sessions to 8
            if (Sessions.Count > 8)
                Sessions.RemoveAt(0);

            recentList.DataSource = snoisses;      
        }

        private async Task CheckTables(Session session)
        {
            if ((bool)await Query.Invoke(new LoadedInformation()))
                infoLink.Stage = /*session.Info =*/ Stage.Imported;
            else
                infoLink.Stage = Stage.Missing;

            if ((bool)await Query.Invoke(new LoadedExperiments()))
            {
                expsLink.Stage = /*session.Exps =*/ Stage.Imported;
                AddDetailerPage();
            }
            else
                expsLink.Stage = Stage.Missing;

            if ((bool)await Query.Invoke(new LoadedData()))
                dataLink.Stage = /*session.Data =*/ Stage.Imported;
            else
                dataLink.Stage = Stage.Missing;
        }

        private async Task CreateSession(string file)
        {           

            var session = new Session() { DB = file };

            // If overwriting a .db, remove the old session
            if (Sessions.Find(s => s.DB == file) is Session S)
                Sessions.Remove(S);

            // Change to the new session
            await ChangeSession(session);            
        }

        private void OnImportComplete(ImportLink link)
        {
            ImportCompleted?.Invoke(link);

            if (link == expsLink)
            {
                LoadExportBox();
                AddDetailerPage();
            }
        }

        private async void LoadExportBox()
        {
            exportList.Items.Clear();

            var exps = (await Query.Invoke(new ExperimentsQuery())) as IEnumerable<KeyValuePair<int, string>>;
            var items = exps.Select(e => e.Value).ToArray();
            exportList.Items.AddRange(items);
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

                await Query.Invoke(new CreateDBCommand() { FileName = save.FileName });
                DBCreated?.Invoke(save.FileName);

                await CreateSession(save.FileName);                
            }
        }

        private async void OnOpenClick(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = folder;
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() != DialogResult.OK) return;

                if (Sessions.FirstOrDefault(s => s.DB == open.FileName) is Session session)
                    await ChangeSession(session);
                else
                    await CreateSession(open.FileName);

                folder = Path.GetDirectoryName(open.FileName);

                await Query.Invoke(new OpenDBCommand() { FileName = open.FileName });                                
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
                    var exporter = new ApsimXporter
                    {
                        Experiments = exportList.CheckedItems.Cast<string>()
                    };

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

        public void Close()
        {
            SaveSessions();
        }

        private async void OnRecentListDoubleClick(object sender, EventArgs e)
        {
            if (recentList.SelectedItem is Session session)
                await ChangeSession(session);

            recentList.SelectedIndex = -1;
        }
    }
}
