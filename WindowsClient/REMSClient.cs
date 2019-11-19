using REMS;
using Services;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MediatR;
using Rems.Application.Tables.Queries.GetTableList;
using Rems.Application.Common.Mappings;
using Rems.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Rems.Application.DB.Commands.CreateDB;
using Rems.Application.DB.Commands.OpenDB;
using Rems.Application.DB.Queries.GetDataTable;

namespace WindowsClient
{
    public partial class REMSClient : Form
    {
        private IREMSDatabase database = REMSDataFactory.Create();
        private string _importFolder = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase), "Data");

        private readonly Settings settings = Settings.Instance;
        private IMediator _mediator;
        private readonly IServiceProvider _provider;
        protected IMediator Mediator => _mediator ??= _provider.GetRequiredService<IMediator>();
        public REMSClient(IServiceProvider provider)
        {
            _provider = provider;
            InitializeComponent();
            InitializeControls();                       

            FormClosed += REMSClientFormClosed;
            tablesBox.Click += TablesBoxClicked;
            notebook.SelectedIndexChanged += TabChanged;
        }
        
        private void TabChanged(object sender, EventArgs e)
        {
            if (relationsListBox.SelectedItem == null) return;

            if (notebook.SelectedTab == pageData)
                UpdateGridData();
            else if (notebook.SelectedTab == pageProperties)
                UpdateProperties(relationsListBox.SelectedItem.ToString());
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
                // Track the tables
                var tables = new PropertyMap("TABLES");
                foreach (var table in database.Tables)
                {
                    tables.AddMapping(table);
                }
                settings.TrackProperty(tables);

                // Track the traits
                var traits = new PropertyMap("TRAITS");

                // TEMPORARY
                traits.AddMapping("AirDry", "air_dry");
                traits.AddMapping("CN2Bare", "cn2_bare");
                traits.AddMapping("CNCov", "cn_cov");
                traits.AddMapping("CNRed", "cn_red");
                traits.AddMapping("SummerCona", "cona");
                traits.AddMapping("DiffusConst", "diffus_const");
                traits.AddMapping("DiffusSlope", "diffus_slope");
                traits.AddMapping("DUL", "dul");
                //traits.AddMapping("", "enr_a_coeff");
                //traits.AddMapping("", "enr_b_coeff");
                traits.AddMapping("FBiom", "fbiom");
                traits.AddMapping("FInert", "finert");
                traits.AddMapping("KL", "kl");
                traits.AddMapping("LL", "ll");
                traits.AddMapping("LL15", "ll15");
                traits.AddMapping("MaxT", "maxt");
                traits.AddMapping("MinT", "mint");
                //traits.AddMapping("", "nh4ppm");
                //traits.AddMapping("", "no3ppm");
                traits.AddMapping("OC", "oc");
                traits.AddMapping("Radn", "radn");
                //traits.AddMapping("", "root_cn");
                //traits.AddMapping("", "root_wt");
                traits.AddMapping("Salb", "salb");
                traits.AddMapping("SAT", "sat");
                traits.AddMapping("SoilCNRatio", "soil_cn");
                traits.AddMapping("SW", "sw");
                traits.AddMapping("SWCON", "swcon");
                //traits.AddMapping("", "u");
                //traits.AddMapping("", "ureappm");
                traits.AddMapping("XF", "xf");

                // Track the entities
                foreach (var entity in database.Entities)
                {
                    var map = new PropertyMap(entity);
                    settings.TrackProperty(map);
                }
            }
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs args)
        {
            if (settings[args.TableName][args.PropertyName] == args.NewValue) return;

            try
            {
                settings[args.TableName][args.PropertyName] = args.NewValue;
            }
            catch
            {
                ErrorMessage("That value is currently mapped to another property.", "Invalid name.");
                ((PropertyControl)sender).Value = settings[args.TableName][args.PropertyName];
            }
        }

        /// <summary>
        /// Populates the listview with the tables in the database
        /// </summary>
        private async void UpdateListView()
        {
            var tables = await Mediator.Send(new GetTableListQuery());

            relationsListBox.Items.Clear();
            foreach (string table in tables)
            {
                relationsListBox.Items.Add(table);
            }            
        }

        private async void UpdateGridData()
        {
            try
            {
                var tableName = relationsListBox.SelectedItem.ToString();
                dataGridView.DataSource = await Mediator.Send(new GetDataTableQuery() { TableName = tableName });
            }
            catch (Exception error)
            {
                ErrorMessage(error.Message);
            }
        }

        private void UpdateProperties(string table)
        {
            pageProperties.Controls.Clear();

            int count = 0;
            foreach (var item in settings[table].Reverse())
            {
                var property = new PropertyControl()
                {
                    Table = table,
                    Property = item.Key,
                    Value = item.Value,
                    Dock = DockStyle.Top
                };

                property.PropertyChanged += PropertyChangedEvent;
                pageProperties.Controls.Add(property);

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
        private async void MenuNewClicked(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = true,
                Filter = "SQLite (*.db)|*.db",
                RestoreDirectory = true
            };
            {
                if (save.ShowDialog() == DialogResult.OK)
                {
                    Application.UseWaitCursor = true;
                    Application.DoEvents();
                    try
                    {
                        await Mediator.Send(new CreateDBCommand() { FileName = save.FileName });

                        LoadSettings();
                        UpdateListView();
                        Application.UseWaitCursor = false;
                    }
                    catch (Exception error)
                    {
                        Application.UseWaitCursor = false;
                        ErrorMessage(error.Message);
                    }
                }
            };
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private async void MenuOpenClicked(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SQLite (*.db)|*.db"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Application.UseWaitCursor = true;
                    Application.DoEvents();

                    await Mediator.Send(new OpenDBCommand() { FileName = open.FileName });

                    if (database.IsOpen) database.Close();
                    database.Open(open.FileName);

                    LoadSettings();
                    UpdateListView();
                    Application.UseWaitCursor = false;
                }
                catch (Exception error)
                {
                    Application.UseWaitCursor = false;
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
                settings.Save();
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

            OpenFileDialog open = new OpenFileDialog()
            {
                InitialDirectory = _importFolder != "" ? _importFolder : _importFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls"
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                Application.UseWaitCursor = true;
                Application.DoEvents();
                try
                {                  
                    var watch1 = new System.Diagnostics.Stopwatch();
                    watch1.Start();
                    database.ImportExcelDataFast(open.FileName);
                    watch1.Stop();

                    //var watch2 = new System.Diagnostics.Stopwatch();
                    //watch2.Start();
                    //testdatabase.ImportExcelDataSlow(open.FileName);
                    //watch2.Stop();

                    UpdateListView();
                    Application.UseWaitCursor = false;
                    MessageBox.Show($"Import Complete.\n");
                    //MessageBox.Show($"Import Complete.\nTime elapsed fast: {watch1.ElapsedMilliseconds} ms\nTime elapsed slow: {watch2.ElapsedMilliseconds} ms");
                }
                catch (Exception error)
                {
                    ErrorMessage(error.Message);
                    Application.UseWaitCursor = false;
                }
            }
        }

        private void MenuExportClicked(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "ApsimNG (*.apsimx)|*.apsimx"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                var path = Path.GetDirectoryName(save.FileName);
                Application.UseWaitCursor = true;
                Application.DoEvents();

                try
                {
                    var sims = database.CreateApsimFile(path);
                    sims.SaveApsimFile(save.FileName);

                    Application.UseWaitCursor = false;
                    MessageBox.Show($"Export Complete.");
                }
                catch (Exception error)
                {
                    ErrorMessage(error.Message);
                    Application.UseWaitCursor = false;
                }
            }
        }

        private void ErrorMessage(string message, string caption = "Oops! Something went wrong.")
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
