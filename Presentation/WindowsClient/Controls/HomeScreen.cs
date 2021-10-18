using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;
using Rems.Application.CQRS;
using Rems.Infrastructure;
using Rems.Infrastructure.ApsimX;
using WindowsClient.Forms;
using WindowsClient.Models;

using Settings = WindowsClient.Properties.Settings;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages database selection and file import/export
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        public event EventHandler AttachTab;
        public event EventHandler RemoveTab;

        private Logger log;

        private Importer importer = new();
        private ExperimentDetailer detailer = new ExperimentDetailer { Name = "Experiments"};

        private Session session;
        private WebBrowser browser = new();

        /// <summary>
        /// Describes how the browser will render text
        /// </summary>
        string style = 
            "<style>\n" +
            "html *\n" +
            "{\n" +
            "   font-size: 12px !important;\n" +
            "   color: #000 !important;" +
            "   font-family: Arial !important; " +
            "}\n" +
            "</style>\n";

        public HomeScreen()
        {
            log = LogManager.GetCurrentClassLogger();

            log.Info("Loading home screen.");

            InitializeComponent();
            browser.Dock = DockStyle.Fill;            
            summaryBox.Controls.Add(browser);

            importer.ImportCompleted += OnImportCompleted;
            importer.ImportCancelled += (s, e) => RemoveTab.Invoke(s, e);

            infoLink.Clicked += OnImportLinkClicked;
            expsLink.Clicked += OnImportLinkClicked;
            dataLink.Clicked += OnImportLinkClicked;

            log.Info("Loading recent sessions list.");
            var sessions = LoadSessions().ToArray();
            recentList.Items.AddRange(sessions);
            recentList.MouseDown += OnRecentListClick;
            recentList.DoubleClick += OnRecentListDoubleClick;
            recentList.MouseHover += OnRecentsHover;

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

            return JsonTools.LoadJson<List<Session>>(info, JsonTools.JsonLoad.New) ?? new List<Session>();
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

            JsonTools.SaveJson(file, recentList.Items.OfType<Session>());
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
            if (recentList.Items.OfType<Session>().FirstOrDefault(s => s.DB == file) is Session s)
            {
                recentList.Items.Remove(s);
                File.Delete(file);
            }            

            // Change to the new session
            await RefreshSession().TryRun();
        }

        /// <summary>
        /// Changes the current session
        /// </summary>
        /// <param name="session"></param>
        private async Task RefreshSession()
        {
            // Open the DB from the new session
            FileManager.Instance.DbConnection = session.DB;

            // Set the link icons based on existing session data
            infoLink.HasData = session.HasInformation;
            expsLink.HasData = session.HasExperiments;
            dataLink.HasData = session.HasData;

            // Update the import display
            DisplayImport();          

            // Reset the export box
            await DisplayExperiments();

            UpdateRecents();

            ParentForm.Text = "REMS2020 - " + Path.GetFileName(session.DB);
        }
        #endregion

        /// <summary>
        /// Activates the <see cref="ImportLink"/> controls if a database is connected
        /// </summary>
        private void DisplayImport()
        {
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
        }

        /// <summary>
        /// When an import link starts the import process
        /// </summary>
        private async void OnImportLinkClicked(object sender, EventArgs args)
        {
            if (sender is not ImportLink link)
                throw new Exception("Import requested from unknown control type.");

            importer.Name = "Import " + link.Label;
            importer.Tag = true;
            AttachTab.Invoke(importer, EventArgs.Empty);            
            importer.Leave += (s, e) => RemoveTab.Invoke(importer, EventArgs.Empty);

            await importer.OpenFile(link.Label);
        }

        private async void OnImportCompleted(object sender, EventArgs e)
        {
            if (infoLink.WasClicked)
                infoLink.WasClicked = !(session.HasInformation = infoLink.HasData = true);

            if (expsLink.WasClicked)
                expsLink.WasClicked = !(session.HasExperiments = expsLink.HasData = true);

            if (dataLink.WasClicked)
                dataLink.WasClicked = !(session.HasData = dataLink.HasData = true);

            RemoveTab.Invoke(importer, EventArgs.Empty);

            AlertBox.Show("Import successful!", AlertType.Success);

            DisplayImport();
            await DisplayExperiments();
        }

        /// <summary>
        /// Adds the available experiments to the export box
        /// </summary>
        public async Task DisplayExperiments()
        {
            exportList.Items.Clear();

            groupExport.Visible = false;
            RemoveTab.Invoke(detailer, EventArgs.Empty);

            if (session.HasExperiments)
            {
                groupExport.Visible = true;

                var exps = await QueryManager.Request(new ExperimentsQuery());
                
                foreach (var exp in exps)
                    exportList.Items.Add(exp.Name);
                
                await detailer.LoadTreeView();
                detailer.Tag = session.HasData;
                AttachTab.Invoke(detailer, EventArgs.Empty);
            }
        }

        public void UpdateRecents()
        {
            // Reorder the list
            recentList.Items.Remove(session);
            recentList.Items.Insert(0, session);

            // Limit the number of sessions to 8
            if (recentList.Items.Count > 8)
                recentList.Items.RemoveAt(7);
        }

        /// <summary>
        /// Handles the creation of a new database
        /// </summary>
        private async void OnCreateClick(object sender, EventArgs e)
        {
            using var save = new SaveFileDialog();

            save.InitialDirectory = Settings.Default.ImportPath;
            save.AddExtension = true;
            save.Filter = "SQLite (*.db)|*.db";
            save.RestoreDirectory = true;

            if (save.ShowDialog() != DialogResult.OK) return;

            File.Delete(save.FileName);
            await CreateSession(save.FileName).TryRun();
        }

        /// <summary>
        /// Handles the opening of an existing database
        /// </summary>
        private async void OnOpenClick(object sender, EventArgs e)
        {
            using var open = new OpenFileDialog();
            open.InitialDirectory = Settings.Default.ImportPath;
            open.Filter = "SQLite (*.db)|*.db";

            if (open.ShowDialog() != DialogResult.OK) return;

            if (recentList.Items.OfType<Session>().FirstOrDefault(s => s.DB == open.FileName) is Session existing)
            {
                session = existing;
                await RefreshSession().TryRun();
            }
            else
                await CreateSession(open.FileName).TryRun();

            FileManager.Instance.DbConnection = open.FileName;
        }

        /// <summary>
        /// Handles the export of .apsimx files
        /// </summary>
        private async void OnExportClick(object sender, EventArgs args)
        {
            // Check that a valid database exists            
            if (!FileManager.Connected)
            {
                AlertBox.Show("A database must be opened before exporting.", AlertType.Error);
                return;
            }

            using var save = new SaveFileDialog();
            save.InitialDirectory = Settings.Default.ExportPath;
            save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

            if (save.ShowDialog() != DialogResult.OK) return;

            FileManager.Instance.ExportPath = Settings.Default.ExportPath = Path.GetDirectoryName(save.FileName);
            
            using var exporter = new ApsimXporter
            {
                Experiments = exportList.CheckedItems.Cast<string>(),
                FilePath = save.FileName,
                Manager = FileManager.Instance,
                Handler = QueryManager.Instance
            };

            exportTracker.AttachRunner(exporter);

            var task = exporter.Run();
            await task.TryRun();
            
            if (task.IsCompletedSuccessfully)
            {
                AlertBox.Show("Export complete!", AlertType.Success);
                browser.DocumentText = style + Markdig.Markdown.ToHtml(exporter.Summary.Text);
                summaryBox.Visible = true;
            }
            else
            {
                AlertBox.Show("Export failed.\n" + task.Exception.InnerException.Message, AlertType.Error);
                browser.DocumentText = "";
                summaryBox.Visible = false;
            }

            exportTracker.Reset();
        }

        /// <summary>
        /// Handles the closure of the control
        /// </summary>
        public void Close()
        {
            SaveSessions();
        }

        private void OnRecentListClick(object sender, MouseEventArgs e)
        {
            // On right click
            if (e.Button == MouseButtons.Right)
            {
                var point = recentList.PointToClient(MousePosition);
                int index = recentList.IndexFromPoint(point);

                var menu = new ContextMenuStrip();
                if (index != -1)
                {
                    recentList.SelectedIndex = index;
                    EventHandler handler = (s, e) => { recentList.Items.RemoveAt(index); };
                    var item = new ToolStripMenuItem("Clear", null, handler);
                    menu.Items.Add(item);
                }
                else
                {
                    EventHandler handler = (s, e) => recentList.Items.Clear();
                    var item = new ToolStripMenuItem("Clear all", null, handler);
                    menu.Items.Add(item);
                }
                menu.Show(MousePosition);
            }
        }

        /// <summary>
        /// Handles a change in the selected recent items
        /// </summary>
        private async void OnRecentListDoubleClick(object sender, EventArgs e)
        {
            if (recentList.SelectedItem is not Session existing)
                return;

            session = existing;
            await RefreshSession().TryRun();

            recentList.SelectedIndex = -1;
        }

        private ToolTip tip = new ToolTip();
        private void OnRecentsHover(object sender, EventArgs e)
        {
            tip.RemoveAll();

            var mouse = MousePosition;
            var client = recentList.PointToClient(mouse);
            int index = recentList.IndexFromPoint(client);

            if (index == -1) return;

            var sess = recentList.Items[index] as Session;
            var text = sess.DB;
            tip.SetToolTip(recentList, text);
        }
    }
}
