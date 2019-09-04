namespace WindowsForm
{
    partial class ADAMSClient
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageDatabase = new System.Windows.Forms.TabPage();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pageAnalysis = new System.Windows.Forms.TabPage();
            this.pageModelling = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pageDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pageModelling.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(760, 24);
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
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.pageDatabase);
            this.tabControl1.Controls.Add(this.pageAnalysis);
            this.tabControl1.Controls.Add(this.pageModelling);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 429);
            this.tabControl1.TabIndex = 2;
            // 
            // pageDatabase
            // 
            this.pageDatabase.BackColor = System.Drawing.Color.Transparent;
            this.pageDatabase.Controls.Add(this.relationsListBox);
            this.pageDatabase.Controls.Add(this.dataGridView);
            this.pageDatabase.Location = new System.Drawing.Point(4, 22);
            this.pageDatabase.Name = "pageDatabase";
            this.pageDatabase.Padding = new System.Windows.Forms.Padding(3);
            this.pageDatabase.Size = new System.Drawing.Size(1829, 928);
            this.pageDatabase.TabIndex = 0;
            this.pageDatabase.Text = "Database";
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.Location = new System.Drawing.Point(0, 0);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.Size = new System.Drawing.Size(143, 927);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.SelectedRelationChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(143, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(1686, 928);
            this.dataGridView.TabIndex = 1;
            // 
            // pageAnalysis
            // 
            this.pageAnalysis.Location = new System.Drawing.Point(4, 22);
            this.pageAnalysis.Name = "pageAnalysis";
            this.pageAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.pageAnalysis.Size = new System.Drawing.Size(1829, 928);
            this.pageAnalysis.TabIndex = 1;
            this.pageAnalysis.Text = "Analysis";
            this.pageAnalysis.UseVisualStyleBackColor = true;
            // 
            // pageModelling
            // 
            this.pageModelling.Controls.Add(this.button1);
            this.pageModelling.Location = new System.Drawing.Point(4, 22);
            this.pageModelling.Name = "pageModelling";
            this.pageModelling.Padding = new System.Windows.Forms.Padding(3);
            this.pageModelling.Size = new System.Drawing.Size(752, 403);
            this.pageModelling.TabIndex = 2;
            this.pageModelling.Text = "Modelling";
            this.pageModelling.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ADAMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 455);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ADAMSClient";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.pageDatabase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pageModelling.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageDatabase;
        private System.Windows.Forms.TabPage pageAnalysis;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabPage pageModelling;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuImport;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem menuRecent;
        private System.Windows.Forms.Button button1;
    }
}

