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
using Newtonsoft.Json;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure;
using WindowsClient.Controls;
using WindowsClient.Models;

namespace WindowsClient
{
    public enum TagType
    {
        Empty,
        Experiment,
        Treatment,
        Plot
    }

    public struct NodeTag
    {
        public int ID { get; set; }

        public TagType Type { get; set; }
    }

    public partial class REMSClient : Form
    {
        private readonly IMediator _mediator;

        public REMSClient(IServiceProvider provider)
        {
            InitializeComponent();

            _mediator = provider.GetRequiredService<IMediator>();

            LoadSettings();
            
            FormClosed += REMSClientFormClosed;

            //experimentDetailer.REMS += _mediator.Send;

            homeScreen.Query += _mediator.Send;
            homeScreen.DBCreated += LoadListView;
            homeScreen.DBOpened += OnDBOpened;
            homeScreen.ImportRequested += OnImportRequested;
            homeScreen.ImportCompleted += OnImportCompleted;
            homeScreen.SessionChanging += OnSessionChanged;
            homeScreen.PageCreated += OnPageCreated;
        }

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

        private void OnPageCreated(TabPage page)
        {
            notebook.TabPages.Add(page);
        }

        private void OnSessionChanged()
        {
            // Remove all pages except the homescreen
            var pages = notebook.TabPages.Cast<TabPage>().Skip(2);

            foreach (var page in pages)
                notebook.TabPages.Remove(page);
        }

        private void OnImportRequested(object sender, EventArgs e)
        {
            var link = sender as ImportLink;

            link.Importer.Query = _mediator.Send;

            if (!notebook.TabPages.Contains(link.Tab))
                notebook.TabPages.Add(link.Tab);
            notebook.SelectedTab = link.Tab;

            if (!link.Importer.SelectFile())
                notebook.TabPages.Remove(link.Tab);
        }

        private void OnImportCompleted(ImportLink link)
        {
            MessageBox.Show("Import successful!", "");

            notebook.TabPages.Remove(link.Tab);
        }

        private void OnDBOpened(string file)
        {
            // Update the title
            Text = "REMS 2020 - " + Path.GetFileName(file);

            // Update the tables
            LoadListView(file);
        }

        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await TryQueryREMS(new DataTableQuery() { TableName = item });
            dataGridView.Format();
        }

        private async void LoadListView(string file)
        {
            var items = await new GetTableListQuery().Send(TryQueryREMS);

            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(items.ToArray());
        }

        public Task<object> TryQueryREMS(object request, CancellationToken token = default)
        {
            Application.UseWaitCursor = true;

            List<Exception> errors = new List<Exception>();

            try
            {
                var task = _mediator.Send(request);

                Application.UseWaitCursor = false;
                return task;
            }
            catch (Exception error)
            {
                while (error.InnerException != null) error = error.InnerException;
                errors.Add(error);
            }

            var builder = new StringBuilder();

            foreach (var error in errors)
            {
                builder.AppendLine(error.Message + "at\n" + error.StackTrace + "\n");
            }

            MessageBox.Show(builder.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Application.UseWaitCursor = false;

            return null;
        }

        public string ParseText(string file)
        {
            var filePath = Path.Combine("DataFiles", "apsimx", file);
            StringBuilder builder = new StringBuilder();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream) builder.AppendLine(reader.ReadLine());
            }

            builder.Replace("\r", "");
            return builder.ToString();
        }

        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            homeScreen.Close();
            SaveSettings();
        }
    }
}
