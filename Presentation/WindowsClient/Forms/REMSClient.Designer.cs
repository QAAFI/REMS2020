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
            this.pageExport = new System.Windows.Forms.TabPage();
            this.exporter = new WindowsClient.Controls.Exporter();
            this.pageExps = new System.Windows.Forms.TabPage();
            this.pageInfo = new System.Windows.Forms.TabPage();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.notebook = new System.Windows.Forms.TabControl();
            this.pageImport = new System.Windows.Forms.TabPage();
            this.importer = new WindowsClient.Controls.Importer();
            this.experimentDetailer = new WindowsClient.Controls.ExperimentDetailer();
            this.pageExport.SuspendLayout();
            this.pageExps.SuspendLayout();
            this.pageInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
            this.pageImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageExport
            // 
            this.pageExport.Controls.Add(this.exporter);
            this.pageExport.Location = new System.Drawing.Point(4, 22);
            this.pageExport.Name = "pageExport";
            this.pageExport.Padding = new System.Windows.Forms.Padding(3);
            this.pageExport.Size = new System.Drawing.Size(859, 658);
            this.pageExport.TabIndex = 5;
            this.pageExport.Text = "Export";
            this.pageExport.UseVisualStyleBackColor = true;
            // 
            // exporter
            // 
            this.exporter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exporter.Folder = "C:\\Users\\uqmstow1\\Documents";
            this.exporter.Location = new System.Drawing.Point(0, 0);
            this.exporter.Name = "exporter";
            this.exporter.Size = new System.Drawing.Size(929, 631);
            this.exporter.TabIndex = 0;
            // 
            // pageExps
            // 
            this.pageExps.Controls.Add(this.experimentDetailer);
            this.pageExps.Location = new System.Drawing.Point(4, 22);
            this.pageExps.Name = "pageExps";
            this.pageExps.Padding = new System.Windows.Forms.Padding(3);
            this.pageExps.Size = new System.Drawing.Size(916, 621);
            this.pageExps.TabIndex = 4;
            this.pageExps.Text = "Experiments";
            this.pageExps.UseVisualStyleBackColor = true;
            // 
            // pageInfo
            // 
            this.pageInfo.BackColor = System.Drawing.Color.Transparent;
            this.pageInfo.Controls.Add(this.btnOpen);
            this.pageInfo.Controls.Add(this.btnNew);
            this.pageInfo.Controls.Add(this.dataGridView);
            this.pageInfo.Controls.Add(this.relationsListBox);
            this.pageInfo.Location = new System.Drawing.Point(4, 22);
            this.pageInfo.Name = "pageInfo";
            this.pageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pageInfo.Size = new System.Drawing.Size(859, 658);
            this.pageInfo.TabIndex = 0;
            this.pageInfo.Text = "Data";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(84, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.OnOpenClicked);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.OnNewClicked);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(165, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(691, 652);
            this.dataGridView.TabIndex = 1;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.IntegralHeight = false;
            this.relationsListBox.Location = new System.Drawing.Point(3, 32);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.ScrollAlwaysVisible = true;
            this.relationsListBox.Size = new System.Drawing.Size(156, 623);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.OnRelationsIndexChanged);
            // 
            // notebook
            // 
            this.notebook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notebook.Controls.Add(this.pageInfo);
            this.notebook.Controls.Add(this.pageExps);
            this.notebook.Controls.Add(this.pageImport);
            this.notebook.Controls.Add(this.pageExport);
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(924, 647);
            this.notebook.TabIndex = 2;
            // 
            // pageImport
            // 
            this.pageImport.Controls.Add(this.importer);
            this.pageImport.Location = new System.Drawing.Point(4, 22);
            this.pageImport.Name = "pageImport";
            this.pageImport.Padding = new System.Windows.Forms.Padding(3);
            this.pageImport.Size = new System.Drawing.Size(859, 658);
            this.pageImport.TabIndex = 6;
            this.pageImport.Text = "Import";
            this.pageImport.UseVisualStyleBackColor = true;
            // 
            // importer
            // 
            this.importer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importer.Folder = "C:\\Users\\uqmstow1\\Documents";
            this.importer.Location = new System.Drawing.Point(0, 0);
            this.importer.Name = "importer";
            this.importer.Size = new System.Drawing.Size(929, 631);
            this.importer.TabIndex = 0;
            // 
            // experimentDetailer1
            // 
            this.experimentDetailer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.experimentDetailer.Location = new System.Drawing.Point(0, 0);
            this.experimentDetailer.MinimumSize = new System.Drawing.Size(920, 620);
            this.experimentDetailer.Name = "experimentDetailer1";
            this.experimentDetailer.Size = new System.Drawing.Size(920, 620);
            this.experimentDetailer.TabIndex = 0;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 646);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "REMSClient";
            this.Text = "REMS 2020";
            this.pageExport.ResumeLayout(false);
            this.pageExps.ResumeLayout(false);
            this.pageInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
            this.pageImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage pageExport;
        private System.Windows.Forms.TabPage pageExps;
        private System.Windows.Forms.TabPage pageInfo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage pageImport;
        private Controls.Importer importer;
        private Controls.Exporter exporter;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnNew;
        private Controls.ExperimentDetailer experimentDetailer;
    }
}

