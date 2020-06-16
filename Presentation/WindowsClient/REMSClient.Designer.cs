namespace WindowsClient
{
    partial class REMSClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(REMSClient));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageData = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.notebook = new System.Windows.Forms.TabControl();
            this.pageProperties = new System.Windows.Forms.TabPage();
            this.tablesBox = new System.Windows.Forms.TextBox();
            this.pageGraph = new System.Windows.Forms.TabPage();
            this.tChart = new Steema.TeeChart.TChart();
            this.comboItem = new System.Windows.Forms.ComboBox();
            this.labelItem = new System.Windows.Forms.Label();
            this.comboXData = new System.Windows.Forms.ComboBox();
            this.comboYData = new System.Windows.Forms.ComboBox();
            this.labelXData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.pageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
            this.pageGraph.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.menuRecent});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(112, 22);
            this.menuNew.Text = "New";
            this.menuNew.Click += new System.EventHandler(this.MenuNewClicked);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(112, 22);
            this.menuOpen.Text = "Open";
            this.menuOpen.Click += new System.EventHandler(this.MenuOpenClicked);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(112, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.MenuSaveClicked);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(112, 22);
            this.menuSaveAs.Text = "Save as";
            // 
            // menuRecent
            // 
            this.menuRecent.Name = "menuRecent";
            this.menuRecent.Size = new System.Drawing.Size(112, 22);
            this.menuRecent.Text = "Recent";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImport,
            this.menuExport});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // menuImport
            // 
            this.menuImport.Name = "menuImport";
            this.menuImport.Size = new System.Drawing.Size(110, 22);
            this.menuImport.Text = "Import";
            this.menuImport.Click += new System.EventHandler(this.MenuImportClicked);
            // 
            // menuExport
            // 
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(110, 22);
            this.menuExport.Text = "Export";
            this.menuExport.Click += new System.EventHandler(this.MenuExportClicked);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // pageData
            // 
            this.pageData.BackColor = System.Drawing.Color.Transparent;
            this.pageData.Controls.Add(this.dataGridView);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(3);
            this.pageData.Size = new System.Drawing.Size(632, 653);
            this.pageData.TabIndex = 0;
            this.pageData.Text = "Data";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(632, 653);
            this.dataGridView.TabIndex = 1;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.Location = new System.Drawing.Point(12, 52);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.Size = new System.Drawing.Size(143, 654);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePageDisplay);
            // 
            // notebook
            // 
            this.notebook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebook.Controls.Add(this.pageData);
            this.notebook.Controls.Add(this.pageProperties);
            this.notebook.Controls.Add(this.pageGraph);
            this.notebook.Location = new System.Drawing.Point(161, 27);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(640, 679);
            this.notebook.TabIndex = 2;
            // 
            // pageProperties
            // 
            this.pageProperties.Location = new System.Drawing.Point(4, 22);
            this.pageProperties.Name = "pageProperties";
            this.pageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.pageProperties.Size = new System.Drawing.Size(632, 653);
            this.pageProperties.TabIndex = 1;
            this.pageProperties.Text = "Properties";
            this.pageProperties.UseVisualStyleBackColor = true;
            // 
            // tablesBox
            // 
            this.tablesBox.Location = new System.Drawing.Point(12, 27);
            this.tablesBox.Name = "tablesBox";
            this.tablesBox.Size = new System.Drawing.Size(143, 20);
            this.tablesBox.TabIndex = 4;
            this.tablesBox.Text = "TABLES";
            this.tablesBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pageGraph
            // 
            this.pageGraph.BackColor = System.Drawing.Color.DarkGray;
            this.pageGraph.Controls.Add(this.label1);
            this.pageGraph.Controls.Add(this.labelXData);
            this.pageGraph.Controls.Add(this.comboYData);
            this.pageGraph.Controls.Add(this.comboXData);
            this.pageGraph.Controls.Add(this.labelItem);
            this.pageGraph.Controls.Add(this.comboItem);
            this.pageGraph.Controls.Add(this.tChart);
            this.pageGraph.Location = new System.Drawing.Point(4, 22);
            this.pageGraph.Name = "pageGraph";
            this.pageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.pageGraph.Size = new System.Drawing.Size(632, 653);
            this.pageGraph.TabIndex = 2;
            this.pageGraph.Text = "Graphs";
            // 
            // tChart
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Bottom.Labels.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Bottom.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Bottom.Labels.Font.Size = 9;
            this.tChart.Axes.Bottom.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Angle = 0;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Bottom.Title.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Bottom.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Bottom.Title.Font.Size = 11;
            this.tChart.Axes.Bottom.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Depth.Labels.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Depth.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Depth.Labels.Font.Size = 9;
            this.tChart.Axes.Depth.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Angle = 0;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Depth.Title.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Depth.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Depth.Title.Font.Size = 11;
            this.tChart.Axes.Depth.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.DepthTop.Labels.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.DepthTop.Labels.Font.Size = 9;
            this.tChart.Axes.DepthTop.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Angle = 0;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.DepthTop.Title.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.DepthTop.Title.Font.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.DepthTop.Title.Font.Size = 11;
            this.tChart.Axes.DepthTop.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Left.Labels.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Left.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Left.Labels.Font.Size = 9;
            this.tChart.Axes.Left.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Angle = 90;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Left.Title.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Left.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Left.Title.Font.Size = 11;
            this.tChart.Axes.Left.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Right.Labels.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Right.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Right.Labels.Font.Size = 9;
            this.tChart.Axes.Right.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Angle = 270;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Right.Title.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Right.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Right.Title.Font.Size = 11;
            this.tChart.Axes.Right.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Top.Labels.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Top.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Top.Labels.Font.Size = 9;
            this.tChart.Axes.Top.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Angle = 0;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Top.Title.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Top.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Top.Title.Font.Size = 11;
            this.tChart.Axes.Top.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Footer.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Footer.Brush.Solid = true;
            this.tChart.Footer.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Footer.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Footer.Font.Brush.Color = System.Drawing.Color.Red;
            this.tChart.Footer.Font.Brush.Solid = true;
            this.tChart.Footer.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Footer.Font.Shadow.Brush.Solid = true;
            this.tChart.Footer.Font.Shadow.Brush.Visible = true;
            this.tChart.Footer.Font.Size = 8;
            this.tChart.Footer.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Footer.ImageBevel.Brush.Solid = true;
            this.tChart.Footer.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Footer.Shadow.Brush.Solid = true;
            this.tChart.Footer.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Header.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tChart.Header.Brush.Solid = true;
            this.tChart.Header.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Header.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Header.Font.Brush.Solid = true;
            this.tChart.Header.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Header.Font.Shadow.Brush.Solid = true;
            this.tChart.Header.Font.Shadow.Brush.Visible = true;
            this.tChart.Header.Font.Size = 12;
            this.tChart.Header.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Header.ImageBevel.Brush.Solid = true;
            this.tChart.Header.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChart.Header.Shadow.Brush.Solid = true;
            this.tChart.Header.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Legend.Brush.Color = System.Drawing.Color.White;
            this.tChart.Legend.Brush.Solid = true;
            this.tChart.Legend.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Legend.Font.Brush.Solid = true;
            this.tChart.Legend.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Font.Shadow.Brush.Solid = true;
            this.tChart.Legend.Font.Shadow.Brush.Visible = true;
            this.tChart.Legend.Font.Size = 9;
            this.tChart.Legend.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Legend.ImageBevel.Brush.Solid = true;
            this.tChart.Legend.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart.Legend.Shadow.Brush.Solid = true;
            this.tChart.Legend.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Symbol.Shadow.Brush.Solid = true;
            this.tChart.Legend.Symbol.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Legend.Title.Brush.Color = System.Drawing.Color.White;
            this.tChart.Legend.Title.Brush.Solid = true;
            this.tChart.Legend.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black;
            this.tChart.Legend.Title.Font.Brush.Solid = true;
            this.tChart.Legend.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Legend.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Legend.Title.Font.Size = 8;
            this.tChart.Legend.Title.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Legend.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Legend.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Title.Shadow.Brush.Solid = true;
            this.tChart.Legend.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tChart.Panel.Brush.Solid = true;
            this.tChart.Panel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Panel.ImageBevel.Brush.Solid = true;
            this.tChart.Panel.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Panel.Shadow.Brush.Solid = true;
            this.tChart.Panel.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.SubFooter.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.SubFooter.Brush.Solid = true;
            this.tChart.SubFooter.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Brush.Color = System.Drawing.Color.Red;
            this.tChart.SubFooter.Font.Brush.Solid = true;
            this.tChart.SubFooter.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubFooter.Font.Shadow.Brush.Solid = true;
            this.tChart.SubFooter.Font.Shadow.Brush.Visible = true;
            this.tChart.SubFooter.Font.Size = 8;
            this.tChart.SubFooter.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.SubFooter.ImageBevel.Brush.Solid = true;
            this.tChart.SubFooter.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubFooter.Shadow.Brush.Solid = true;
            this.tChart.SubFooter.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tChart.SubHeader.Brush.Solid = true;
            this.tChart.SubHeader.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.SubHeader.Font.Brush.Solid = true;
            this.tChart.SubHeader.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubHeader.Font.Shadow.Brush.Solid = true;
            this.tChart.SubHeader.Font.Shadow.Brush.Visible = true;
            this.tChart.SubHeader.Font.Size = 12;
            this.tChart.SubHeader.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.SubHeader.ImageBevel.Brush.Solid = true;
            this.tChart.SubHeader.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChart.SubHeader.Shadow.Brush.Solid = true;
            this.tChart.SubHeader.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Walls.Back.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Walls.Back.Brush.Solid = true;
            this.tChart.Walls.Back.Brush.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Back.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Back.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Back.Shadow.Brush.Solid = true;
            this.tChart.Walls.Back.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Walls.Bottom.Brush.Color = System.Drawing.Color.White;
            this.tChart.Walls.Bottom.Brush.Solid = true;
            this.tChart.Walls.Bottom.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Bottom.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Bottom.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Bottom.Shadow.Brush.Solid = true;
            this.tChart.Walls.Bottom.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow;
            this.tChart.Walls.Left.Brush.Solid = true;
            this.tChart.Walls.Left.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Left.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Left.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Left.Shadow.Brush.Solid = true;
            this.tChart.Walls.Left.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.tChart.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow;
            this.tChart.Walls.Right.Brush.Solid = true;
            this.tChart.Walls.Right.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Right.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Right.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Right.Shadow.Brush.Solid = true;
            this.tChart.Walls.Right.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Zoom.Brush.Color = System.Drawing.Color.LightBlue;
            this.tChart.Zoom.Brush.Solid = true;
            this.tChart.Zoom.Brush.Visible = true;
            // 
            // comboItem
            // 
            this.comboItem.FormattingEnabled = true;
            this.comboItem.Location = new System.Drawing.Point(42, 6);
            this.comboItem.Name = "comboItem";
            this.comboItem.Size = new System.Drawing.Size(138, 21);
            this.comboItem.TabIndex = 3;
            // 
            // labelItem
            // 
            this.labelItem.AutoSize = true;
            this.labelItem.Location = new System.Drawing.Point(6, 9);
            this.labelItem.Name = "labelItem";
            this.labelItem.Size = new System.Drawing.Size(30, 13);
            this.labelItem.TabIndex = 4;
            this.labelItem.Text = "Item:";
            // 
            // comboXData
            // 
            this.comboXData.FormattingEnabled = true;
            this.comboXData.Location = new System.Drawing.Point(235, 6);
            this.comboXData.Name = "comboXData";
            this.comboXData.Size = new System.Drawing.Size(138, 21);
            this.comboXData.TabIndex = 5;
            // 
            // comboYData
            // 
            this.comboYData.FormattingEnabled = true;
            this.comboYData.Location = new System.Drawing.Point(428, 6);
            this.comboYData.Name = "comboYData";
            this.comboYData.Size = new System.Drawing.Size(138, 21);
            this.comboYData.TabIndex = 6;
            // 
            // labelXData
            // 
            this.labelXData.AutoSize = true;
            this.labelXData.Location = new System.Drawing.Point(186, 9);
            this.labelXData.Name = "labelXData";
            this.labelXData.Size = new System.Drawing.Size(43, 13);
            this.labelXData.TabIndex = 7;
            this.labelXData.Text = "X Data:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(379, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Y Data:";
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 721);
            this.Controls.Add(this.tablesBox);
            this.Controls.Add(this.relationsListBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "REMSClient";
            this.Text = "REMS 2020";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pageData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
            this.pageGraph.ResumeLayout(false);
            this.pageGraph.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuImport;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuRecent;
        private System.Windows.Forms.TabPage pageData;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage pageProperties;
        private System.Windows.Forms.TextBox tablesBox;
        private System.Windows.Forms.TabPage pageGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelXData;
        private System.Windows.Forms.ComboBox comboYData;
        private System.Windows.Forms.ComboBox comboXData;
        private System.Windows.Forms.Label labelItem;
        private System.Windows.Forms.ComboBox comboItem;
        private Steema.TeeChart.TChart tChart;
    }
}

