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
            ImportCompleted = null;
            ImportCancelled = null;
            
            dataTree.AfterLabelEdit -= AfterLabelEdit;
            tracker.TaskBegun -= RunImporter;

            Data?.Dispose();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Importer));
            this.gridLabel = new System.Windows.Forms.Label();
            this.importData = new System.Windows.Forms.DataGridView();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileBtn = new System.Windows.Forms.Button();
            this.tracker = new WindowsClient.Controls.TrackerBar();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.adviceBox = new System.Windows.Forms.RichTextBox();
            this.warning = new System.Windows.Forms.TableLayoutPanel();
            this.warninglabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.warning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLabel
            // 
            this.gridLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gridLabel.Location = new System.Drawing.Point(4, 8);
            this.gridLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gridLabel.Name = "gridLabel";
            this.gridLabel.Size = new System.Drawing.Size(844, 20);
            this.gridLabel.TabIndex = 10;
            this.gridLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.importData.DefaultCellStyle = dataGridViewCellStyle1;
            this.importData.Location = new System.Drawing.Point(4, 31);
            this.importData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(845, 774);
            this.importData.TabIndex = 0;
            // 
            // fileBox
            // 
            this.fileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileBox.Enabled = false;
            this.fileBox.Location = new System.Drawing.Point(35, 3);
            this.fileBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(263, 23);
            this.fileBox.TabIndex = 10;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.warning);
            this.splitter.Panel2.Controls.Add(this.gridLabel);
            this.splitter.Panel2.Controls.Add(this.importData);
            this.splitter.Size = new System.Drawing.Size(1167, 808);
            this.splitter.SplitterDistance = 303;
            this.splitter.SplitterWidth = 5;
            this.splitter.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileBtn);
            this.splitContainer1.Panel1.Controls.Add(this.tracker);
            this.splitContainer1.Panel1.Controls.Add(this.dataTree);
            this.splitContainer1.Panel1.Controls.Add(this.fileBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(303, 808);
            this.splitContainer1.SplitterDistance = 601;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 14;
            // 
            // fileBtn
            // 
            this.fileBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fileBtn.BackgroundImage")));
            this.fileBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fileBtn.Location = new System.Drawing.Point(5, 3);
            this.fileBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.fileBtn.Name = "fileBtn";
            this.fileBtn.Size = new System.Drawing.Size(23, 23);
            this.fileBtn.TabIndex = 15;
            this.fileBtn.UseVisualStyleBackColor = true;
            this.fileBtn.Click += new System.EventHandler(this.OnFileButtonClicked);
            // 
            // tracker
            // 
            this.tracker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracker.ButtonText = "Import";
            this.tracker.DisplayTask = true;
            this.tracker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tracker.Location = new System.Drawing.Point(4, 33);
            this.tracker.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.tracker.Name = "tracker";
            this.tracker.Size = new System.Drawing.Size(295, 63);
            this.tracker.TabIndex = 14;
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTree.FullRowSelect = true;
            this.dataTree.HideSelection = false;
            this.dataTree.LabelEdit = true;
            this.dataTree.Location = new System.Drawing.Point(5, 102);
            this.dataTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataTree.Name = "dataTree";
            this.dataTree.ShowNodeToolTips = true;
            this.dataTree.Size = new System.Drawing.Size(294, 496);
            this.dataTree.TabIndex = 12;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.adviceBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 202);
            this.panel1.TabIndex = 13;
            // 
            // adviceBox
            // 
            this.adviceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adviceBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.adviceBox.Location = new System.Drawing.Point(0, 0);
            this.adviceBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.adviceBox.Name = "adviceBox";
            this.adviceBox.ReadOnly = true;
            this.adviceBox.Size = new System.Drawing.Size(303, 202);
            this.adviceBox.TabIndex = 13;
            this.adviceBox.Text = "";
            // 
            // warning
            // 
            this.warning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warning.ColumnCount = 2;
            this.warning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.11765F));
            this.warning.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.88235F));
            this.warning.Controls.Add(this.warninglabel, 1, 0);
            this.warning.Controls.Add(this.pictureBox1, 0, 0);
            this.warning.Location = new System.Drawing.Point(4, 33);
            this.warning.Name = "warning";
            this.warning.RowCount = 1;
            this.warning.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.warning.Size = new System.Drawing.Size(845, 200);
            this.warning.TabIndex = 12;
            this.warning.Visible = false;
            // 
            // warninglabel
            // 
            this.warninglabel.AutoSize = true;
            this.warninglabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warninglabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.warninglabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.warninglabel.Location = new System.Drawing.Point(88, 0);
            this.warninglabel.MaximumSize = new System.Drawing.Size(700, 0);
            this.warninglabel.Name = "warninglabel";
            this.warninglabel.Size = new System.Drawing.Size(700, 200);
            this.warninglabel.TabIndex = 11;
            this.warninglabel.Text = "Missing required columns. Please include the specified columns in your excel file" +
    " before continuing.";
            this.warninglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::WindowsClient.Properties.Resources.WarningLarge;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 194);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(1167, 808);
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.warning.ResumeLayout(false);
            this.warning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Label gridLabel;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox adviceBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TrackerBar tracker;
        private System.Windows.Forms.Button fileBtn;
        private System.Windows.Forms.TableLayoutPanel warning;
        private System.Windows.Forms.Label warninglabel;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
