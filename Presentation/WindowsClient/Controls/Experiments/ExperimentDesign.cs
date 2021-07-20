using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public partial class ExperimentDesign : UserControl
    {
        private int experiment = 0;

        public ExperimentDesign()
        {
            InitializeComponent();

            designGrid.CellClick += OnCellClick;
        }        

        public async Task Populate(int id)
        {
            experiment = id;
            var query = new DesignsTableQuery { ExperimentId = experiment };
            designGrid.DataSource = await QueryManager.Request(query);            
        }

        private async void OnCellClick(object sender, EventArgs e)
        {
            var row = designGrid.CurrentCell.OwningRow;
            row.Selected = true;
            var query = new PlotsRepsQuery
            {
                ExperimentId = experiment,
                TreatmentName = row.Cells["Name"].Value.ToString()
            };

            plotsGrid.DataSource = await QueryManager.Request(query);
        }
    }
}
