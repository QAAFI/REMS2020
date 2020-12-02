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
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using WindowsClient.Controls;

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

            experimentDetailer.REMS += _mediator.Send;

            homeScreen.Query += _mediator.Send;
            homeScreen.DBCreated += LoadListView;
            homeScreen.DBOpened += UpdateAllComponents;
            homeScreen.ImportRequested += OnImportRequested;
        }

        private void OnImportRequested(object sender, EventArgs e)
        {
            var link = sender as ImportLink;

            link.Importer.Query = _mediator.Send;
            link.Importer.DatabaseChanged += UpdateAllComponents;

            if (!notebook.TabPages.Contains(link.Tab))
                notebook.TabPages.Add(link.Tab);
            notebook.SelectedTab = link.Tab;
        }

        private async void UpdateAllComponents()
        {
            LoadListView("");
            await experimentDetailer.RefreshContent();
        }

        private async void UpdateAllComponents(string file)
        {            
            LoadListView(file);
            await experimentDetailer.RefreshContent();          
        }

        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await TryQueryREMS(new DataTableQuery() { TableName = item });
        }

        private async void LoadListView(string file)
        {
            Text = "REMS 2020 - " +Path.GetFileName(file);

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
    }
}
