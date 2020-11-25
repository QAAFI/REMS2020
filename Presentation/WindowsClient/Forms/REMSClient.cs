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
        private string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private readonly IMediator _mediator;

        public REMSClient(IServiceProvider provider)
        {
            InitializeComponent();

            _mediator = provider.GetRequiredService<IMediator>();

            experimentDetailer.REMS += _mediator.Send;

            importer.Query += _mediator.Send;
            importer.DatabaseChanged += UpdateAllComponents;
            importer.Initialise();
        }

        #region Taskbar

        private async void OnNewClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = folder;
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() != DialogResult.OK) return;

                folder = Path.GetDirectoryName(save.FileName);

                await TryQueryREMS(new CreateDBCommand() { FileName = save.FileName });
                await TryQueryREMS(new OpenDBCommand() { FileName = save.FileName });

                await LoadListView();
            }
        }

        private async void OnOpenClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = folder;
                open.Filter = "SQLite (*.db)|*.db";

                if (open.ShowDialog() != DialogResult.OK) return;

                folder = Path.GetDirectoryName(open.FileName);

                await TryQueryREMS(new OpenDBCommand() { FileName = open.FileName });

                UpdateAllComponents();
            }
        } 

        private async void UpdateAllComponents()
        {
            await LoadListView();
            await experimentDetailer.RefreshContent();          
        }
        #endregion       


        private async void OnRelationsIndexChanged(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;
            dataGridView.DataSource = await TryQueryREMS(new DataTableQuery() { TableName = item });
        }

        private async Task LoadListView()
        {
            var items = await new GetTableListQuery().Send(new QueryHandler(TryQueryREMS));

            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(items.ToArray());
        }

        #region Logic

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

        #endregion

        
    }
}
