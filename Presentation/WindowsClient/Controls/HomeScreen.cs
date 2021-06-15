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
using Rems.Application.Common;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages database selection and file import/export
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        /// <summary>
        /// Occurs when excel data is requested
        /// </summary>
        public event EventHandler ImportRequested;

        public event EventHandler AttachTab;
        public event EventHandler RemoveTab;

        public IFileManager Manager { get; set; }

        private Importer importer = new();
        private ExperimentDetailer detailer = new();

        /// <summary>
        /// All known sessions
        /// </summary>
        private List<Session> Sessions { get; }

        /// <summary>
        /// The list of sessions reversed
        /// </summary>
        private List<Session> snoisses => Sessions.Reverse<Session>().ToList();

        public HomeScreen()
        {
            InitializeComponent();

            importer.ImportCompleted += OnImportCompleted;
            importer.ImportCancelled += (s, e) => RemoveTab.Invoke(s, e);

            infoLink.Clicked += OnImportLinkClicked;
            expsLink.Clicked += OnImportLinkClicked;
            dataLink.Clicked += OnImportLinkClicked;

            Sessions = LoadSessions();
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

            JsonTools.SaveJson(file, Sessions);
        }

        /// <summary>
        /// Create a new session for a database
        /// </summary>
        /// <param name="file">The path to the .db file</param>
        /// <returns></returns>
        private async Task CreateSession(string file)
        {
            var session = new Session { DB = file };

            // If overwriting a .db, remove the old session
            if (Sessions.Find(s => s.DB == file) is Session S)
            {
                Sessions.Remove(S);
                File.Delete(file);
            }

            // Change to the new session
            await ChangeSession(session);
        }

        /// <summary>
        /// Changes the current session
        /// </summary>
        /// <param name="session"></param>
        private async Task ChangeSession(Session session)
        {
            // Open the DB from the new session
            Manager.DbConnection = session.DB;            
            Manager.ImportFolder = Path.GetDirectoryName(session.DB);

            EnableImport();

            infoLink.Stage = session.HasInformation ? Stage.Imported : Stage.Missing;
            expsLink.Stage = session.HasExperiments ? Stage.Imported : Stage.Missing;
            dataLink.Stage = session.HasData ? Stage.Imported : Stage.Missing;

            // Reset the export box
            exportList.Items.Clear();
            if (session.HasExperiments)
            {
                await LoadExportBox();
                AttachTab.Invoke(detailer, EventArgs.Empty);
            }
            else
                RemoveTab.Invoke(detailer, EventArgs.Empty);

            // Reorder the list
            Sessions.Remove(session);
            Sessions.Add(session);

            // Limit the number of sessions to 8
            if (Sessions.Count > 8)
                Sessions.RemoveAt(0);

            recentList.DataSource = snoisses;

            ParentForm.Text = "REMS2020 - " + session.DB;
        }
        #endregion

        /// <summary>
        /// Activates the <see cref="ImportLink"/> controls if a database is connected
        /// </summary>
        private void EnableImport()
        {
            importText.Text = "Select one of the above options to begin the import process."
            + "\n\n"
            + "The data must come from an excel document based on the template file,"
            + "or the importer will not recognise it."
            + "\n\n"
            + "The data must be imported in the listed order (i.e., information before experiments)."
            + "\n\n"
            + "A green tick indicates that some data is already present in the database.";

            infoLink.Active = true;
            expsLink.Active = true;
            dataLink.Active = true;
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

            importer.Name = link.Label;
            AttachTab.Invoke(importer, EventArgs.Empty);
            await importer.OpenFile();
        }

        private async void OnImportCompleted(object sender, EventArgs args)
        {
            MessageBox.Show("Import successful!", "");

            RemoveTab.Invoke(importer, EventArgs.Empty);
            
            await LoadExportBox();
            AttachTab.Invoke(detailer, EventArgs.Empty);
        }

        /// <summary>
        /// Adds the available experiments to the export box
        /// </summary>
        public async Task LoadExportBox()
        {
            var exps = (await QueryManager.Request(new ExperimentsQuery())) as IEnumerable<KeyValuePair<int, string>>;
            var items = exps.Select(e => e.Value).Distinct().ToArray();
            
            exportList.Items.AddRange(items);
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
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Manager.ImportFolder;
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() != DialogResult.OK) return;

                if (Sessions.FirstOrDefault(s => s.DB == open.FileName) is Session session)
                    await ChangeSession(session);
                else
                    await CreateSession(open.FileName);

                Manager.DbConnection = open.FileName;                                
            }
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

            using (var save = new SaveFileDialog())
            {
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
            if (recentList.SelectedItem is Session session)
                await ChangeSession(session);

            recentList.SelectedIndex = -1;
        }
    }
}
