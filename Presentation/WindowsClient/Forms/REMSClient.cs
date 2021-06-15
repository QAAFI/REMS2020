using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using WindowsClient.Controls;
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

            homeScreen.DBOpened += OnDBOpened;
            homeScreen.ImportRequested += OnImportRequested;

            importer.ImportCompleted += OnImportCompleted;
            importer.ImportCancelled += (s, e) => notebook.TabPages.Remove(importTab);
            importTab.Leave += (s, e) => notebook.TabPages.Remove(importTab);

            notebook.TabPages.Remove(detailsTab);
            notebook.TabPages.Remove(importTab);
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

        /// <summary>
        /// When an import link starts the import process
        /// </summary>
        private async void OnImportRequested(object sender, EventArgs args)
        {
            if (!(sender is ImportLink link))
                throw new Exception("Import requested from unknown control type.");

            importer.ImportCompleted += (s, e) => link.Stage = Stage.Imported;
            importer.ImportFailed += (s, e) => link.Stage = Stage.Missing;

            importTab.Text = "Import " + link.Label;
            await AttachImporter();
        }         

        /// <summary>
        /// When an import link confirms the import has finished
        /// </summary>
        private async void OnImportCompleted(object sender, EventArgs args)
        {
            MessageBox.Show("Import successful!", "");
            notebook.TabPages.Remove(importTab);
            await homeScreen.LoadExportBox();
            await AttachDetailer();
        }

        private async Task AttachImporter()
        {
            notebook.TabPages.Add(importTab);
            notebook.SelectedTab = importTab;

            await importer.OpenFile();
        }

        private async Task AttachDetailer()
        {
            if (notebook.TabPages.Contains(detailsTab))
                return;

            if (!await QueryManager.Request(new LoadedExperiments()))
                return;
            
            notebook.TabPages.Add(detailsTab);
            await detailer.LoadNodes();
        }

        /// <summary>
        /// When a new database is opened
        /// </summary>
        private async void OnDBOpened(object sender, Args<string> args)
        {
            // Update the title
            Text = "REMS 2020 - " + Path.GetFileName(args.Item);

            // Update the tables
            await AttachDetailer();
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
