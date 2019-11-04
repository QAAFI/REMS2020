using REMS;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class REMSClient : Form
    {
        private readonly IREMSDatabase database = REMSDataFactory.Create();

        private readonly Settings settings = Settings.Instance;

        public REMSClient()
        {
            InitializeComponent();
            InitializeControls();
                        

            

            FormClosed += REMSClientFormClosed;
            tablesBox.Click += TablesBoxClicked;
        }                   

        private void InitializeControls()
        {
            pageProperties.AutoScroll = true;
        }

        private void LoadSettings()
        {
            settings.Load();

            // If the settings couldn't be loaded
            if (!settings.Loaded)
            {
                settings.TrackProperty("TABLES");
                foreach (var table in database.Tables)
                {
                    settings["TABLES"][table] = table;
                }

                foreach (var entity in database.Entities)
                {
                    var type = entity.GetType();
                    var name = type.Name + "s";
                    settings.TrackProperty(name);
                    foreach (var property in type.GetProperties())
                    {
                        settings[name][property.Name] = property.Name;
                    }
                }
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

        private void UpdateGridData()
        {
            try
            {
                var table = relationsListBox.SelectedItem.ToString();
                dataGridView.DataSource = database[table];
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }

        private void UpdateProperties(string table)
        {
            pageProperties.Controls.Clear();

            var c1 = new TextBox()
            {
                Text = "NAME:",
                Height = 20,
                Width = 150,
                Left = 5,
                Top = 0,
                TextAlign = HorizontalAlignment.Center,
            };

            var c2 = new TextBox()
            {
                Text = "MAPPED TO:",
                Height = 20,
                Left = 155,
                Top = 0,
                TextAlign = HorizontalAlignment.Center
            };

            pageProperties.Controls.Add(c1);
            pageProperties.Controls.Add(c2);

            int count = 1;
            foreach (var item in settings[table])
            {
                var name = new TextBox()
                {
                    Text = item.Key,
                    Height = 20, 
                    Width = 150,
                    Left = 5,
                    Top = 20 * count        
                };                

                var value = new TextBox()
                {
                    Text = item.Value,
                    Height = 20,
                    Left = 155,
                    Top = 20 * count                                 
                };
                
                pageProperties.Controls.Add(name);
                pageProperties.Controls.Add(value);

                count++;
            }            
        }

        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            settings.Save();
            database.Close();
        }

        private void ListBoxIndexChanged(object sender, EventArgs e)
        {
            if (notebook.SelectedTab.Name == "pageData")
                UpdateGridData();
            else if (notebook.SelectedTab.Name == "pageProperties")
                UpdateProperties(relationsListBox.SelectedItem.ToString());
        }

        private void TablesBoxClicked(object sender, EventArgs e)
        {
            var s = sender as TextBox;
            UpdateProperties(s.Text);
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
                        LoadSettings();
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
            using OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SQLite (*.db)|*.db"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (database.IsOpen) database.Close();

                    database.Open(open.FileName);
                    LoadSettings();
                    UpdateListView();
                }
                catch (Exception error)
                {
                    ErrorMessage(error.Message);
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

            using OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Excel (2007) (*.xlsx)|*.xlsx"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    database.ImportData(open.FileName);
                    UpdateListView();
                }
                catch (Exception error)
                {
                    ErrorMessage(error.Message);
                }
            }
        }

        private void MenuExportClicked(object sender, EventArgs e)
        {
            using SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "ApsimNG (*.apsimx)|*.apsimx"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    database.ExportData(save.FileName);
                }
                catch (Exception error)
                {
                    ErrorMessage(error.Message);
                }
            }
        }

        private void ErrorMessage(string message, string caption = "Oops! Something went wrong.")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void propertyTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
