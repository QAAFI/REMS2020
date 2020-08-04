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
            this.pageExps = new System.Windows.Forms.TabPage();
            this.experimentsTab = new System.Windows.Forms.TabControl();
            this.pageSoil = new System.Windows.Forms.TabPage();
            this.pageCrop = new System.Windows.Forms.TabPage();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.pageDesign = new System.Windows.Forms.TabPage();
            this.designData = new System.Windows.Forms.DataGridView();
            this.pageSummary = new System.Windows.Forms.TabPage();
            this.experimentsTree = new System.Windows.Forms.TreeView();
            this.pageGraph = new System.Windows.Forms.TabPage();
            this.graph = new Steema.TeeChart.TChart();
            this.comboTable = new System.Windows.Forms.ComboBox();
            this.labelTable = new System.Windows.Forms.Label();
            this.comboXData = new System.Windows.Forms.ComboBox();
            this.comboYData = new System.Windows.Forms.ComboBox();
            this.labelXData = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboTrait = new System.Windows.Forms.ComboBox();
            this.labelTrait = new System.Windows.Forms.Label();
            this.pageProperties = new System.Windows.Forms.TabPage();
            this.pageInfo = new System.Windows.Forms.TabPage();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.notebook = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.pageExps.SuspendLayout();
            this.experimentsTab.SuspendLayout();
            this.pageDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).BeginInit();
            this.pageGraph.SuspendLayout();
            this.pageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
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
            // pageExps
            // 
            this.pageExps.Controls.Add(this.experimentsTree);
            this.pageExps.Controls.Add(this.experimentsTab);
            this.pageExps.Location = new System.Drawing.Point(4, 22);
            this.pageExps.Name = "pageExps";
            this.pageExps.Padding = new System.Windows.Forms.Padding(3);
            this.pageExps.Size = new System.Drawing.Size(805, 667);
            this.pageExps.TabIndex = 4;
            this.pageExps.Text = "Experiments";
            this.pageExps.UseVisualStyleBackColor = true;
            // 
            // experimentsTab
            // 
            this.experimentsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.experimentsTab.Controls.Add(this.pageSummary);
            this.experimentsTab.Controls.Add(this.pageDesign);
            this.experimentsTab.Controls.Add(this.pageOperation);
            this.experimentsTab.Controls.Add(this.pageCrop);
            this.experimentsTab.Controls.Add(this.pageSoil);
            this.experimentsTab.Location = new System.Drawing.Point(156, 3);
            this.experimentsTab.Name = "experimentsTab";
            this.experimentsTab.SelectedIndex = 0;
            this.experimentsTab.Size = new System.Drawing.Size(646, 658);
            this.experimentsTab.TabIndex = 1;
            // 
            // pageSoil
            // 
            this.pageSoil.Location = new System.Drawing.Point(4, 22);
            this.pageSoil.Name = "pageSoil";
            this.pageSoil.Padding = new System.Windows.Forms.Padding(3);
            this.pageSoil.Size = new System.Drawing.Size(638, 632);
            this.pageSoil.TabIndex = 6;
            this.pageSoil.Text = "Soil";
            this.pageSoil.UseVisualStyleBackColor = true;
            // 
            // pageCrop
            // 
            this.pageCrop.Location = new System.Drawing.Point(4, 22);
            this.pageCrop.Name = "pageCrop";
            this.pageCrop.Padding = new System.Windows.Forms.Padding(3);
            this.pageCrop.Size = new System.Drawing.Size(638, 632);
            this.pageCrop.TabIndex = 5;
            this.pageCrop.Text = "Crop";
            this.pageCrop.UseVisualStyleBackColor = true;
            // 
            // pageOperation
            // 
            this.pageOperation.Location = new System.Drawing.Point(4, 22);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.pageOperation.Size = new System.Drawing.Size(638, 632);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            this.pageOperation.UseVisualStyleBackColor = true;
            // 
            // pageDesign
            // 
            this.pageDesign.Controls.Add(this.designData);
            this.pageDesign.Location = new System.Drawing.Point(4, 22);
            this.pageDesign.Name = "pageDesign";
            this.pageDesign.Padding = new System.Windows.Forms.Padding(3);
            this.pageDesign.Size = new System.Drawing.Size(638, 632);
            this.pageDesign.TabIndex = 2;
            this.pageDesign.Text = "Design";
            this.pageDesign.UseVisualStyleBackColor = true;
            // 
            // designData
            // 
            this.designData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designData.Location = new System.Drawing.Point(0, 0);
            this.designData.Name = "designData";
            this.designData.Size = new System.Drawing.Size(638, 631);
            this.designData.TabIndex = 0;
            // 
            // pageSummary
            // 
            this.pageSummary.Location = new System.Drawing.Point(4, 22);
            this.pageSummary.Name = "pageSummary";
            this.pageSummary.Padding = new System.Windows.Forms.Padding(3);
            this.pageSummary.Size = new System.Drawing.Size(638, 632);
            this.pageSummary.TabIndex = 7;
            this.pageSummary.Text = "Summary";
            this.pageSummary.UseVisualStyleBackColor = true;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.experimentsTree.Location = new System.Drawing.Point(6, 6);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(144, 654);
            this.experimentsTree.TabIndex = 2;
            // 
            // pageGraph
            // 
            this.pageGraph.BackColor = System.Drawing.Color.DarkGray;
            this.pageGraph.Controls.Add(this.labelTrait);
            this.pageGraph.Controls.Add(this.comboTrait);
            this.pageGraph.Controls.Add(this.label1);
            this.pageGraph.Controls.Add(this.labelXData);
            this.pageGraph.Controls.Add(this.comboYData);
            this.pageGraph.Controls.Add(this.comboXData);
            this.pageGraph.Controls.Add(this.labelTable);
            this.pageGraph.Controls.Add(this.comboTable);
            this.pageGraph.Controls.Add(this.graph);
            this.pageGraph.Location = new System.Drawing.Point(4, 22);
            this.pageGraph.Name = "pageGraph";
            this.pageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.pageGraph.Size = new System.Drawing.Size(805, 667);
            this.pageGraph.TabIndex = 2;
            this.pageGraph.Text = "Graphs";
            // 
            // graph
            // 
            this.graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.graph.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.Bottom.Labels.Brush.Solid = true;
            this.graph.Axes.Bottom.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.Bottom.Labels.Font.Brush.Solid = true;
            this.graph.Axes.Bottom.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Bottom.Labels.Font.Size = 9;
            this.graph.Axes.Bottom.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Bottom.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Bottom.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Bottom.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.Bottom.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Angle = 0;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.Bottom.Title.Brush.Solid = true;
            this.graph.Axes.Bottom.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.Bottom.Title.Font.Brush.Solid = true;
            this.graph.Axes.Bottom.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Bottom.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Bottom.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Bottom.Title.Font.Size = 11;
            this.graph.Axes.Bottom.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Bottom.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Bottom.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Bottom.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.Bottom.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.Depth.Labels.Brush.Solid = true;
            this.graph.Axes.Depth.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.Depth.Labels.Font.Brush.Solid = true;
            this.graph.Axes.Depth.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Depth.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Depth.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Depth.Labels.Font.Size = 9;
            this.graph.Axes.Depth.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Depth.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Depth.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Depth.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.Depth.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Angle = 0;
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.Depth.Title.Brush.Solid = true;
            this.graph.Axes.Depth.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.Depth.Title.Font.Brush.Solid = true;
            this.graph.Axes.Depth.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Depth.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Depth.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Depth.Title.Font.Size = 11;
            this.graph.Axes.Depth.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Depth.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Depth.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Depth.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.Depth.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.DepthTop.Labels.Brush.Solid = true;
            this.graph.Axes.DepthTop.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.DepthTop.Labels.Font.Brush.Solid = true;
            this.graph.Axes.DepthTop.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.DepthTop.Labels.Font.Size = 9;
            this.graph.Axes.DepthTop.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.DepthTop.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.DepthTop.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Angle = 0;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.DepthTop.Title.Brush.Solid = true;
            this.graph.Axes.DepthTop.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.DepthTop.Title.Font.Brush.Solid = true;
            this.graph.Axes.DepthTop.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.DepthTop.Title.Font.Size = 11;
            this.graph.Axes.DepthTop.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.DepthTop.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.DepthTop.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.DepthTop.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.DepthTop.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.Left.Labels.Brush.Solid = true;
            this.graph.Axes.Left.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.Left.Labels.Font.Brush.Solid = true;
            this.graph.Axes.Left.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Left.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Left.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Left.Labels.Font.Size = 9;
            this.graph.Axes.Left.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Left.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Left.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Left.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.Left.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Angle = 90;
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.Left.Title.Brush.Solid = true;
            this.graph.Axes.Left.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.Left.Title.Font.Brush.Solid = true;
            this.graph.Axes.Left.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Left.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Left.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Left.Title.Font.Size = 11;
            this.graph.Axes.Left.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Left.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Left.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Left.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.Left.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.Right.Labels.Brush.Solid = true;
            this.graph.Axes.Right.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.Right.Labels.Font.Brush.Solid = true;
            this.graph.Axes.Right.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Right.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Right.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Right.Labels.Font.Size = 9;
            this.graph.Axes.Right.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Right.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Right.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Right.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.Right.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Angle = 270;
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.Right.Title.Brush.Solid = true;
            this.graph.Axes.Right.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.Right.Title.Font.Brush.Solid = true;
            this.graph.Axes.Right.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Right.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Right.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Right.Title.Font.Size = 11;
            this.graph.Axes.Right.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Right.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Right.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Right.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.Right.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White;
            this.graph.Axes.Top.Labels.Brush.Solid = true;
            this.graph.Axes.Top.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Axes.Top.Labels.Font.Brush.Solid = true;
            this.graph.Axes.Top.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Top.Labels.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Top.Labels.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Top.Labels.Font.Size = 9;
            this.graph.Axes.Top.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Top.Labels.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Top.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Top.Labels.Shadow.Brush.Solid = true;
            this.graph.Axes.Top.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Angle = 0;
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Axes.Top.Title.Brush.Solid = true;
            this.graph.Axes.Top.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Axes.Top.Title.Font.Brush.Solid = true;
            this.graph.Axes.Top.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Top.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Axes.Top.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Axes.Top.Title.Font.Size = 11;
            this.graph.Axes.Top.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Axes.Top.Title.ImageBevel.Brush.Solid = true;
            this.graph.Axes.Top.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Axes.Top.Title.Shadow.Brush.Solid = true;
            this.graph.Axes.Top.Title.Shadow.Brush.Visible = true;
            this.graph.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Footer.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Footer.Brush.Solid = true;
            this.graph.Footer.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Footer.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Footer.Font.Brush.Color = System.Drawing.Color.Red;
            this.graph.Footer.Font.Brush.Solid = true;
            this.graph.Footer.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Footer.Font.Shadow.Brush.Solid = true;
            this.graph.Footer.Font.Shadow.Brush.Visible = true;
            this.graph.Footer.Font.Size = 8;
            this.graph.Footer.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Footer.ImageBevel.Brush.Solid = true;
            this.graph.Footer.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Footer.Shadow.Brush.Solid = true;
            this.graph.Footer.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Header.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.graph.Header.Brush.Solid = true;
            this.graph.Header.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Header.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.Header.Font.Brush.Solid = true;
            this.graph.Header.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Header.Font.Shadow.Brush.Solid = true;
            this.graph.Header.Font.Shadow.Brush.Visible = true;
            this.graph.Header.Font.Size = 12;
            this.graph.Header.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Header.ImageBevel.Brush.Solid = true;
            this.graph.Header.ImageBevel.Brush.Visible = true;
            this.graph.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.graph.Header.Shadow.Brush.Solid = true;
            this.graph.Header.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Legend.Brush.Color = System.Drawing.Color.White;
            this.graph.Legend.Brush.Solid = true;
            this.graph.Legend.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Legend.Font.Bold = false;
            // 
            // 
            // 
            this.graph.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.graph.Legend.Font.Brush.Solid = true;
            this.graph.Legend.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Legend.Font.Shadow.Brush.Solid = true;
            this.graph.Legend.Font.Shadow.Brush.Visible = true;
            this.graph.Legend.Font.Size = 9;
            this.graph.Legend.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Legend.ImageBevel.Brush.Solid = true;
            this.graph.Legend.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.graph.Legend.Shadow.Brush.Solid = true;
            this.graph.Legend.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Legend.Symbol.Shadow.Brush.Solid = true;
            this.graph.Legend.Symbol.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Legend.Title.Brush.Color = System.Drawing.Color.White;
            this.graph.Legend.Title.Brush.Solid = true;
            this.graph.Legend.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.graph.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black;
            this.graph.Legend.Title.Font.Brush.Solid = true;
            this.graph.Legend.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Legend.Title.Font.Shadow.Brush.Solid = true;
            this.graph.Legend.Title.Font.Shadow.Brush.Visible = true;
            this.graph.Legend.Title.Font.Size = 8;
            this.graph.Legend.Title.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Legend.Title.ImageBevel.Brush.Solid = true;
            this.graph.Legend.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Legend.Title.Shadow.Brush.Solid = true;
            this.graph.Legend.Title.Shadow.Brush.Visible = true;
            this.graph.Location = new System.Drawing.Point(9, 33);
            this.graph.Name = "graph";
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.graph.Panel.Brush.Solid = true;
            this.graph.Panel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Panel.ImageBevel.Brush.Solid = true;
            this.graph.Panel.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Panel.Shadow.Brush.Solid = true;
            this.graph.Panel.Shadow.Brush.Visible = true;
            this.graph.Size = new System.Drawing.Size(788, 628);
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.SubFooter.Brush.Color = System.Drawing.Color.Silver;
            this.graph.SubFooter.Brush.Solid = true;
            this.graph.SubFooter.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.SubFooter.Font.Bold = false;
            // 
            // 
            // 
            this.graph.SubFooter.Font.Brush.Color = System.Drawing.Color.Red;
            this.graph.SubFooter.Font.Brush.Solid = true;
            this.graph.SubFooter.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.SubFooter.Font.Shadow.Brush.Solid = true;
            this.graph.SubFooter.Font.Shadow.Brush.Visible = true;
            this.graph.SubFooter.Font.Size = 8;
            this.graph.SubFooter.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.SubFooter.ImageBevel.Brush.Solid = true;
            this.graph.SubFooter.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.SubFooter.Shadow.Brush.Solid = true;
            this.graph.SubFooter.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.graph.SubHeader.Brush.Solid = true;
            this.graph.SubHeader.Brush.Visible = true;
            // 
            // 
            // 
            this.graph.SubHeader.Font.Bold = false;
            // 
            // 
            // 
            this.graph.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.graph.SubHeader.Font.Brush.Solid = true;
            this.graph.SubHeader.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.SubHeader.Font.Shadow.Brush.Solid = true;
            this.graph.SubHeader.Font.Shadow.Brush.Visible = true;
            this.graph.SubHeader.Font.Size = 12;
            this.graph.SubHeader.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.SubHeader.ImageBevel.Brush.Solid = true;
            this.graph.SubHeader.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.graph.SubHeader.Shadow.Brush.Solid = true;
            this.graph.SubHeader.Shadow.Brush.Visible = true;
            this.graph.TabIndex = 9;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Walls.Back.Brush.Color = System.Drawing.Color.Silver;
            this.graph.Walls.Back.Brush.Solid = true;
            this.graph.Walls.Back.Brush.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Walls.Back.ImageBevel.Brush.Solid = true;
            this.graph.Walls.Back.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Walls.Back.Shadow.Brush.Solid = true;
            this.graph.Walls.Back.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Walls.Bottom.Brush.Color = System.Drawing.Color.White;
            this.graph.Walls.Bottom.Brush.Solid = true;
            this.graph.Walls.Bottom.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Walls.Bottom.ImageBevel.Brush.Solid = true;
            this.graph.Walls.Bottom.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Walls.Bottom.Shadow.Brush.Solid = true;
            this.graph.Walls.Bottom.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow;
            this.graph.Walls.Left.Brush.Solid = true;
            this.graph.Walls.Left.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Walls.Left.ImageBevel.Brush.Solid = true;
            this.graph.Walls.Left.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Walls.Left.Shadow.Brush.Solid = true;
            this.graph.Walls.Left.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.graph.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow;
            this.graph.Walls.Right.Brush.Solid = true;
            this.graph.Walls.Right.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.graph.Walls.Right.ImageBevel.Brush.Solid = true;
            this.graph.Walls.Right.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.graph.Walls.Right.Shadow.Brush.Solid = true;
            this.graph.Walls.Right.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.graph.Zoom.Brush.Color = System.Drawing.Color.LightBlue;
            this.graph.Zoom.Brush.Solid = true;
            this.graph.Zoom.Brush.Visible = true;
            // 
            // comboTable
            // 
            this.comboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTable.FormattingEnabled = true;
            this.comboTable.Items.AddRange(new object[] {
            "MetData",
            "PlotData",
            "SoilData",
            "SoilLayerData"});
            this.comboTable.Location = new System.Drawing.Point(49, 6);
            this.comboTable.Name = "comboTable";
            this.comboTable.Size = new System.Drawing.Size(138, 21);
            this.comboTable.TabIndex = 3;
            this.comboTable.SelectedIndexChanged += new System.EventHandler(this.GraphTableChanged);
            // 
            // labelTable
            // 
            this.labelTable.AutoSize = true;
            this.labelTable.Location = new System.Drawing.Point(6, 9);
            this.labelTable.Name = "labelTable";
            this.labelTable.Size = new System.Drawing.Size(37, 13);
            this.labelTable.TabIndex = 4;
            this.labelTable.Text = "Table:";
            // 
            // comboXData
            // 
            this.comboXData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboXData.FormattingEnabled = true;
            this.comboXData.Location = new System.Drawing.Point(423, 6);
            this.comboXData.Name = "comboXData";
            this.comboXData.Size = new System.Drawing.Size(138, 21);
            this.comboXData.TabIndex = 5;
            this.comboXData.SelectedIndexChanged += new System.EventHandler(this.XDataChanged);
            // 
            // comboYData
            // 
            this.comboYData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboYData.FormattingEnabled = true;
            this.comboYData.Location = new System.Drawing.Point(616, 6);
            this.comboYData.Name = "comboYData";
            this.comboYData.Size = new System.Drawing.Size(138, 21);
            this.comboYData.TabIndex = 6;
            // 
            // labelXData
            // 
            this.labelXData.AutoSize = true;
            this.labelXData.Location = new System.Drawing.Point(374, 9);
            this.labelXData.Name = "labelXData";
            this.labelXData.Size = new System.Drawing.Size(43, 13);
            this.labelXData.TabIndex = 7;
            this.labelXData.Text = "X Data:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(567, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Y Data:";
            // 
            // comboTrait
            // 
            this.comboTrait.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTrait.FormattingEnabled = true;
            this.comboTrait.Items.AddRange(new object[] {
            "MetDatas",
            "PlotData",
            "SoilDatas",
            "SoilLayerDatas"});
            this.comboTrait.Location = new System.Drawing.Point(230, 6);
            this.comboTrait.Name = "comboTrait";
            this.comboTrait.Size = new System.Drawing.Size(138, 21);
            this.comboTrait.TabIndex = 10;
            this.comboTrait.SelectedIndexChanged += new System.EventHandler(this.GraphTraitChanged);
            // 
            // labelTrait
            // 
            this.labelTrait.AutoSize = true;
            this.labelTrait.Location = new System.Drawing.Point(193, 9);
            this.labelTrait.Name = "labelTrait";
            this.labelTrait.Size = new System.Drawing.Size(31, 13);
            this.labelTrait.TabIndex = 11;
            this.labelTrait.Text = "Trait:";
            // 
            // pageProperties
            // 
            this.pageProperties.Location = new System.Drawing.Point(4, 22);
            this.pageProperties.Name = "pageProperties";
            this.pageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.pageProperties.Size = new System.Drawing.Size(805, 667);
            this.pageProperties.TabIndex = 1;
            this.pageProperties.Text = "Properties";
            this.pageProperties.UseVisualStyleBackColor = true;
            // 
            // pageInfo
            // 
            this.pageInfo.BackColor = System.Drawing.Color.Transparent;
            this.pageInfo.Controls.Add(this.dataGridView);
            this.pageInfo.Controls.Add(this.relationsListBox);
            this.pageInfo.Location = new System.Drawing.Point(4, 22);
            this.pageInfo.Name = "pageInfo";
            this.pageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pageInfo.Size = new System.Drawing.Size(805, 667);
            this.pageInfo.TabIndex = 0;
            this.pageInfo.Text = "Information";
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.Location = new System.Drawing.Point(3, 3);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.Size = new System.Drawing.Size(143, 654);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.UpdatePageDisplay);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(152, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(650, 655);
            this.dataGridView.TabIndex = 1;
            // 
            // notebook
            // 
            this.notebook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebook.Controls.Add(this.pageInfo);
            this.notebook.Controls.Add(this.pageProperties);
            this.notebook.Controls.Add(this.pageGraph);
            this.notebook.Controls.Add(this.pageExps);
            this.notebook.Location = new System.Drawing.Point(0, 27);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(813, 693);
            this.notebook.TabIndex = 2;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 721);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "REMSClient";
            this.Text = "REMS 2020";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pageExps.ResumeLayout(false);
            this.experimentsTab.ResumeLayout(false);
            this.pageDesign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.designData)).EndInit();
            this.pageGraph.ResumeLayout(false);
            this.pageGraph.PerformLayout();
            this.pageInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage pageExps;
        private System.Windows.Forms.TreeView experimentsTree;
        private System.Windows.Forms.TabControl experimentsTab;
        private System.Windows.Forms.TabPage pageSummary;
        private System.Windows.Forms.TabPage pageDesign;
        private System.Windows.Forms.DataGridView designData;
        private System.Windows.Forms.TabPage pageOperation;
        private System.Windows.Forms.TabPage pageCrop;
        private System.Windows.Forms.TabPage pageSoil;
        private System.Windows.Forms.TabPage pageGraph;
        private System.Windows.Forms.Label labelTrait;
        private System.Windows.Forms.ComboBox comboTrait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelXData;
        private System.Windows.Forms.ComboBox comboYData;
        private System.Windows.Forms.ComboBox comboXData;
        private System.Windows.Forms.Label labelTable;
        private System.Windows.Forms.ComboBox comboTable;
        private Steema.TeeChart.TChart graph;
        private System.Windows.Forms.TabPage pageProperties;
        private System.Windows.Forms.TabPage pageInfo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
    }
}

