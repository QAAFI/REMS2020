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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.columnLabel = new System.Windows.Forms.Label();
            this.stateBox = new System.Windows.Forms.PictureBox();
            this.importData = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = true;
            this.columnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnLabel.Location = new System.Drawing.Point(200, 7);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(44, 17);
            this.columnLabel.TabIndex = 10;
            this.columnLabel.Text = "         ";
            // 
            // stateBox
            // 
            this.stateBox.Location = new System.Drawing.Point(178, 7);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(16, 16);
            this.stateBox.TabIndex = 9;
            this.stateBox.TabStop = false;
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.importData.DefaultCellStyle = dataGridViewCellStyle2;
            this.importData.Location = new System.Drawing.Point(3, 29);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(451, 556);
            this.importData.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 29);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(91, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Open";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.OnLoadClicked);
            // 
            // fileBox
            // 
            this.fileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileBox.Location = new System.Drawing.Point(3, 3);
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(188, 20);
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
            this.splitter.Panel1.Controls.Add(this.dataTree);
            this.splitter.Panel1.Controls.Add(this.btnImport);
            this.splitter.Panel1.Controls.Add(this.btnLoad);
            this.splitter.Panel1.Controls.Add(this.fileBox);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.columnLabel);
            this.splitter.Panel2.Controls.Add(this.stateBox);
            this.splitter.Panel2.Controls.Add(this.importData);
            this.splitter.Size = new System.Drawing.Size(655, 588);
            this.splitter.SplitterDistance = 194;
            this.splitter.TabIndex = 13;
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTree.HideSelection = false;
            this.dataTree.Location = new System.Drawing.Point(3, 59);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(188, 526);
            this.dataTree.TabIndex = 12;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(100, 29);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(91, 23);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.OnImportClicked);
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitter);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(655, 588);
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel1.PerformLayout();
            this.splitter.Panel2.ResumeLayout(false);
            this.splitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.PictureBox stateBox;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Button btnImport;
    }
}
