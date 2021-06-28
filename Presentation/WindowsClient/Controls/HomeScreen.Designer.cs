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
            recentList.DoubleClick -= OnRecentListDoubleClick;
            exportTracker.TaskBegun -= OnExportClick;

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
            this.groupExport = new System.Windows.Forms.GroupBox();
            this.exportTracker = new WindowsClient.Controls.TrackerBar();
            this.exportList = new System.Windows.Forms.CheckedListBox();
            this.groupImport = new System.Windows.Forms.GroupBox();
            this.importText = new System.Windows.Forms.RichTextBox();
            this.dataLink = new WindowsClient.Controls.ImportLink();
            this.expsLink = new WindowsClient.Controls.ImportLink();
            this.infoLink = new WindowsClient.Controls.ImportLink();
            this.groupFile.SuspendLayout();
            this.groupExport.SuspendLayout();
            this.groupImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupFile
            // 
            this.groupFile.Controls.Add(this.recentList);
            this.groupFile.Controls.Add(this.openLabel);
            this.groupFile.Controls.Add(this.createLabel);
            this.groupFile.Controls.Add(this.recentLabel);
            this.groupFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupFile.Location = new System.Drawing.Point(4, 3);
            this.groupFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupFile.Name = "groupFile";
            this.groupFile.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupFile.Size = new System.Drawing.Size(204, 415);
            this.groupFile.TabIndex = 0;
            this.groupFile.TabStop = false;
            this.groupFile.Text = "File";
            // 
            // recentList
            // 
            this.recentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recentList.FormattingEnabled = true;
            this.recentList.IntegralHeight = false;
            this.recentList.Location = new System.Drawing.Point(7, 85);
            this.recentList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recentList.MultiColumn = true;
            this.recentList.Name = "recentList";
            this.recentList.Size = new System.Drawing.Size(190, 323);
            this.recentList.TabIndex = 5;
            // 
            // openLabel
            // 
            this.openLabel.AutoSize = true;
            this.openLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.openLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.openLabel.Location = new System.Drawing.Point(7, 47);
            this.openLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.openLabel.Name = "openLabel";
            this.openLabel.Size = new System.Drawing.Size(80, 13);
            this.openLabel.TabIndex = 4;
            this.openLabel.Text = "Open existing...";
            this.openLabel.Click += new System.EventHandler(this.OnOpenClick);
            // 
            // createLabel
            // 
            this.createLabel.AutoSize = true;
            this.createLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.createLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.createLabel.Location = new System.Drawing.Point(7, 28);
            this.createLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.createLabel.Name = "createLabel";
            this.createLabel.Size = new System.Drawing.Size(70, 13);
            this.createLabel.TabIndex = 3;
            this.createLabel.Text = "Create new...";
            this.createLabel.Click += new System.EventHandler(this.OnCreateClick);
            // 
            // recentLabel
            // 
            this.recentLabel.AutoSize = true;
            this.recentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recentLabel.Location = new System.Drawing.Point(8, 66);
            this.recentLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.recentLabel.Name = "recentLabel";
            this.recentLabel.Size = new System.Drawing.Size(45, 13);
            this.recentLabel.TabIndex = 1;
            this.recentLabel.Text = "Recent:";
            // 
            // groupExport
            // 
            this.groupExport.Controls.Add(this.exportTracker);
            this.groupExport.Controls.Add(this.exportList);
            this.groupExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupExport.Location = new System.Drawing.Point(426, 3);
            this.groupExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupExport.Name = "groupExport";
            this.groupExport.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupExport.Size = new System.Drawing.Size(204, 415);
            this.groupExport.TabIndex = 3;
            this.groupExport.TabStop = false;
            this.groupExport.Text = "Experiments";
            this.groupExport.Visible = false;
            // 
            // exportTracker
            // 
            this.exportTracker.ButtonText = "Export";
            this.exportTracker.DisplayTask = true;
            this.exportTracker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.exportTracker.Location = new System.Drawing.Point(8, 345);
            this.exportTracker.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.exportTracker.Name = "exportTracker";
            this.exportTracker.Size = new System.Drawing.Size(189, 63);
            this.exportTracker.TabIndex = 2;
            // 
            // exportList
            // 
            this.exportList.CheckOnClick = true;
            this.exportList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exportList.FormattingEnabled = true;
            this.exportList.IntegralHeight = false;
            this.exportList.Location = new System.Drawing.Point(7, 28);
            this.exportList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.exportList.Name = "exportList";
            this.exportList.Size = new System.Drawing.Size(190, 314);
            this.exportList.TabIndex = 1;
            // 
            // groupImport
            // 
            this.groupImport.Controls.Add(this.importText);
            this.groupImport.Controls.Add(this.dataLink);
            this.groupImport.Controls.Add(this.expsLink);
            this.groupImport.Controls.Add(this.infoLink);
            this.groupImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupImport.Location = new System.Drawing.Point(215, 3);
            this.groupImport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupImport.Name = "groupImport";
            this.groupImport.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupImport.Size = new System.Drawing.Size(204, 415);
            this.groupImport.TabIndex = 4;
            this.groupImport.TabStop = false;
            this.groupImport.Text = "Import";
            this.groupImport.Visible = false;
            // 
            // importText
            // 
            this.importText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.importText.Location = new System.Drawing.Point(7, 132);
            this.importText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importText.Name = "importText";
            this.importText.ReadOnly = true;
            this.importText.Size = new System.Drawing.Size(190, 276);
            this.importText.TabIndex = 3;
            this.importText.Text = "A database must be connected prior to importing data.";
            // 
            // dataLink
            // 
            this.dataLink.Active = false;
            this.dataLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataLink.Image = ((System.Drawing.Image)(resources.GetObject("dataLink.Image")));
            this.dataLink.Label = "Data";
            this.dataLink.Location = new System.Drawing.Point(7, 96);
            this.dataLink.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataLink.Name = "dataLink";
            this.dataLink.Size = new System.Drawing.Size(190, 29);
            this.dataLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.dataLink.TabIndex = 2;
            // 
            // expsLink
            // 
            this.expsLink.Active = false;
            this.expsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.expsLink.Image = ((System.Drawing.Image)(resources.GetObject("expsLink.Image")));
            this.expsLink.Label = "Experiments";
            this.expsLink.Location = new System.Drawing.Point(7, 60);
            this.expsLink.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.expsLink.Name = "expsLink";
            this.expsLink.Size = new System.Drawing.Size(190, 29);
            this.expsLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.expsLink.TabIndex = 1;
            // 
            // infoLink
            // 
            this.infoLink.Active = false;
            this.infoLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infoLink.Image = ((System.Drawing.Image)(resources.GetObject("infoLink.Image")));
            this.infoLink.Label = "Information";
            this.infoLink.Location = new System.Drawing.Point(7, 24);
            this.infoLink.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.infoLink.Name = "infoLink";
            this.infoLink.Size = new System.Drawing.Size(190, 29);
            this.infoLink.Stage = WindowsClient.Controls.Stage.Missing;
            this.infoLink.TabIndex = 0;
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupImport);
            this.Controls.Add(this.groupExport);
            this.Controls.Add(this.groupFile);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "HomeScreen";
            this.Size = new System.Drawing.Size(810, 600);
            this.groupFile.ResumeLayout(false);
            this.groupFile.PerformLayout();
            this.groupExport.ResumeLayout(false);
            this.groupImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupFile;
        private System.Windows.Forms.Label recentLabel;
        private System.Windows.Forms.GroupBox groupExport;
        private System.Windows.Forms.Label openLabel;
        private System.Windows.Forms.Label createLabel;
        private System.Windows.Forms.ListBox recentList;
        private System.Windows.Forms.CheckedListBox exportList;
        private System.Windows.Forms.GroupBox groupImport;
        private ImportLink dataLink;
        private ImportLink expsLink;
        private ImportLink infoLink;
        private System.Windows.Forms.RichTextBox importText;
        private TrackerBar exportTracker;
    }
}
