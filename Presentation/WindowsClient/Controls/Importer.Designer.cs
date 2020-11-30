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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nodeSplitter = new System.Windows.Forms.SplitContainer();
            this.columnLabel = new System.Windows.Forms.Label();
            this.stateBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.propertiesBox = new System.Windows.Forms.ComboBox();
            this.ignoreBox = new System.Windows.Forms.CheckBox();
            this.isTraitBox = new System.Windows.Forms.CheckBox();
            this.importData = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.btnImport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).BeginInit();
            this.nodeSplitter.Panel1.SuspendLayout();
            this.nodeSplitter.Panel2.SuspendLayout();
            this.nodeSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nodeSplitter
            // 
            this.nodeSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeSplitter.Location = new System.Drawing.Point(3, 27);
            this.nodeSplitter.Name = "nodeSplitter";
            // 
            // nodeSplitter.Panel1
            // 
            this.nodeSplitter.Panel1.Controls.Add(this.columnLabel);
            this.nodeSplitter.Panel1.Controls.Add(this.stateBox);
            // 
            // nodeSplitter.Panel2
            // 
            this.nodeSplitter.Panel2.Controls.Add(this.label2);
            this.nodeSplitter.Panel2.Controls.Add(this.propertiesBox);
            this.nodeSplitter.Size = new System.Drawing.Size(485, 31);
            this.nodeSplitter.SplitterDistance = 157;
            this.nodeSplitter.TabIndex = 9;
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = true;
            this.columnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnLabel.Location = new System.Drawing.Point(26, 6);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(44, 17);
            this.columnLabel.TabIndex = 10;
            this.columnLabel.Text = "         ";
            // 
            // stateBox
            // 
            this.stateBox.Location = new System.Drawing.Point(4, 6);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(16, 16);
            this.stateBox.TabIndex = 9;
            this.stateBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Property:";
            // 
            // propertiesBox
            // 
            this.propertiesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.propertiesBox.Enabled = false;
            this.propertiesBox.FormattingEnabled = true;
            this.propertiesBox.Location = new System.Drawing.Point(58, 5);
            this.propertiesBox.Name = "propertiesBox";
            this.propertiesBox.Size = new System.Drawing.Size(223, 21);
            this.propertiesBox.TabIndex = 1;
            this.propertiesBox.SelectedIndexChanged += new System.EventHandler(this.PropertiesSelectionChanged);
            // 
            // ignoreBox
            // 
            this.ignoreBox.AutoSize = true;
            this.ignoreBox.Location = new System.Drawing.Point(6, 3);
            this.ignoreBox.Name = "ignoreBox";
            this.ignoreBox.Size = new System.Drawing.Size(59, 17);
            this.ignoreBox.TabIndex = 0;
            this.ignoreBox.Text = "Ignore ";
            this.ignoreBox.UseVisualStyleBackColor = true;
            this.ignoreBox.CheckedChanged += new System.EventHandler(this.IgnoreBoxCheckChanged);
            // 
            // isTraitBox
            // 
            this.isTraitBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.isTraitBox.AutoSize = true;
            this.isTraitBox.Location = new System.Drawing.Point(93, 3);
            this.isTraitBox.Name = "isTraitBox";
            this.isTraitBox.Size = new System.Drawing.Size(58, 17);
            this.isTraitBox.TabIndex = 2;
            this.isTraitBox.Text = "Is Trait";
            this.isTraitBox.UseVisualStyleBackColor = true;
            this.isTraitBox.CheckedChanged += new System.EventHandler(this.IsTraitBoxCheckChanged);
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.importData.DefaultCellStyle = dataGridViewCellStyle3;
            this.importData.Location = new System.Drawing.Point(3, 59);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(485, 526);
            this.importData.TabIndex = 0;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(413, 1);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
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
            this.fileBox.Size = new System.Drawing.Size(404, 20);
            this.fileBox.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.isTraitBox);
            this.splitContainer1.Panel1.Controls.Add(this.ignoreBox);
            this.splitContainer1.Panel1.Controls.Add(this.dataTree);
            this.splitContainer1.Panel1.Controls.Add(this.btnImport);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.importData);
            this.splitContainer1.Panel2.Controls.Add(this.nodeSplitter);
            this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
            this.splitContainer1.Panel2.Controls.Add(this.fileBox);
            this.splitContainer1.Size = new System.Drawing.Size(655, 588);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 13;
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataTree.HideSelection = false;
            this.dataTree.Location = new System.Drawing.Point(6, 26);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(148, 530);
            this.dataTree.TabIndex = 12;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(6, 562);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(148, 23);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.OnImportClicked);
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(655, 588);
            this.nodeSplitter.Panel1.ResumeLayout(false);
            this.nodeSplitter.Panel1.PerformLayout();
            this.nodeSplitter.Panel2.ResumeLayout(false);
            this.nodeSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).EndInit();
            this.nodeSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer nodeSplitter;
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox propertiesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.CheckBox ignoreBox;
        private System.Windows.Forms.CheckBox isTraitBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.PictureBox stateBox;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Button btnImport;
    }
}
