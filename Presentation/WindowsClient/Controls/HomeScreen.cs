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
using MediatR;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages database selection and file import/export
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        /// <summary>
        /// Occurs when a new database is created
        /// </summary>
        public event Action<string> DBCreated;

        /// <summary>
        /// Occurs when a database is opened
        /// </summary>
        public event Action<string> DBOpened;

        /// <summary>
        /// Occurs when excel data is requested
        /// </summary>
        public event EventHandler ImportRequested;

        /// <summary>
        /// Occurs when data has finished importing
        /// </summary>
        public event Action<ImportLink> ImportCompleted;

        /// <summary>
        /// Occurs immediately before the session changes
        /// </summary>
        public event Action SessionChanging;

        /// <summary>
        /// Occurs when a new page needs to be created
        /// </summary>
        public event Action<TabPage> PageCreated;

        /// <summary>
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;
        
        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="query">The request object</param>
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        /// <summary>
        /// All known sessions
        /// </summary>
        private List<Session> Sessions { get; }

        /// <summary>
        /// The list of sessions reversed
        /// </summary>
        private List<Session> snoisses => Sessions.Reverse<Session>().ToList();

        /// <summary>
        /// The currently active session
        /// </summary>
        private Session Session { get; set; }

        /// <summary>
        /// The most recently accessed folder
        /// </summary>
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
            exportTracker.TaskBegun += OnExportClick;
        }

        /// <summary>
        /// Adds an experiment detailer to the homescreen
        /// </summary>
        private void AddDetailerPage()
        {
            Session.Detailer.Query += (o) => Query?.Invoke(o);
            PageCreated?.Invoke(Session.Experiments);
            Session.Detailer.LoadNodes();
        }

        /// <summary>
        /// Connects the import link to the 
        /// </summary>
        /// <param name="link"></param>
        private void AttachLink(ImportLink link)
        {
            link.Clicked += (s, e) => ImportRequested?.Invoke(s, e);
            link.ImportComplete += OnImportComplete;            
        }

        /// <summary>
        /// Loads the list of recent sessions
        /// </summary>
        /// <returns></returns>
        private List<Session> LoadSessions()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;

            string file = Environment.GetFolderPath(local) + "\\REMS2020\\sessions.json";

            return JsonTools.LoadJson<List<Session>>(file, JsonTools.JsonLoad.New);
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
            var session = new Session() { DB = file };

            // If overwriting a .db, remove the old session
            if (Sessions.Find(s => s.DB == file) is Session S)
                Sessions.Remove(S);

            // Change to the new session
            await ChangeSession(session);
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

            EnableImport();

            Session = session;
            await CheckTables();

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
        /// Check if any data has previously been loaded
        /// </summary>
        private async Task CheckTables()
        {
            if (await InvokeQuery(new LoadedInformation()))
                infoLink.Stage = Stage.Imported;
            else
                infoLink.Stage = Stage.Missing;

            if (await InvokeQuery(new LoadedExperiments()))
            {
                expsLink.Stage = Stage.Imported;
                AddDetailerPage();
            }
            else
                expsLink.Stage = Stage.Missing;

            if (await InvokeQuery(new LoadedData()))
                dataLink.Stage = Stage.Imported;
            else
                dataLink.Stage = Stage.Missing;
        }

        /// <summary>
        /// Handles the post-import functions
        /// </summary>
        private void OnImportComplete(ImportLink link)
        {
            ImportCompleted?.Invoke(link);

            if (link == expsLink)
            {
                LoadExportBox();
                AddDetailerPage();
            }
        }

        /// <summary>
        /// Adds the available experiments to the export box
        /// </summary>
        private async void LoadExportBox()
        {
            var exps = (await InvokeQuery(new ExperimentsQuery())) as IEnumerable<KeyValuePair<int, string>>;
            var items = exps.Select(e => e.Value).Distinct().ToArray();

            exportList.Items.Clear();
            exportList.Items.AddRange(items);
        }

        /// <summary>
        /// Handles the creation of a new database
        /// </summary>
        private async void OnCreateClick(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = folder;
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() != DialogResult.OK) return;

                await InvokeQuery(new CreateDBCommand() { FileName = save.FileName });
                DBCreated?.Invoke(save.FileName);

                await CreateSession(save.FileName);                
            }
        }

        /// <summary>
        /// Handles the opening of an existing database
        /// </summary>
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

                await InvokeQuery(new OpenDBCommand() { FileName = open.FileName });                                
            }
        }

        /// <summary>
        /// Handles the export of .apsimx files
        /// </summary>
        private async void OnExportClick()
        {
            // Check that a valid database exists
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

                    exporter.Query += (o) => Query?.Invoke(o);
                    exporter.FileName = save.FileName;
                    
                    exportTracker.SetSteps(exporter);

                    exporter.NextItem += exportTracker.OnNextTask;
                    exporter.IncrementProgress += exportTracker.OnProgressChanged;
                    exporter.TaskFinished += () => MessageBox.Show("Export complete!");
                    exporter.TaskFailed += exportTracker.OnTaskFailed;

                    await exporter.Run();

                    exportTracker.Reset();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
