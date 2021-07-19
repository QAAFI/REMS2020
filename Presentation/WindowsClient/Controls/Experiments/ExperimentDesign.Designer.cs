
namespace WindowsClient.Controls
{
    partial class ExperimentDesign
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.designGrid = new System.Windows.Forms.DataGridView();
            this.plotsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.designGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.plotsGrid);
            this.splitContainer1.Size = new System.Drawing.Size(767, 722);
            this.splitContainer1.SplitterDistance = 379;
            this.splitContainer1.TabIndex = 0;
            // 
            // designGrid
            // 
            this.designGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designGrid.Location = new System.Drawing.Point(0, 0);
            this.designGrid.MultiSelect = false;
            this.designGrid.Name = "designGrid";
            this.designGrid.ReadOnly = true;
            this.designGrid.RowTemplate.Height = 25;
            this.designGrid.Size = new System.Drawing.Size(379, 722);
            this.designGrid.TabIndex = 0;
            // 
            // plotsGrid
            // 
            this.plotsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.plotsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotsGrid.Location = new System.Drawing.Point(0, 0);
            this.plotsGrid.Name = "plotsGrid";
            this.plotsGrid.ReadOnly = true;
            this.plotsGrid.RowTemplate.Height = 25;
            this.plotsGrid.Size = new System.Drawing.Size(384, 722);
            this.plotsGrid.TabIndex = 0;
            // 
            // ExperimentDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExperimentDesign";
            this.Size = new System.Drawing.Size(767, 722);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.designGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plotsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView designGrid;
        private System.Windows.Forms.DataGridView plotsGrid;
    }
}
