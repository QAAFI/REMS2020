namespace WindowsClient.Controls
{
    partial class Importer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageIData = new System.Windows.Forms.TabPage();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tableBox = new System.Windows.Forms.ListBox();
            this.importData = new System.Windows.Forms.DataGridView();
            this.pageIValidater = new System.Windows.Forms.TabPage();
            this.importValidater = new WindowsClient.Controls.ImportValidater();
            this.tabControl1.SuspendLayout();
            this.pageIData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            this.pageIValidater.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.pageIData);
            this.tabControl1.Controls.Add(this.pageIValidater);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(655, 588);
            this.tabControl1.TabIndex = 2;
            // 
            // pageIData
            // 
            this.pageIData.Controls.Add(this.btnImport);
            this.pageIData.Controls.Add(this.btnLoad);
            this.pageIData.Controls.Add(this.tableBox);
            this.pageIData.Controls.Add(this.importData);
            this.pageIData.Location = new System.Drawing.Point(4, 22);
            this.pageIData.Name = "pageIData";
            this.pageIData.Padding = new System.Windows.Forms.Padding(3);
            this.pageIData.Size = new System.Drawing.Size(647, 562);
            this.pageIData.TabIndex = 0;
            this.pageIData.Text = "Data";
            this.pageIData.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(80, 0);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 3;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.OnImportClicked);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(0, 0);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.OnLoadClicked);
            // 
            // tableBox
            // 
            this.tableBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableBox.FormattingEnabled = true;
            this.tableBox.IntegralHeight = false;
            this.tableBox.Location = new System.Drawing.Point(0, 27);
            this.tableBox.Name = "tableBox";
            this.tableBox.ScrollAlwaysVisible = true;
            this.tableBox.Size = new System.Drawing.Size(155, 535);
            this.tableBox.TabIndex = 1;
            this.tableBox.SelectedIndexChanged += new System.EventHandler(this.OnTableSelected);
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importData.Location = new System.Drawing.Point(161, 0);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(486, 562);
            this.importData.TabIndex = 0;
            // 
            // pageIValidater
            // 
            this.pageIValidater.Controls.Add(this.importValidater);
            this.pageIValidater.Location = new System.Drawing.Point(4, 22);
            this.pageIValidater.Name = "pageIValidater";
            this.pageIValidater.Padding = new System.Windows.Forms.Padding(3);
            this.pageIValidater.Size = new System.Drawing.Size(647, 562);
            this.pageIValidater.TabIndex = 1;
            this.pageIValidater.Text = "Validater";
            this.pageIValidater.UseVisualStyleBackColor = true;
            // 
            // importValidater
            // 
            this.importValidater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importValidater.Location = new System.Drawing.Point(-1, 0);
            this.importValidater.Name = "importValidater";
            this.importValidater.Size = new System.Drawing.Size(648, 562);
            this.importValidater.TabIndex = 0;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(655, 588);
            this.tabControl1.ResumeLayout(false);
            this.pageIData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            this.pageIValidater.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageIData;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListBox tableBox;
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.TabPage pageIValidater;
        private ImportValidater importValidater;
    }
}
