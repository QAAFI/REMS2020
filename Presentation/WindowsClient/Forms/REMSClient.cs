using System;
using System.Linq;
using System.Windows.Forms;
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
        public REMSClient(IServiceProvider provider)
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            QueryManager.Provider = provider;
            
            LoadSettings();

            FormClosed += REMSClientFormClosed;

            homeScreen.AttachTab += OnAttachTab;
            homeScreen.RemoveTab += OnRemoveTab;
        }

        /// <summary>
        /// Loads the settings from file
        /// </summary>
        private void LoadSettings()
        {
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

        private void OnAttachTab(object sender, EventArgs e)
        {
            if (sender is not Control control)
                throw new Exception("Source object was not a control");

            var existing = notebook.TabPages.OfType<TabPage>().FirstOrDefault(t => t.Text == control.Name);
            if (existing is not null)
                return;

            var tab = new TabPage(control.Name);
            tab.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            notebook.TabPages.Add(tab);

            // Assume that a 'true' tag means that the client should switch to the new tab
            if (control.Tag is true)
                notebook.SelectedTab = tab;
        }

        private void OnRemoveTab(object sender, EventArgs e)
        {
            if (sender is not Control control)
                throw new Exception("Source object was not a control");

            var tab = notebook.TabPages.OfType<TabPage>().FirstOrDefault(t => t.Text == control.Name);

            if (tab is not null)
                notebook.TabPages.Remove(tab);                
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
