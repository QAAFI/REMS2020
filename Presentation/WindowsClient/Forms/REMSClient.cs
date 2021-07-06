using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common.Interfaces;
using WindowsClient.Models;

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

            QueryManager.Provider = provider;
            homeScreen.Manager = provider.GetRequiredService<IFileManager>();
            
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
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
            Left = Properties.Settings.Default.Left;
            Top  = Properties.Settings.Default.Top;
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;
            Properties.Settings.Default.Left = Left;
            Properties.Settings.Default.Top = Top;
            Properties.Settings.Default.Save();
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
