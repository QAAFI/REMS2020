using NLog;
using System;
using System.Windows.Forms;
using WindowsClient.Controls;
using WindowsClient.Forms;
using WindowsClient.Models;
using Settings = WindowsClient.Properties.Settings;

namespace WindowsClient
{
    /// <summary>
    /// Manages and integrates the individual UI components in REMS.
    /// Handles the connection to the database.
    /// </summary>
    public partial class REMSClient : Form
    {
        private Logger log;

        public TabPage DetailPage { get; } = new TabPage("Experiments");

        public TabPage ImportPage { get; } = new TabPage("Importer");

        private Importer importer = new();

        public REMSClient(IServiceProvider provider)
        {
            log = LogManager.GetCurrentClassLogger();
            log.Info("Launching client.");

            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            QueryManager.Instance.Provider = provider;
            
            LoadSettings();

            FormClosed += REMSClientFormClosed;

            homeScreen.LinkClicked += OnImportLinkClicked;
            homeScreen.SessionChanged += OnSessionChanged;

            importer.ImportCompleted += OnImportCompleted;
            importer.ImportCancelled += OnImportCancelled;

            ImportPage.Leave += OnImportCancelled;
            ImportPage.Controls.Add(importer);

            DetailPage.Controls.Add(homeScreen.detailer);
        }

        private void OnSessionChanged(object sender, EventArgs e)
        {
            if (sender is not Session session)
                return;

            if (session.HasExperiments && !notebook.TabPages.Contains(DetailPage))
                notebook.TabPages.Add(DetailPage);
            else if (notebook.TabPages.Contains(DetailPage))
                notebook.TabPages.Remove(DetailPage);
        }

        /// <summary>
        /// Loads the settings from file
        /// </summary>
        private void LoadSettings()
        {
            log.Info("Loading stored settings.");

            Width = Settings.Default.Width;
            Height = Settings.Default.Height;
            Left = Settings.Default.Left;
            Top  = Settings.Default.Top;

            if (Settings.Default.ImportPath == "")
                Settings.Default.ImportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (Settings.Default.ExportPath == "")
                Settings.Default.ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        private void SaveSettings()
        {
            Settings.Default.Width = Width;
            Settings.Default.Height = Height;
            Settings.Default.Left = Left;
            Settings.Default.Top = Top;
            Settings.Default.Save();
        }

        /// <summary>
        /// When an import link starts the import process
        /// </summary>
        private async void OnImportLinkClicked(object sender, EventArgs args)
        {
            if (sender is not ImportLink link)
                throw new Exception("Import requested from unknown control type.");

            ImportPage.Text = "Import " + link.Label;

            notebook.TabPages.Add(ImportPage);
            notebook.SelectedTab = ImportPage;

            await importer.OpenFile(link.Label);
        }

        private async void OnImportCompleted(object sender, EventArgs e)
        {
            notebook.TabPages.Remove(ImportPage);

            AlertBox.Show("Import successful!", AlertType.Success);

            if (notebook.TabPages.Contains(DetailPage))
                notebook.TabPages.Remove(DetailPage);
            
            homeScreen.DisplayImport();

            if (await homeScreen.DisplayExperiments())
            {
                notebook.TabPages.Add(DetailPage);
                notebook.SelectedTab = DetailPage;
            }

            notebook.Enabled = true;
        }

        private void OnImportCancelled(object sender, EventArgs e)
        {
            importer.Reset();
            notebook.TabPages.Remove(ImportPage);
        }

        /// <summary>
        /// When the client is closed
        /// </summary>
        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            homeScreen.Close();
            SaveSettings();
        }
    }
}
