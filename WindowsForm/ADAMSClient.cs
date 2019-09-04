using Database;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private ADAMSContext ADAMS = null;        

        public ADAMSClient()
        {
            InitializeComponent();
            PopulateListView();

            FormClosed += ADAMSClient_FormClosed;
        }

        private void ADAMSClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseDatabase();
            
        }        

        private void SelectedRelationChanged(object sender, EventArgs e)
        {
            if (ADAMS == null) return;

            var table = relationsListBox.SelectedItem.ToString();
            var property = typeof(ADAMSContext).GetProperty(table);
            dynamic set = property.GetValue(ADAMS);            

            var data = set.Local.ToBindingList();
            dataGridView.DataSource = data;
        }        

        /// <summary>
        /// Opens a connection to a database
        /// </summary>
        /// <param name="file"></param>
        private void OpenDatabase(string file)
        {
            string connection = $"Data Source={file};";

            ADAMS = new ADAMSContext(connection);
            ADAMS.Database.EnsureCreated();
            ADAMS.SaveChanges();

            dataGridView.DataSource = ADAMS.ChemicalApplication.Local.ToBindingList();
        }

        /// <summary>
        /// Saves the database
        /// </summary>
        private void SaveDatabase()
        {
            ADAMS.SaveChanges();
        }

        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        private void CloseDatabase()
        {
            SaveDatabase();
            ADAMS.Database.CloseConnection();
        }

        /// <summary>
        /// Populates the listview with the tables in the database
        /// </summary>
        private void PopulateListView()
        {
            foreach (string table in ADAMSContext.TableNames)
            {
                relationsListBox.Items.Add(table);
            }            
        }

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private void MenuNewClicked(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = true,
                Filter = "SQLite (*.db)|*.db",
                RestoreDirectory = true
            };

            if (save.ShowDialog() == DialogResult.OK)
            {
                if (ADAMS != null) CloseDatabase();

                OpenDatabase(save.FileName);
            }

            save.Dispose();
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private void MenuOpenClicked(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SQLite (*.db)|*.db"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {
                OpenDatabase(open.FileName);
            }

            open.Dispose();
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            SaveDatabase();
        }

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {            
            if (ADAMS == null)
            {
                const string message = "You must select a database before importing data.";
                const string caption = "No Database Selected";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);         

                return;
            }

            OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Excel (2007) (*.xlsx)|*.xlsx|Excel (2.0-2003) (*.xls)|*.xls|CSV (*.csv)|*.csv"
            };

            if (open.ShowDialog() == DialogResult.OK)
            {                
                var data = Excel.ReadRawData(open.FileName);
                ADAMS.ImportDataSet(data);
            }

            open.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ExampleApsimX.GenerateExampleA();           
        }
    }
}
