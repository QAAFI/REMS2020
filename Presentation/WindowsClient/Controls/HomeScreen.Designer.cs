namespace WindowsClient.Controls
{
    partial class HomeScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeScreen));
            this.groupFile = new System.Windows.Forms.GroupBox();
            this.recentList = new System.Windows.Forms.ListBox();
            this.openLabel = new System.Windows.Forms.Label();
            this.createLabel = new System.Windows.Forms.Label();
            this.recentLabel = new System.Windows.Forms.Label();
            this.groupImport = new System.Windows.Forms.GroupBox();
            this.groupAbout = new System.Windows.Forms.GroupBox();
            this.groupExperiments = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataLink = new WindowsClient.Controls.ImportLink();
            this.expsLink = new WindowsClient.Controls.ImportLink();
            this.infoLink = new WindowsClient.Controls.ImportLink();
            this.export = new System.Windows.Forms.Button();
            this.groupFile.SuspendLayout();
            this.groupImport.SuspendLayout();
            this.groupExperiments.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFile
            // 
            this.groupFile.Controls.Add(this.recentList);
            this.groupFile.Controls.Add(this.openLabel);
            this.groupFile.Controls.Add(this.createLabel);
            this.groupFile.Controls.Add(this.recentLabel);
            this.groupFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFile.Location = new System.Drawing.Point(3, 3);
            this.groupFile.MinimumSize = new System.Drawing.Size(250, 250);
            this.groupFile.Name = "groupFile";
            this.groupFile.Size = new System.Drawing.Size(254, 254);
            this.groupFile.TabIndex = 0;
            this.groupFile.TabStop = false;
            this.groupFile.Text = "File";
            // 
            // recentList
            // 
            this.recentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recentList.FormattingEnabled = true;
            this.recentList.IntegralHeight = false;
            this.recentList.ItemHeight = 20;
            this.recentList.Location = new System.Drawing.Point(6, 97);
            this.recentList.MultiColumn = true;
            this.recentList.Name = "recentList";
            this.recentList.Size = new System.Drawing.Size(242, 151);
            this.recentList.TabIndex = 5;
            // 
            // openLabel
            // 
            this.openLabel.AutoSize = true;
            this.openLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.openLabel.Location = new System.Drawing.Point(6, 45);
            this.openLabel.Margin = new System.Windows.Forms.Padding(3);
            this.openLabel.Name = "openLabel";
            this.openLabel.Size = new System.Drawing.Size(117, 20);
            this.openLabel.TabIndex = 4;
            this.openLabel.Text = "Open existing...";
            this.openLabel.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // createLabel
            // 
            this.createLabel.AutoSize = true;
            this.createLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.createLabel.Location = new System.Drawing.Point(6, 19);
            this.createLabel.Margin = new System.Windows.Forms.Padding(3);
            this.createLabel.Name = "createLabel";
            this.createLabel.Size = new System.Drawing.Size(102, 20);
            this.createLabel.TabIndex = 3;
            this.createLabel.Text = "Create new...";
            this.createLabel.Click += new System.EventHandler(this.OnCreateClick);
            // 
            // recentLabel
            // 
            this.recentLabel.AutoSize = true;
            this.recentLabel.Location = new System.Drawing.Point(6, 71);
            this.recentLabel.Margin = new System.Windows.Forms.Padding(3);
            this.recentLabel.Name = "recentLabel";
            this.recentLabel.Size = new System.Drawing.Size(65, 20);
            this.recentLabel.TabIndex = 1;
            this.recentLabel.Text = "Recent:";
            // 
            // groupImport
            // 
            this.groupImport.Controls.Add(this.dataLink);
            this.groupImport.Controls.Add(this.expsLink);
            this.groupImport.Controls.Add(this.infoLink);
            this.groupImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupImport.Location = new System.Drawing.Point(263, 3);
            this.groupImport.MinimumSize = new System.Drawing.Size(250, 250);
            this.groupImport.Name = "groupImport";
            this.groupImport.Size = new System.Drawing.Size(254, 254);
            this.groupImport.TabIndex = 1;
            this.groupImport.TabStop = false;
            this.groupImport.Text = "Import";
            // 
            // groupAbout
            // 
            this.groupAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupAbout.Location = new System.Drawing.Point(3, 263);
            this.groupAbout.MinimumSize = new System.Drawing.Size(250, 250);
            this.groupAbout.Name = "groupAbout";
            this.groupAbout.Size = new System.Drawing.Size(254, 254);
            this.groupAbout.TabIndex = 2;
            this.groupAbout.TabStop = false;
            this.groupAbout.Text = "About";
            // 
            // groupExperiments
            // 
            this.groupExperiments.Controls.Add(this.export);
            this.groupExperiments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupExperiments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupExperiments.Location = new System.Drawing.Point(263, 263);
            this.groupExperiments.MinimumSize = new System.Drawing.Size(250, 250);
            this.groupExperiments.Name = "groupExperiments";
            this.groupExperiments.Size = new System.Drawing.Size(254, 254);
            this.groupExperiments.TabIndex = 3;
            this.groupExperiments.TabStop = false;
            this.groupExperiments.Text = "Experiments";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupFile, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupAbout, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupExperiments, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupImport, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.MinimumSize = new System.Drawing.Size(500, 500);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(520, 520);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // dataLink
            // 
            this.dataLink.File = null;
            this.dataLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLink.Image = ((System.Drawing.Image)(resources.GetObject("dataLink.Image")));
            this.dataLink.Label = "Data";
            this.dataLink.Location = new System.Drawing.Point(6, 107);
            this.dataLink.Name = "dataLink";
            this.dataLink.Size = new System.Drawing.Size(238, 38);
            this.dataLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.dataLink.TabIndex = 2;
            // 
            // expsLink
            // 
            this.expsLink.File = null;
            this.expsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expsLink.Image = ((System.Drawing.Image)(resources.GetObject("expsLink.Image")));
            this.expsLink.Label = "Experiments";
            this.expsLink.Location = new System.Drawing.Point(6, 63);
            this.expsLink.Name = "expsLink";
            this.expsLink.Size = new System.Drawing.Size(238, 38);
            this.expsLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.expsLink.TabIndex = 1;
            // 
            // infoLink
            // 
            this.infoLink.File = null;
            this.infoLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLink.Image = ((System.Drawing.Image)(resources.GetObject("infoLink.Image")));
            this.infoLink.Label = "Information";
            this.infoLink.Location = new System.Drawing.Point(6, 19);
            this.infoLink.Name = "infoLink";
            this.infoLink.Size = new System.Drawing.Size(238, 38);
            this.infoLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.infoLink.TabIndex = 0;
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(166, 220);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(78, 28);
            this.export.TabIndex = 0;
            this.export.Text = "Export";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.OnExportClick);
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "HomeScreen";
            this.Size = new System.Drawing.Size(520, 520);
            this.groupFile.ResumeLayout(false);
            this.groupFile.PerformLayout();
            this.groupImport.ResumeLayout(false);
            this.groupExperiments.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupFile;
        private System.Windows.Forms.Label recentLabel;
        private System.Windows.Forms.GroupBox groupImport;
        private System.Windows.Forms.GroupBox groupAbout;
        private System.Windows.Forms.GroupBox groupExperiments;
        private System.Windows.Forms.Label openLabel;
        private System.Windows.Forms.Label createLabel;
        private System.Windows.Forms.ListBox recentList;
        private ImportLink infoLink;
        private ImportLink dataLink;
        private ImportLink expsLink;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button export;
    }
}
