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
            this.tablesTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.notebook = new System.Windows.Forms.TabControl();
            this.homeTab = new System.Windows.Forms.TabPage();
            this.homeScreen = new WindowsClient.Controls.HomeScreen();
            this.detailsTab = new System.Windows.Forms.TabPage();
            this.detailer = new WindowsClient.Controls.ExperimentDetailer();
            this.importTab = new System.Windows.Forms.TabPage();
            this.importer = new WindowsClient.Controls.Importer();
            this.tablesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
            this.homeTab.SuspendLayout();
            this.detailsTab.SuspendLayout();
            this.importTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesTab
            // 
            this.tablesTab.BackColor = System.Drawing.Color.Transparent;
            this.tablesTab.Controls.Add(this.splitContainer1);
            this.tablesTab.Location = new System.Drawing.Point(4, 22);
            this.tablesTab.Name = "tablesTab";
            this.tablesTab.Padding = new System.Windows.Forms.Padding(3);
            this.tablesTab.Size = new System.Drawing.Size(1056, 655);
            this.tablesTab.TabIndex = 0;
            this.tablesTab.Text = "Tables";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.relationsListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 649);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 6;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.IntegralHeight = false;
            this.relationsListBox.Location = new System.Drawing.Point(0, 0);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.ScrollAlwaysVisible = true;
            this.relationsListBox.Size = new System.Drawing.Size(227, 649);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.OnRelationsIndexChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(819, 649);
            this.dataGridView.TabIndex = 1;
            // 
            // notebook
            // 
            this.notebook.Controls.Add(this.homeTab);
            this.notebook.Controls.Add(this.tablesTab);
            this.notebook.Controls.Add(this.detailsTab);
            this.notebook.Controls.Add(this.importTab);
            this.notebook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(1064, 681);
            this.notebook.TabIndex = 2;
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.homeScreen);
            this.homeTab.Location = new System.Drawing.Point(4, 22);
            this.homeTab.Name = "homeTab";
            this.homeTab.Padding = new System.Windows.Forms.Padding(3);
            this.homeTab.Size = new System.Drawing.Size(1056, 655);
            this.homeTab.TabIndex = 7;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // homeScreen
            // 
            this.homeScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeScreen.Location = new System.Drawing.Point(3, 3);
            this.homeScreen.Manager = null;
            this.homeScreen.Name = "homeScreen";
            this.homeScreen.Size = new System.Drawing.Size(1050, 649);
            this.homeScreen.TabIndex = 0;
            // 
            // detailsTab
            // 
            this.detailsTab.Controls.Add(this.detailer);
            this.detailsTab.Location = new System.Drawing.Point(4, 22);
            this.detailsTab.Name = "detailsTab";
            this.detailsTab.Padding = new System.Windows.Forms.Padding(3);
            this.detailsTab.Size = new System.Drawing.Size(1056, 655);
            this.detailsTab.TabIndex = 9;
            this.detailsTab.Text = "Experiment details";
            this.detailsTab.UseVisualStyleBackColor = true;
            // 
            // detailer
            // 
            this.detailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailer.Location = new System.Drawing.Point(3, 3);
            this.detailer.Name = "detailer";
            this.detailer.Size = new System.Drawing.Size(1050, 649);
            this.detailer.TabIndex = 0;
            // 
            // importTab
            // 
            this.importTab.Controls.Add(this.importer);
            this.importTab.Location = new System.Drawing.Point(4, 22);
            this.importTab.Name = "importTab";
            this.importTab.Padding = new System.Windows.Forms.Padding(3);
            this.importTab.Size = new System.Drawing.Size(1056, 655);
            this.importTab.TabIndex = 8;
            this.importTab.Text = "Import";
            this.importTab.UseVisualStyleBackColor = true;
            // 
            // importer
            // 
            this.importer.Data = null;
            this.importer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importer.Folder = "C:\\Users\\uqmstow1\\Documents";
            this.importer.Location = new System.Drawing.Point(3, 3);
            this.importer.Name = "importer";
            this.importer.Size = new System.Drawing.Size(1050, 649);
            this.importer.TabIndex = 0;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "REMSClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "REMS 2020";
            this.tablesTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
            this.homeTab.ResumeLayout(false);
            this.detailsTab.ResumeLayout(false);
            this.importTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tablesTab;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage homeTab;
        private Controls.HomeScreen homeScreen;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage importTab;
        private Controls.Importer importer;
        private System.Windows.Forms.TabPage detailsTab;
        private Controls.ExperimentDetailer detailer;
    }
}

