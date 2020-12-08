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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.columnLabel = new System.Windows.Forms.Label();
            this.importData = new System.Windows.Forms.DataGridView();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.adviceBox = new System.Windows.Forms.RichTextBox();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tracker = new WindowsClient.Controls.TrackerBar();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnLabel
            // 
            this.columnLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnLabel.Location = new System.Drawing.Point(3, 7);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(548, 17);
            this.columnLabel.TabIndex = 10;
            this.columnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Format = "N3";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.importData.DefaultCellStyle = dataGridViewCellStyle1;
            this.importData.Location = new System.Drawing.Point(3, 29);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(548, 538);
            this.importData.TabIndex = 0;
            // 
            // fileBox
            // 
            this.fileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileBox.Enabled = false;
            this.fileBox.Location = new System.Drawing.Point(4, 3);
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(216, 20);
            this.fileBox.TabIndex = 10;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.columnLabel);
            this.splitter.Panel2.Controls.Add(this.importData);
            this.splitter.Size = new System.Drawing.Size(781, 570);
            this.splitter.SplitterDistance = 223;
            this.splitter.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tracker);
            this.panel1.Controls.Add(this.adviceBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 190);
            this.panel1.TabIndex = 13;
            // 
            // adviceBox
            // 
            this.adviceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.adviceBox.Location = new System.Drawing.Point(3, 3);
            this.adviceBox.Name = "adviceBox";
            this.adviceBox.ReadOnly = true;
            this.adviceBox.Size = new System.Drawing.Size(217, 123);
            this.adviceBox.TabIndex = 13;
            this.adviceBox.Text = "";
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTree.HideSelection = false;
            this.dataTree.Location = new System.Drawing.Point(4, 29);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(216, 344);
            this.dataTree.TabIndex = 12;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataTree);
            this.splitContainer1.Panel1.Controls.Add(this.fileBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(223, 570);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 14;
            // 
            // tracker
            // 
            this.tracker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracker.Location = new System.Drawing.Point(4, 124);
            this.tracker.Name = "tracker";
            this.tracker.Size = new System.Drawing.Size(216, 63);
            this.tracker.TabIndex = 14;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(781, 570);
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox adviceBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TrackerBar tracker;
    }
}
