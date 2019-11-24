using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Rems.Infrastructure;

namespace WindowsClient
{
    public partial class REMSClient : Form
    {        
        private string _importFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "Data");

        private readonly ClientLogic Logic;

        public REMSClient(IServiceProvider provider)
        {
            Logic = new ClientLogic(provider);

            InitializeComponent();
            InitializeControls();            

            FormClosed += REMSClientFormClosed;
            tablesBox.Click += UpdatePageDisplay;
            notebook.SelectedIndexChanged += UpdatePageDisplay;
            Logic.ListViewOutdated += UpdateListView;
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
            relationsListBox.Items.AddRange(ProcessUserAction(Logic.GetListItems).Result);
        }

        private void REMSClientFormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Instance.Save();
            ProcessUserAction(Logic.TryCloseDatabase);
        }

        private void UpdatePageDisplay(object sender, EventArgs e)
        {
            var item = (string)relationsListBox.SelectedItem;
            if (item == null) return;

            if (notebook.SelectedTab == pageData)
                dataGridView.DataSource = ProcessUserAction(Logic.TryGetGridData, item).Result;
            else if (notebook.SelectedTab == pageProperties)
            {
                if (sender is TextBox) 
                    item = ((TextBox)sender).Text;
                else
                    item = item.Remove(item.Length - 1);

                pageProperties.Controls.Clear();
                pageProperties.Controls.AddRange(ProcessUserAction(Logic.GetProperties, item));
            };
        }

        /// <summary>
        /// On click, prompt the user to create a new blank database
        /// </summary>
        private void MenuNewClicked(object sender, EventArgs e)
        {
            using var save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = true,
                Filter = "SQLite (*.db)|*.db",
                RestoreDirectory = true
            };            
            if (save.ShowDialog() == DialogResult.OK) ProcessUserAction(Logic.TryCreateDatabase, save.FileName);
        }

        /// <summary>
        /// On click, prompt the user to open an existing database
        /// </summary>
        private void MenuOpenClicked(object sender, EventArgs e)
        {
            using var open = new OpenFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "SQLite (*.db)|*.db"
            };
            if (open.ShowDialog() == DialogResult.OK) ProcessUserAction(Logic.TryOpenDatabase, open.FileName);            
        }

        /// <summary>
        /// On click, saves changes made to the database
        /// </summary>
        private void MenuSaveClicked(object sender, EventArgs e)
        {
            ProcessUserAction(Logic.TrySaveDatabase);
        }

        /// <summary>
        /// On click, imports data from the selected file
        /// </summary>
        private void MenuImportClicked(object sender, EventArgs e)
        {            
            using var open = new OpenFileDialog()
            {
                InitialDirectory = _importFolder != "" ? _importFolder : _importFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Excel Files (2007) (*.xlsx;*.xls)|*.xlsx;*.xls"
            };
            if (open.ShowDialog() == DialogResult.OK) ProcessUserAction(Logic.TryDataImport, open.FileName);            
        }

        /// <summary>
        /// 
        /// </summary>
        private void MenuExportClicked(object sender, EventArgs e)
        {
            using var save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "ApsimNG (*.apsimx)|*.apsimx"
            };
            if (save.ShowDialog() == DialogResult.OK) ProcessUserAction(Logic.TryDataExport, Path.GetDirectoryName(save.FileName));            
        }

        private TResult ProcessUserAction<TResult>(Func<TResult> logic)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            var result = logic();
            Application.UseWaitCursor = false;

            return result;
        }

        private TResult ProcessUserAction<Value, TResult>(Func<Value, TResult> logic, Value value)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();
            var result = logic(value);
            Application.UseWaitCursor = false;

            return result;
        }
    }
}
