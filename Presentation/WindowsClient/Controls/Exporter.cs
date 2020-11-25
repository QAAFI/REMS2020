using System;
using System.Windows.Forms;
using Rems.Infrastructure.ApsimX;
using Rems.Application.Common;
using Rems.Application.CQRS;
using WindowsClient.Forms;

namespace WindowsClient.Controls
{
    public partial class Exporter : UserControl
    {
        public QueryHandler Query;

        public string Folder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        

        public Exporter()
        {
            InitializeComponent();
        }

        public void Initialise()
        {
            
        }

        private async void OnExportClick(object sender, EventArgs e)
        {
            
        }
    }
}
