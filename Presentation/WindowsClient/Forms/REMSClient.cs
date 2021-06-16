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
            var local = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(local) + "\\REMS2020\\settings";

            if (File.Exists(path))
            {
                var stream = new FileStream(path, FileMode.Open);
                var reader = new StreamReader(stream);

                Width = Convert.ToInt32(reader.ReadLine());
                Height = Convert.ToInt32(reader.ReadLine());
                Left = Convert.ToInt32(reader.ReadLine());
                Top  = Convert.ToInt32(reader.ReadLine());

                reader.Close();
            }
        }

        /// <summary>
        /// Saves the settings to file
        /// </summary>
        private void SaveSettings()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(local) + "\\REMS2020\\settings";

            var stream = new FileStream(path, FileMode.Create);
            var writer = new StreamWriter(stream);

            writer.WriteLine(Width);
            writer.WriteLine(Height);
            writer.WriteLine(Left);
            writer.WriteLine(Top);
            writer.Close();
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
