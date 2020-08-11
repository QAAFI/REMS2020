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
            this.experimentsTree = new System.Windows.Forms.TreeView();
            this.experimentsTab = new System.Windows.Forms.TabControl();
            this.pageSummary = new System.Windows.Forms.TabPage();
            this.pageDesign = new System.Windows.Forms.TabPage();
            this.designData = new System.Windows.Forms.DataGridView();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.pageData = new System.Windows.Forms.TabPage();
            this.rightBtn = new System.Windows.Forms.Button();
            this.leftBtn = new System.Windows.Forms.Button();
            this.traitTypeBox = new System.Windows.Forms.ComboBox();
            this.traitsBox = new System.Windows.Forms.CheckedListBox();
            this.cropChart = new Steema.TeeChart.TChart();
            this.pageProperties = new System.Windows.Forms.TabPage();
            this.pageInfo = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.notebook = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.pageExps.SuspendLayout();
            this.experimentsTab.SuspendLayout();
            this.pageDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).BeginInit();
            this.pageData.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(938, 24);
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
            this.menuNew.Size = new System.Drawing.Size(180, 22);
            this.menuNew.Text = "New";
            this.menuNew.Click += new System.EventHandler(this.MenuNewClicked);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(180, 22);
            this.menuOpen.Text = "Open";
            this.menuOpen.Click += new System.EventHandler(this.MenuOpenClicked);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(180, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.MenuSaveClicked);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(180, 22);
            this.menuSaveAs.Text = "Save as";
            this.menuSaveAs.Click += new System.EventHandler(this.MenuSaveAsClicked);
            // 
            // menuRecent
            // 
            this.menuRecent.Name = "menuRecent";
            this.menuRecent.Size = new System.Drawing.Size(180, 22);
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
            this.pageExps.Size = new System.Drawing.Size(930, 649);
            this.pageExps.TabIndex = 4;
            this.pageExps.Text = "Experiments";
            this.pageExps.UseVisualStyleBackColor = true;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.experimentsTree.Location = new System.Drawing.Point(6, 6);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(144, 637);
            this.experimentsTree.TabIndex = 2;
            // 
            // experimentsTab
            // 
            this.experimentsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.experimentsTab.Controls.Add(this.pageSummary);
            this.experimentsTab.Controls.Add(this.pageDesign);
            this.experimentsTab.Controls.Add(this.pageOperation);
            this.experimentsTab.Controls.Add(this.pageData);
            this.experimentsTab.Location = new System.Drawing.Point(156, 3);
            this.experimentsTab.Name = "experimentsTab";
            this.experimentsTab.SelectedIndex = 0;
            this.experimentsTab.Size = new System.Drawing.Size(771, 640);
            this.experimentsTab.TabIndex = 1;
            // 
            // pageSummary
            // 
            this.pageSummary.Location = new System.Drawing.Point(4, 22);
            this.pageSummary.Name = "pageSummary";
            this.pageSummary.Padding = new System.Windows.Forms.Padding(3);
            this.pageSummary.Size = new System.Drawing.Size(763, 614);
            this.pageSummary.TabIndex = 7;
            this.pageSummary.Text = "Summary";
            this.pageSummary.UseVisualStyleBackColor = true;
            // 
            // pageDesign
            // 
            this.pageDesign.Controls.Add(this.designData);
            this.pageDesign.Location = new System.Drawing.Point(4, 22);
            this.pageDesign.Name = "pageDesign";
            this.pageDesign.Padding = new System.Windows.Forms.Padding(3);
            this.pageDesign.Size = new System.Drawing.Size(763, 614);
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
            this.designData.Size = new System.Drawing.Size(763, 613);
            this.designData.TabIndex = 0;
            // 
            // pageOperation
            // 
            this.pageOperation.Location = new System.Drawing.Point(4, 22);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.pageOperation.Size = new System.Drawing.Size(763, 614);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            this.pageOperation.UseVisualStyleBackColor = true;
            // 
            // pageData
            // 
            this.pageData.BackColor = System.Drawing.Color.LightGray;
            this.pageData.Controls.Add(this.rightBtn);
            this.pageData.Controls.Add(this.leftBtn);
            this.pageData.Controls.Add(this.traitTypeBox);
            this.pageData.Controls.Add(this.traitsBox);
            this.pageData.Controls.Add(this.cropChart);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(3);
            this.pageData.Size = new System.Drawing.Size(763, 614);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // rightBtn
            // 
            this.rightBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightBtn.BackgroundImage = global::WindowsClient.Properties.Resources.right;
            this.rightBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightBtn.Location = new System.Drawing.Point(572, 17);
            this.rightBtn.Name = "rightBtn";
            this.rightBtn.Size = new System.Drawing.Size(24, 24);
            this.rightBtn.TabIndex = 14;
            this.rightBtn.UseVisualStyleBackColor = true;
            this.rightBtn.Visible = false;
            this.rightBtn.Click += new System.EventHandler(this.OnRightBtnClicked);
            // 
            // leftBtn
            // 
            this.leftBtn.BackgroundImage = global::WindowsClient.Properties.Resources.left;
            this.leftBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.leftBtn.Location = new System.Drawing.Point(17, 17);
            this.leftBtn.Name = "leftBtn";
            this.leftBtn.Size = new System.Drawing.Size(24, 24);
            this.leftBtn.TabIndex = 13;
            this.leftBtn.UseVisualStyleBackColor = true;
            this.leftBtn.Visible = false;
            this.leftBtn.Click += new System.EventHandler(this.OnLeftBtnClicked);
            // 
            // traitTypeBox
            // 
            this.traitTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.traitTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.traitTypeBox.FormattingEnabled = true;
            this.traitTypeBox.Location = new System.Drawing.Point(614, 6);
            this.traitTypeBox.Name = "traitTypeBox";
            this.traitTypeBox.Size = new System.Drawing.Size(143, 21);
            this.traitTypeBox.TabIndex = 12;
            this.traitTypeBox.SelectedIndexChanged += new System.EventHandler(this.OnTraitTypeBoxSelectionChanged);
            // 
            // traitsBox
            // 
            this.traitsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.traitsBox.CheckOnClick = true;
            this.traitsBox.FormattingEnabled = true;
            this.traitsBox.Location = new System.Drawing.Point(614, 32);
            this.traitsBox.Name = "traitsBox";
            this.traitsBox.Size = new System.Drawing.Size(143, 574);
            this.traitsBox.TabIndex = 11;
            // 
            // cropChart
            // 
            this.cropChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
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
            this.cropChart.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.Bottom.Labels.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.Bottom.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Bottom.Labels.Font.Size = 9;
            this.cropChart.Axes.Bottom.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Bottom.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Bottom.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Angle = 0;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.Bottom.Title.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.Bottom.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Bottom.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Bottom.Title.Font.Size = 11;
            this.cropChart.Axes.Bottom.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Bottom.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Bottom.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Bottom.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.Depth.Labels.Brush.Solid = true;
            this.cropChart.Axes.Depth.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.Depth.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.Depth.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Depth.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Depth.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Depth.Labels.Font.Size = 9;
            this.cropChart.Axes.Depth.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Depth.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Depth.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Depth.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Depth.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Angle = 0;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.Depth.Title.Brush.Solid = true;
            this.cropChart.Axes.Depth.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.Depth.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.Depth.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Depth.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Depth.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Depth.Title.Font.Size = 11;
            this.cropChart.Axes.Depth.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Depth.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Depth.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Depth.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Depth.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.DepthTop.Labels.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.DepthTop.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.DepthTop.Labels.Font.Size = 9;
            this.cropChart.Axes.DepthTop.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.DepthTop.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Angle = 0;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.DepthTop.Title.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.DepthTop.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.DepthTop.Title.Font.Size = 11;
            this.cropChart.Axes.DepthTop.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.DepthTop.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.DepthTop.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.DepthTop.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.Left.Labels.Brush.Solid = true;
            this.cropChart.Axes.Left.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.Left.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.Left.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Left.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Left.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Left.Labels.Font.Size = 9;
            this.cropChart.Axes.Left.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Left.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Left.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Left.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Left.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Angle = 90;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.Left.Title.Brush.Solid = true;
            this.cropChart.Axes.Left.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.Left.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.Left.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Left.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Left.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Left.Title.Font.Size = 11;
            this.cropChart.Axes.Left.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Left.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Left.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Left.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Left.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.Right.Labels.Brush.Solid = true;
            this.cropChart.Axes.Right.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.Right.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.Right.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Right.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Right.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Right.Labels.Font.Size = 9;
            this.cropChart.Axes.Right.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Right.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Right.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Right.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Right.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Angle = 270;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.Right.Title.Brush.Solid = true;
            this.cropChart.Axes.Right.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.Right.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.Right.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Right.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Right.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Right.Title.Font.Size = 11;
            this.cropChart.Axes.Right.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Right.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Right.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Right.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Right.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Axes.Top.Labels.Brush.Solid = true;
            this.cropChart.Axes.Top.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Axes.Top.Labels.Font.Brush.Solid = true;
            this.cropChart.Axes.Top.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Top.Labels.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Top.Labels.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Top.Labels.Font.Size = 9;
            this.cropChart.Axes.Top.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Top.Labels.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Top.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Top.Labels.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Top.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Angle = 0;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Axes.Top.Title.Brush.Solid = true;
            this.cropChart.Axes.Top.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Axes.Top.Title.Font.Brush.Solid = true;
            this.cropChart.Axes.Top.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Top.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Top.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Axes.Top.Title.Font.Size = 11;
            this.cropChart.Axes.Top.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Axes.Top.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Axes.Top.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Axes.Top.Title.Shadow.Brush.Solid = true;
            this.cropChart.Axes.Top.Title.Shadow.Brush.Visible = true;
            this.cropChart.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Footer.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Footer.Brush.Solid = true;
            this.cropChart.Footer.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Footer.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Footer.Font.Brush.Color = System.Drawing.Color.Red;
            this.cropChart.Footer.Font.Brush.Solid = true;
            this.cropChart.Footer.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Footer.Font.Shadow.Brush.Solid = true;
            this.cropChart.Footer.Font.Shadow.Brush.Visible = true;
            this.cropChart.Footer.Font.Size = 8;
            this.cropChart.Footer.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Footer.ImageBevel.Brush.Solid = true;
            this.cropChart.Footer.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Footer.Shadow.Brush.Solid = true;
            this.cropChart.Footer.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Header.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cropChart.Header.Brush.Solid = true;
            this.cropChart.Header.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Header.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.Header.Font.Brush.Solid = true;
            this.cropChart.Header.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Header.Font.Shadow.Brush.Solid = true;
            this.cropChart.Header.Font.Shadow.Brush.Visible = true;
            this.cropChart.Header.Font.Size = 12;
            this.cropChart.Header.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Header.ImageBevel.Brush.Solid = true;
            this.cropChart.Header.ImageBevel.Brush.Visible = true;
            this.cropChart.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cropChart.Header.Shadow.Brush.Solid = true;
            this.cropChart.Header.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Legend.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Legend.Brush.Solid = true;
            this.cropChart.Legend.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Legend.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cropChart.Legend.Font.Brush.Solid = true;
            this.cropChart.Legend.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Legend.Font.Shadow.Brush.Solid = true;
            this.cropChart.Legend.Font.Shadow.Brush.Visible = true;
            this.cropChart.Legend.Font.Size = 9;
            this.cropChart.Legend.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Legend.ImageBevel.Brush.Solid = true;
            this.cropChart.Legend.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cropChart.Legend.Shadow.Brush.Solid = true;
            this.cropChart.Legend.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Legend.Symbol.Shadow.Brush.Solid = true;
            this.cropChart.Legend.Symbol.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Legend.Title.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Legend.Title.Brush.Solid = true;
            this.cropChart.Legend.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.cropChart.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black;
            this.cropChart.Legend.Title.Font.Brush.Solid = true;
            this.cropChart.Legend.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Legend.Title.Font.Shadow.Brush.Solid = true;
            this.cropChart.Legend.Title.Font.Shadow.Brush.Visible = true;
            this.cropChart.Legend.Title.Font.Size = 8;
            this.cropChart.Legend.Title.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Legend.Title.ImageBevel.Brush.Solid = true;
            this.cropChart.Legend.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Legend.Title.Shadow.Brush.Solid = true;
            this.cropChart.Legend.Title.Shadow.Brush.Visible = true;
            this.cropChart.Location = new System.Drawing.Point(6, 6);
            this.cropChart.Name = "cropChart";
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cropChart.Panel.Brush.Solid = true;
            this.cropChart.Panel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Panel.ImageBevel.Brush.Solid = true;
            this.cropChart.Panel.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Panel.Shadow.Brush.Solid = true;
            this.cropChart.Panel.Shadow.Brush.Visible = true;
            this.cropChart.Size = new System.Drawing.Size(600, 600);
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.SubFooter.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.SubFooter.Brush.Solid = true;
            this.cropChart.SubFooter.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.SubFooter.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.SubFooter.Font.Brush.Color = System.Drawing.Color.Red;
            this.cropChart.SubFooter.Font.Brush.Solid = true;
            this.cropChart.SubFooter.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.SubFooter.Font.Shadow.Brush.Solid = true;
            this.cropChart.SubFooter.Font.Shadow.Brush.Visible = true;
            this.cropChart.SubFooter.Font.Size = 8;
            this.cropChart.SubFooter.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.SubFooter.ImageBevel.Brush.Solid = true;
            this.cropChart.SubFooter.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.SubFooter.Shadow.Brush.Solid = true;
            this.cropChart.SubFooter.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.cropChart.SubHeader.Brush.Solid = true;
            this.cropChart.SubHeader.Brush.Visible = true;
            // 
            // 
            // 
            this.cropChart.SubHeader.Font.Bold = false;
            // 
            // 
            // 
            this.cropChart.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cropChart.SubHeader.Font.Brush.Solid = true;
            this.cropChart.SubHeader.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.SubHeader.Font.Shadow.Brush.Solid = true;
            this.cropChart.SubHeader.Font.Shadow.Brush.Visible = true;
            this.cropChart.SubHeader.Font.Size = 12;
            this.cropChart.SubHeader.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.SubHeader.ImageBevel.Brush.Solid = true;
            this.cropChart.SubHeader.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cropChart.SubHeader.Shadow.Brush.Solid = true;
            this.cropChart.SubHeader.Shadow.Brush.Visible = true;
            this.cropChart.TabIndex = 10;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Walls.Back.Brush.Color = System.Drawing.Color.Silver;
            this.cropChart.Walls.Back.Brush.Solid = true;
            this.cropChart.Walls.Back.Brush.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Walls.Back.ImageBevel.Brush.Solid = true;
            this.cropChart.Walls.Back.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Walls.Back.Shadow.Brush.Solid = true;
            this.cropChart.Walls.Back.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Walls.Bottom.Brush.Color = System.Drawing.Color.White;
            this.cropChart.Walls.Bottom.Brush.Solid = true;
            this.cropChart.Walls.Bottom.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Walls.Bottom.ImageBevel.Brush.Solid = true;
            this.cropChart.Walls.Bottom.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Walls.Bottom.Shadow.Brush.Solid = true;
            this.cropChart.Walls.Bottom.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow;
            this.cropChart.Walls.Left.Brush.Solid = true;
            this.cropChart.Walls.Left.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Walls.Left.ImageBevel.Brush.Solid = true;
            this.cropChart.Walls.Left.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Walls.Left.Shadow.Brush.Solid = true;
            this.cropChart.Walls.Left.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.cropChart.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow;
            this.cropChart.Walls.Right.Brush.Solid = true;
            this.cropChart.Walls.Right.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.cropChart.Walls.Right.ImageBevel.Brush.Solid = true;
            this.cropChart.Walls.Right.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.cropChart.Walls.Right.Shadow.Brush.Solid = true;
            this.cropChart.Walls.Right.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.cropChart.Zoom.Brush.Color = System.Drawing.Color.LightBlue;
            this.cropChart.Zoom.Brush.Solid = true;
            this.cropChart.Zoom.Brush.Visible = true;
            // 
            // pageProperties
            // 
            this.pageProperties.Location = new System.Drawing.Point(4, 22);
            this.pageProperties.Name = "pageProperties";
            this.pageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.pageProperties.Size = new System.Drawing.Size(930, 649);
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
            this.pageInfo.Size = new System.Drawing.Size(930, 649);
            this.pageInfo.TabIndex = 0;
            this.pageInfo.Text = "Information";
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
            this.dataGridView.Size = new System.Drawing.Size(775, 638);
            this.dataGridView.TabIndex = 1;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.Location = new System.Drawing.Point(3, 3);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.Size = new System.Drawing.Size(143, 628);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.OnRelationsIndexChanged);
            // 
            // notebook
            // 
            this.notebook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebook.Controls.Add(this.pageInfo);
            this.notebook.Controls.Add(this.pageProperties);
            this.notebook.Controls.Add(this.pageExps);
            this.notebook.Location = new System.Drawing.Point(0, 27);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(938, 675);
            this.notebook.TabIndex = 2;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 703);
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
            this.pageData.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage pageData;
        private System.Windows.Forms.TabPage pageProperties;
        private System.Windows.Forms.TabPage pageInfo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.CheckedListBox traitsBox;
        private Steema.TeeChart.TChart cropChart;
        private System.Windows.Forms.ComboBox traitTypeBox;
        private System.Windows.Forms.Button rightBtn;
        private System.Windows.Forms.Button leftBtn;
    }
}

