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
            this.nodeSplitter = new System.Windows.Forms.SplitContainer();
            this.ignoreBox = new System.Windows.Forms.CheckBox();
            this.isTraitBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.propertiesBox = new System.Windows.Forms.ComboBox();
            this.importData = new System.Windows.Forms.DataGridView();
            this.columnLabel = new System.Windows.Forms.Label();
            this.stateBox = new System.Windows.Forms.PictureBox();
            this.dataTree = new System.Windows.Forms.TreeView();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.fileBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).BeginInit();
            this.nodeSplitter.Panel1.SuspendLayout();
            this.nodeSplitter.Panel2.SuspendLayout();
            this.nodeSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.importData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).BeginInit();
            this.SuspendLayout();
            // 
            // nodeSplitter
            // 
            this.nodeSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nodeSplitter.Location = new System.Drawing.Point(165, 30);
            this.nodeSplitter.Name = "nodeSplitter";
            // 
            // nodeSplitter.Panel1
            // 
            this.nodeSplitter.Panel1.Controls.Add(this.ignoreBox);
            // 
            // nodeSplitter.Panel2
            // 
            this.nodeSplitter.Panel2.Controls.Add(this.isTraitBox);
            this.nodeSplitter.Panel2.Controls.Add(this.label2);
            this.nodeSplitter.Panel2.Controls.Add(this.propertiesBox);
            this.nodeSplitter.Panel2Collapsed = true;
            this.nodeSplitter.Size = new System.Drawing.Size(487, 25);
            this.nodeSplitter.SplitterDistance = 62;
            this.nodeSplitter.TabIndex = 9;
            // 
            // ignoreBox
            // 
            this.ignoreBox.AutoSize = true;
            this.ignoreBox.Location = new System.Drawing.Point(3, 4);
            this.ignoreBox.Name = "ignoreBox";
            this.ignoreBox.Size = new System.Drawing.Size(59, 17);
            this.ignoreBox.TabIndex = 0;
            this.ignoreBox.Text = "Ignore ";
            this.ignoreBox.UseVisualStyleBackColor = true;
            this.ignoreBox.CheckedChanged += new System.EventHandler(this.IgnoreBoxCheckChanged);
            // 
            // isTraitBox
            // 
            this.isTraitBox.AutoSize = true;
            this.isTraitBox.Location = new System.Drawing.Point(3, 4);
            this.isTraitBox.Name = "isTraitBox";
            this.isTraitBox.Size = new System.Drawing.Size(58, 17);
            this.isTraitBox.TabIndex = 2;
            this.isTraitBox.Text = "Is Trait";
            this.isTraitBox.UseVisualStyleBackColor = true;
            this.isTraitBox.CheckedChanged += new System.EventHandler(this.IsTraitBoxCheckChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 5);
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
            this.propertiesBox.Location = new System.Drawing.Point(198, 2);
            this.propertiesBox.Name = "propertiesBox";
            this.propertiesBox.Size = new System.Drawing.Size(220, 21);
            this.propertiesBox.TabIndex = 1;
            this.propertiesBox.SelectedIndexChanged += new System.EventHandler(this.PropertiesSelectionChanged);
            // 
            // importData
            // 
            this.importData.AllowUserToAddRows = false;
            this.importData.AllowUserToDeleteRows = false;
            this.importData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.importData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.importData.Location = new System.Drawing.Point(165, 57);
            this.importData.Name = "importData";
            this.importData.Size = new System.Drawing.Size(487, 528);
            this.importData.TabIndex = 0;
            // 
            // columnLabel
            // 
            this.columnLabel.AutoSize = true;
            this.columnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.columnLabel.Location = new System.Drawing.Point(29, 32);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(44, 17);
            this.columnLabel.TabIndex = 3;
            this.columnLabel.Text = "         ";
            // 
            // stateBox
            // 
            this.stateBox.Location = new System.Drawing.Point(3, 32);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(16, 16);
            this.stateBox.TabIndex = 2;
            this.stateBox.TabStop = false;
            // 
            // dataTree
            // 
            this.dataTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataTree.HideSelection = false;
            this.dataTree.Location = new System.Drawing.Point(3, 56);
            this.dataTree.Name = "dataTree";
            this.dataTree.Size = new System.Drawing.Size(156, 529);
            this.dataTree.TabIndex = 8;
            this.dataTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeAfterSelect);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(84, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 7;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.OnImportClicked);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 3);
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
            this.fileBox.Location = new System.Drawing.Point(165, 4);
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(487, 20);
            this.fileBox.TabIndex = 10;
            // 
            // Importer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.importData);
            this.Controls.Add(this.fileBox);
            this.Controls.Add(this.stateBox);
            this.Controls.Add(this.columnLabel);
            this.Controls.Add(this.nodeSplitter);
            this.Controls.Add(this.dataTree);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnLoad);
            this.Name = "Importer";
            this.Size = new System.Drawing.Size(655, 588);
            this.nodeSplitter.Panel1.ResumeLayout(false);
            this.nodeSplitter.Panel1.PerformLayout();
            this.nodeSplitter.Panel2.ResumeLayout(false);
            this.nodeSplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSplitter)).EndInit();
            this.nodeSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.importData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer nodeSplitter;
        private System.Windows.Forms.DataGridView importData;
        private System.Windows.Forms.TreeView dataTree;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ComboBox propertiesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox stateBox;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.TextBox fileBox;
        private System.Windows.Forms.CheckBox ignoreBox;
        private System.Windows.Forms.CheckBox isTraitBox;
    }
}
