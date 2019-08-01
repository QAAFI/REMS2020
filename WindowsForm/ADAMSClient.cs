using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class ADAMSClient : Form
    {
        private ADAMSContext ADAMS;        

        public ADAMSClient()
        {
            InitializeComponent();
            LoadDatabase();
            FillListView();
            OtherMethods();
            this.FormClosed += ADAMSClient_FormClosed;
        }

        private void ADAMSClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            ADAMS.Database.CloseConnection();
        }

        private void LoadDatabase()
        {
            var local = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(local) + "\\ADAMS";
            Directory.CreateDirectory(path);

            string connection = $"Data Source={path}\\ADAMS.db;";

            var exp = new Experiment()
            {
                ExperimentId = 13,
                ExperimentName = "Name",
                Description = "Testing",
                CropId = 2,
                FieldId = 3,
                BeginDate = new DateTime(2019, 7, 30),
                EndDate = new DateTime(2019, 11, 3),
                MetStationId = 4,
                ExperimentDesign = "Good design",
                Repetitions = 5,
                Rating = 6,
                Notes = "Nothing",
                MethodId = 7,
                PlantingNotes = "Nothing"
            };

            ADAMS = new ADAMSContext(connection);
            ADAMS.Database.EnsureCreated();
            ADAMS.Experiments.Add(new Experiment());
            ADAMS.SaveChanges();
        }

        private void OtherMethods()
        {
            string xlspath = "C:\\Users\\uqmstow1\\Documents\\RemsData.xls";
            //var data = Excel.ReadRawData(xlspath);
            //ADAMS.GetTable("Experiments");
            //ADAMS.ImportDataSet(data);            
        }

        private void FillListView()
        {
            foreach(string table in ADAMSContext.TableNames)
            {
                relationsListBox.Items.Add(table);
            }

            dataGridView.DataSource = ADAMS.ChemicalApplication.Local.ToBindingList();
        }

        private void RelationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var table = relationsListBox.SelectedItem.ToString();
            var property = typeof(ADAMSContext).GetProperty(table);
            dynamic set = property.GetValue(ADAMS);            

            dataGridView.DataSource = set.Local.ToBindingList();

        }

    }
}
