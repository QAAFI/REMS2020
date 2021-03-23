using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IMediator _mediator;

        public REMSClient(IServiceProvider provider)
        {
            InitializeComponent();

            _mediator = provider.GetRequiredService<IMediator>();

            LoadSettings();
            
            FormClosed += REMSClientFormClosed;

            homeScreen.Query += SendQuery;
            homeScreen.DBCreated += LoadListView;
            homeScreen.DBOpened += OnDBOpened;
            homeScreen.ImportRequested += OnImportRequested;
            homeScreen.ImportCompleted += OnImportCompleted;
            homeScreen.SessionChanging += OnSessionChanged;
            homeScreen.PageCreated += OnPageCreated;

            homeScreen.Manager = provider.GetRequiredService<IFileManager>();
        }

        /// <summary>
        /// Sends a query to the mediator
        /// </summary>
        /// <param name="query">The query object</param>
        private async Task<object> SendQuery(object query)
        {
            Application.UseWaitCursor = true;
            var result = await _mediator.Send(query);
            Application.UseWaitCursor = false;

            return result;
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
        /// When a new tab is added to the client
        /// </summary>
        private void OnPageCreated(TabPage page) => notebook.TabPages.Add(page);

        /// <summary>
        /// When the client connects to a different database
        /// </summary>
        private void OnSessionChanged()
        {
            // Remove all pages except the homescreen
            var pages = notebook.TabPages.Cast<TabPage>().Skip(2);

            foreach (var page in pages)
                notebook.TabPages.Remove(page);
        }

        /// <summary>
        /// When an import link starts the import process
        /// </summary>
        private async void OnImportRequested(object sender, EventArgs e)
        {
            var link = sender as ImportLink;

            link.Importer.Query += SendQuery;

            if (!notebook.TabPages.Contains(link.Tab))
            {
                notebook.TabPages.Add(link.Tab);
                link.Tab.Leave += (s, o) => notebook.TabPages.Remove(link.Tab);
            }
                
            notebook.SelectedTab = link.Tab;

            if (! await link.Importer.OpenFile())
                notebook.TabPages.Remove(link.Tab);
        }         

        /// <summary>
        /// When an import link confirms the import has finished
        /// </summary>
        private void OnImportCompleted(ImportLink link)
        {
            MessageBox.Show("Import successful!", "");

            notebook.TabPages.Remove(link.Tab);
        }

        /// <summary>
        /// When a new database is opened
        /// </summary>
        private void OnDBOpened(string file)
        {
            // Update the title
            Text = "REMS 2020 - " + Path.GetFileName(file);

            // Update the tables
            LoadListView(file);
        }

        /// <summary>
        /// When a different item in the list box is selected
        /// </summary>
        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await TrySendQuery(new DataTableQuery() { TableName = item });
            dataGridView.Format();
        }

        /// <summary>
        /// Fills the listbox
        /// </summary>
        private async void LoadListView(string file)
        {
            var query = new GetTableNamesQuery();
            var items = await TrySendQuery(query);

            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(items);
        }

        /// <summary>
        /// Attempts to send a query to the mediator
        /// </summary>
        public async Task<T> TrySendQuery<T>(IRequest<T> request, CancellationToken token = default)
        {
            List<Exception> errors = new List<Exception>();

            try
            {
                return (T) await SendQuery(request);
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                errors.Add(error);
            }

            var builder = new StringBuilder();

            foreach (var error in errors)
                builder.AppendLine(error.Message + "at\n" + error.StackTrace + "\n");            

            MessageBox.Show(builder.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.UseWaitCursor = false;

            return default;
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
