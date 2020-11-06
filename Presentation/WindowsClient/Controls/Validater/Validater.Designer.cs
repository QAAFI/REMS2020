namespace WindowsClient.Controls
{
    partial class Validater
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AltsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ignore = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ignoreBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.AltsColumn,
            this.Ignore});
            this.dataGrid.Location = new System.Drawing.Point(3, 3);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(564, 526);
            this.dataGrid.TabIndex = 0;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 60;
            // 
            // AltsColumn
            // 
            this.AltsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AltsColumn.HeaderText = "Alternatives";
            this.AltsColumn.Name = "AltsColumn";
            // 
            // Ignore
            // 
            this.Ignore.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ignore.FalseValue = "F";
            this.Ignore.HeaderText = "Ignore";
            this.Ignore.IndeterminateValue = "F";
            this.Ignore.Name = "Ignore";
            this.Ignore.TrueValue = "T";
            this.Ignore.Width = 60;
            // 
            // ignoreBox
            // 
            this.ignoreBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ignoreBox.AutoSize = true;
            this.ignoreBox.BackColor = System.Drawing.SystemColors.Window;
            this.ignoreBox.Location = new System.Drawing.Point(547, 8);
            this.ignoreBox.Name = "ignoreBox";
            this.ignoreBox.Size = new System.Drawing.Size(15, 14);
            this.ignoreBox.TabIndex = 1;
            this.ignoreBox.UseVisualStyleBackColor = false;
            this.ignoreBox.CheckedChanged += new System.EventHandler(this.ignoreBoxChecked);
            // 
            // Validater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ignoreBox);
            this.Controls.Add(this.dataGrid);
            this.Name = "Validater";
            this.Size = new System.Drawing.Size(570, 532);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AltsColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Ignore;
        private System.Windows.Forms.CheckBox ignoreBox;
    }
}
