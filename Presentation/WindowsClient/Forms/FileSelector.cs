using LamarCodeGeneration.Frames;
using Rems.Infrastructure.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Forms
{
    public partial class FileSelector : Form
    {
        public DataSet InfoTables { get; private set; }
        public DataSet ExpsTables { get; private set; }
        public DataSet DataTables { get; private set; }

        public FileSelector()
        {
            InitializeComponent();            
        }

        private void InfoClicked(object sender, EventArgs e) => SelectFile(infoBox);
        private void ExpsClicked(object sender, EventArgs e) => SelectFile(expsBox);
        private void DataClicked(object sender, EventArgs e) => SelectFile(dataBox);

        private void SelectFile(TextBox box)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    box.Text = open.FileName;
                }
            }             
        }

        private void ImportClicked(object sender, EventArgs e)
        {
            InfoTables = AccessData(infoBox);
            ExpsTables = AccessData(expsBox);
            DataTables = AccessData(dataBox);

            if (InfoTables is null || ExpsTables is null || DataTables is null)
            {
                MessageBox.Show("Could not import selected files. Try again.");
                return;
            }
            
            DialogResult = DialogResult.OK;
            Close(); 
        }

        private DataSet AccessData(TextBox box)
        {
            try
            {
                //return ExcelImporter.ReadDataSet(box.Text);
                return null;
            }
            catch
            {
                MessageBox.Show($"Could not read \"{box.Text}\"");
                return null;
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Close();
        }        
    }
}
