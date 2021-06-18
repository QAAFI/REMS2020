using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rems.Application.CQRS;
using Rems.Infrastructure.ApsimX;
using Rems.Infrastructure;

using WindowsClient.Models;
using Rems.Application.Common.Interfaces;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages database selection and file import/export
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        public event EventHandler AttachTab;
        public event EventHandler RemoveTab;

        public IFileManager Manager { get; set; }

        private Importer importer = new();
        private ExperimentDetailer detailer = new();

        private Session session;

        /// <summary>
        /// All known sessions
        /// </summary>
        private List<Session> sessions { get; }

        /// <summary>
        /// The list of sessions reversed
        /// </summary>
        private List<Session> snoisses => sessions.Reverse<Session>().ToList();

        public HomeScreen()
        {
            InitializeComponent();

            importer.ImportCompleted += OnImportCompleted;
            importer.ImportCancelled += (s, e) => RemoveTab.Invoke(s, e);

            infoLink.Clicked += OnImportLinkClicked;
            expsLink.Clicked += OnImportLinkClicked;
            dataLink.Clicked += OnImportLinkClicked;

            sessions = LoadSessions();
            recentList.DataSource = snoisses;

            recentList.DoubleClick += OnRecentListDoubleClick;
            exportTracker.TaskBegun += OnExportClick;
        }

        #region Sessions
        /// <summary>
        /// Loads the list of recent sessions
        /// </summary>
        /// <returns></returns>
        private List<Session> LoadSessions()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;

            string file = Environment.GetFolderPath(local) + "\\REMS2020\\sessions.json";

            var info = new FileInfo(file);

            return JsonTools.LoadJson<List<Session>>(info, JsonTools.JsonLoad.New);
        }

        /// <summary>
        /// Saves the list of recent sessions
        /// </summary>
        private void SaveSessions()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(local);
            Directory.CreateDirectory(path + "\\REMS2020");
            string file = path + "\\REMS2020\\sessions.json";

            JsonTools.SaveJson(file, sessions);
        }

        /// <summary>
        /// Create a new session for a database
        /// </summary>
        /// <param name="file">The path to the .db file</param>
        /// <returns></returns>
        private async Task CreateSession(string file)
        {
            session = new Session { DB = file };

            // If overwriting a .db, remove the old session
            if (sessions.Find(s => s.DB == file) is Session s)
            {
                sessions.Remove(s);
                File.Delete(file);
            }            

            // Change to the new session
            await RefreshSession();
        }

        /// <summary>
        /// Changes the current session
        /// </summary>
        /// <param name="session"></param>
        private async Task RefreshSession()
        {
            // Open the DB from the new session
            Manager.DbConnection = session.DB;            
            Manager.ImportFolder = Path.GetDirectoryName(session.DB);            

            await DisplayImport();            

            // Reset the export box
            await DisplayExperiments();

            UpdateRecents();

            ParentForm.Text = "REMS2020 - " + Path.GetFileName(session.DB);
        }
        #endregion

        /// <summary>
        /// Activates the <see cref="ImportLink"/> controls if a database is connected
        /// </summary>
        private async Task DisplayImport()
        {
            session.HasInformation = await QueryManager.Request(new LoadedInformation());
            session.HasExperiments = await QueryManager.Request(new LoadedExperiments());
            session.HasData = await QueryManager.Request(new LoadedData());

            // Set visibility based on the session status
            if (!(groupImport.Visible = session is not null))
                return;

            string text = "Click one of the links above to begin the import process.\n";

            infoLink.Active = true;

            if (!(expsLink.Active = session.HasInformation))
                text += "\nInformation must be loaded before experiments.\n";
            else
                text += "\nAble to load experiments, or click information again to include additional content.\n";

            if (!(dataLink.Active = session.HasExperiments))
                text += "\nExperiments must be loaded before data.\n";
            else
                text += "\nAble to load data, or click the previous links to include additional content.\n";

            importText.Text = text;

            infoLink.Stage = session.HasInformation ? Stage.Imported : Stage.Missing;
            expsLink.Stage = session.HasExperiments ? Stage.Imported : Stage.Missing;
            dataLink.Stage = session.HasData ? Stage.Imported : Stage.Missing;
        }

        /// <summary>
        /// When an import link starts the import process
        /// </summary>
        private async void OnImportLinkClicked(object sender, EventArgs args)
        {
            if (!(sender is ImportLink link))
                throw new Exception("Import requested from unknown control type.");

            importer.ImportCompleted += (s, e) => link.Stage = Stage.Imported;
            importer.ImportFailed += (s, e) => link.Stage = Stage.Missing;

            importer.Name = "Import " + link.Label;
            AttachTab.Invoke(importer, EventArgs.Empty);

            importer.Leave += (s, e) => RemoveTab.Invoke(importer, EventArgs.Empty);

            await importer.OpenFile(link.Label);
        }

        private async void OnImportCompleted(object sender, EventArgs args)
        {
            MessageBox.Show("Import successful!", "");

            RemoveTab.Invoke(importer, EventArgs.Empty);

            await DisplayImport();
            await DisplayExperiments();
        }

        /// <summary>
        /// Adds the available experiments to the export box
        /// </summary>
        public async Task DisplayExperiments()
        {
            exportList.Items.Clear();

            if (session.HasExperiments)
            {
                groupExport.Visible = true;

                var exps = (await QueryManager.Request(new ExperimentsQuery())) as IEnumerable<KeyValuePair<int, string>>;
                var items = exps.Select(e => e.Value).Distinct().ToArray();

                exportList.Items.AddRange(items);

                AttachTab.Invoke(detailer, EventArgs.Empty);
            }
            else
            {
                groupExport.Visible = false;
                RemoveTab.Invoke(detailer, EventArgs.Empty);
            }
        }

        public void UpdateRecents()
        {
            // Reorder the list
            sessions.Remove(session);
            sessions.Add(session);

            // Limit the number of sessions to 8
            if (sessions.Count > 8)
                sessions.RemoveAt(0);

            recentList.DataSource = snoisses;
        }

        /// <summary>
        /// Handles the creation of a new database
        /// </summary>
        private async void OnCreateClick(object sender, EventArgs e)
        {
            using var save = new SaveFileDialog();

            save.InitialDirectory = Manager.ImportFolder;
            save.AddExtension = true;
            save.Filter = "SQLite (*.db)|*.db";
            save.RestoreDirectory = true;

            if (save.ShowDialog() != DialogResult.OK) return;

            await CreateSession(save.FileName);
        }

        /// <summary>
        /// Handles the opening of an existing database
        /// </summary>
        private async void OnOpenClick(object sender, EventArgs e)
        {
            using var open = new OpenFileDialog();
            open.InitialDirectory = Manager.ImportFolder;
            open.Filter = "SQLite (*.db)|*.db";

            if (open.ShowDialog() != DialogResult.OK) return;

            if (sessions.FirstOrDefault(s => s.DB == open.FileName) is Session existing)
            {
                session = existing;
                await RefreshSession();
            }
            else
                await CreateSession(open.FileName);

            Manager.DbConnection = open.FileName;
        }

        /// <summary>
        /// Handles the export of .apsimx files
        /// </summary>
        private async void OnExportClick(object sender, EventArgs args)
        {
            // Check that a valid database exists
            bool connected = await QueryManager.Request(new ConnectionExists());
            if (!connected)
            {
                MessageBox.Show("A database must be opened before exporting.");
                return;
            }

            using var save = new SaveFileDialog();
            save.InitialDirectory = Manager.ExportFolder;
            save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

            if (save.ShowDialog() != DialogResult.OK) return;

            Manager.ExportFolder = Path.GetDirectoryName(save.FileName);

            try
            {
                using var exporter = new ApsimXporter
                {
                    Experiments = exportList.CheckedItems.Cast<string>(),
                    FileName = save.FileName,
                    Manager = FileManager.Instance
                };

                exportTracker.AttachRunner(exporter);

                exporter.Query += QueryManager.Request;
                exporter.TaskFinished += (s, e) => MessageBox.Show("Export complete!");

                await exporter.Run();

                exportTracker.Reset();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                exportTracker.Reset();
            }
        }

        /// <summary>
        /// Handles the closure of the control
        /// </summary>
        public void Close()
        {
            SaveSessions();
        }

        /// <summary>
        /// Handles a change in the selected recent items
        /// </summary>
        private async void OnRecentListDoubleClick(object sender, EventArgs e)
        {
            if (recentList.SelectedItem is not Session existing)
                return;

            session = existing;
            await RefreshSession();

            recentList.SelectedIndex = -1;
        }
    }
}
