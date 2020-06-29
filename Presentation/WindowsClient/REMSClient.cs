using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Rems.Application;
using Rems.Infrastructure;
using Steema.TeeChart.Styles;

namespace WindowsClient
{
    public partial class REMSClient : Form
    {        
        private string _importFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Data");

        private readonly ClientLogic Logic;

        private DataTable graphTable = new DataTable();

        public REMSClient(IServiceProvider provider)
        {
            Logic = new ClientLogic(provider);

            InitializeComponent();
            InitializeControls();            

            FormClosed += REMSClientFormClosed;
            //tablesBox.Click += UpdatePageDisplay;
            notebook.SelectedIndexChanged += UpdatePageDisplay;
            Logic.ListViewOutdated += UpdateListView;

            EventManager.EntityNotFound += OnEntityNotFound;
        }

        private string OnEntityNotFound(object sender, EntityNotFoundArgs args)
        {
            var selector = new EntitySelector(args.Name, args.Options);
            selector.ShowDialog();
            return selector.Selection;
        }

        /// <summary>
        /// For initializing any control properties that cannot be done through the designer
        /// </summary>
        private void InitializeControls()
        {
            pageProperties.AutoScroll = true;
        }

        private void UpdateListView(object sender, EventArgs e)
        {
            relationsListBox.Items.Clear();
            relationsListBox.Items.AddRange(ProcessAction(Logic.GetListItems).Result);
        }

        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Save();
            ProcessAction(Logic.TryCloseDatabase);

            FormClosed -= REMSClientFormClosed;
            //tablesBox.Click -= UpdatePageDisplay;
            notebook.SelectedIndexChanged -= UpdatePageDisplay;
            Logic.ListViewOutdated -= UpdateListView;
        }

        private void UpdatePageDisplay(object sender, EventArgs e)
        {
            // TODO: Clean up this mess

            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;

            if (notebook.SelectedTab == pageData)
                dataGridView.DataSource = ProcessAction(Logic.TryGetDataTable, item).Result;
            else if (notebook.SelectedTab == pageProperties)
            {
                //if (sender is TextBox) 
                //    item = ((TextBox)sender).Text;
                //else
                    // Remove trailing 's'
                    item = item.Remove(item.Length - 1);

                pageProperties.Controls.Clear();
                pageProperties.Controls.AddRange(ProcessAction(Logic.GetProperties, item));
            }
            else if (notebook.SelectedTab == pageGraph)
            {

            }
        }

        #region Taskbar

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private void MenuNewClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.AddExtension = true;
                save.Filter = "SQLite (*.db)|*.db";
                save.RestoreDirectory = true;

                if (save.ShowDialog() == DialogResult.OK) ProcessAction(Logic.TryCreateDatabase, save.FileName);
            }
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private void MenuOpenClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "SQLite (*.db)|*.db";
                
                if (open.ShowDialog() == DialogResult.OK) ProcessAction(Logic.TryOpenDatabase, open.FileName);
            }                        
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            ProcessAction(Logic.TrySaveDatabase);
        }

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                open.InitialDirectory = _importFolder != "" ? _importFolder : _importFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                open.Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls";

                if (open.ShowDialog() == DialogResult.OK) ProcessAction(Logic.TryDataImport, open.FileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MenuExportClicked(object sender, EventArgs e)
        {
            using (var save = new SaveFileDialog())
            {
                save.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                save.Filter = "ApsimNG (*.apsimx)|*.apsimx";

                if (save.ShowDialog() == DialogResult.OK) ProcessAction(Logic.TryDataExport, save.FileName);
            }
                        
        }

        #endregion

        #region Graph

        private void GraphTableChanged(object sender, EventArgs e)
        {
            //var items = ProcessAction(Logic.TryGetDataTable, comboTable.SelectedItem.ToString());
            //graphTable = items.Result;

            //var ids = Logic.GetUniqueTraitIds(graphTable);
            var table = comboTable.SelectedItem.ToString();
            var names = ProcessAction(Logic.TryGetTraitNamesById, table.Remove(table.Length - 1));

            comboTrait.Items.Clear();
            comboTrait.Items.AddRange(names.Result);

            var items = ProcessAction(Logic.TryGetGraphableItems, table.Remove(table.Length - 1));

            comboXData.Items.Clear();
            comboXData.Items.AddRange(items.Result);

            comboYData.Items.Clear();
            comboYData.Items.AddRange(items.Result);
        }

        private void GraphTraitChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            var names = new string[4];
            names[0] = comboTable.SelectedItem?.ToString();
            names[1] = comboTrait.SelectedItem?.ToString();
            names[2] = comboXData.SelectedItem?.ToString();
            names[3] = comboYData.SelectedItem?.ToString();

            if (!names.Any(n => n == null))
            {
                names[0] = names[0].Remove(names[0].Length - 1);

                var data = ProcessAction(Logic.TryGetGraphData, names);
                data.Wait();

                var p = new Points();
                var l = new Line();
                foreach(var t in data.Result) CastAdd(p, t.Item1, t.Item2);
                foreach(var t in data.Result) CastAdd(l, t.Item1, t.Item2);

                graph.Series.Clear();
                graph.Series.Add(p);
                graph.Series.Add(l);
            }
        }

        private void CastAdd(CustomPoint point, object x, object y)
        {
            if (x is DateTime && y is DateTime) point.Add((DateTime)x, (DateTime)y);
            else if (x is double && y is DateTime) point.Add((double)x, (DateTime)y);
            else if (x is DateTime && y is double) point.Add((DateTime)x, (double)y);
            else point.Add(Convert.ToDouble(x), Convert.ToDouble(y));
        }

        #endregion

        private TResult ProcessAction<TResult>(Func<TResult> logic)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            var result = logic();
            Application.UseWaitCursor = false;

            return result;
        }

        private TResult ProcessAction<TValue, TResult>(Func<TValue, TResult> logic, TValue value)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            var result = logic(value);
            Application.UseWaitCursor = false;

            return result;
        }

        
    }
}
