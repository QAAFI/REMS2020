using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            string path = Environment.GetFolderPath(local) + "\\ADAMS\\ADAMS.db";

            string connection = $"Data Source={path};";

            ADAMS = new ADAMSContext(connection);
            ADAMS.Database.EnsureCreated();         
        }

        private void OtherMethods()
        {
            string xlspath = "C:\\Users\\uqgmclea\\Documents\\Mike\\RemsData.xls";
            var data = Excel.ReadRawData(xlspath);

            DbSet<dynamic> table = ADAMS.GetTable("Experiments");

            table.Add(new Experiment());
            
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
