using REMS;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class REMSClient : Form
    {
        private IREMSDatabase database = new REMSDatabase();        

        public REMSClient()
        {
            InitializeComponent();

            FormClosed += REMSClient_FormClosed;
        }

        private void REMSClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            database.Close();            
        }        

        private void SelectedRelationChanged(object sender, EventArgs e)
        {
            try
            {
                var table = relationsListBox.SelectedItem.ToString();
                var source = database[table];
                dataGridView.DataSource = source;
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }                

        /// <summary>
        /// Populates the listview with the tables in the database
        /// </summary>
        private void UpdateListView()
        {
            relationsListBox.Items.Clear();
            foreach (string table in database.Tables)
            {
                relationsListBox.Items.Add(table);
            }            
        }

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private void MenuNewClicked(object sender, EventArgs e)
        {
            using (SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = true,
                Filter = "SQLite (*.db)|*.db",
                RestoreDirectory = true
            })
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (database.IsOpen) database.Close();
                        database.Create(save.FileName);
                        database.Open(save.FileName);
                        UpdateListView();
                    }
                    catch (Exception error)
                    {
                        ErrorMessage(error.Message);
                    }
                }
            };
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private void MenuOpenClicked(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SQLite (*.db)|*.db"
            })
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (database.IsOpen) database.Close();
                        
                        database.Open(open.FileName);
                        UpdateListView();
                    }
                    catch (Exception error)
                    {                        
                        ErrorMessage(error.Message);
                    }
                }
            }            
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            try
            {
                database.Save();
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {            
            if (!database.IsOpen)
            {
                ErrorMessage("You must select a database before importing data.", "No database selected.");
                return;
            }

            using (OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Excel (2007) (*.xlsx)|*.xlsx"
            })
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        database.ImportData(open.FileName);
                    }
                    catch (Exception error)
                    {
                        ErrorMessage(error.Message);
                    }
                }
            }
        }

        private void ErrorMessage(string message, string caption = "Oops! Something went wrong.")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ExampleApsimX.GenerateExampleA();           
        }
    }
}
