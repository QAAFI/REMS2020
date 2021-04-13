namespace WindowsClient.Controls
{
    partial class ExperimentDetailer
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
            Query = null;

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
            this.experimentsTree = new System.Windows.Forms.TreeView();
            this.experimentDetails = new System.Windows.Forms.TabControl();
            this.pageSummary = new System.Windows.Forms.TabPage();
            this.pageDesign = new System.Windows.Forms.TabPage();
            this.designBox = new System.Windows.Forms.TextBox();
            this.designData = new System.Windows.Forms.DataGridView();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.pageData = new System.Windows.Forms.TabPage();
            this.pageSoil = new System.Windows.Forms.TabPage();
            this.operations = new WindowsClient.Controls.OperationsChart();
            this.traitChart = new WindowsClient.Controls.TraitChart();
            this.soilsChart = new WindowsClient.Controls.SoilChart();
            this.summariser = new WindowsClient.Controls.Experiments.ExperimentSummariser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.experimentDetails.SuspendLayout();
            this.pageSummary.SuspendLayout();
            this.pageDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).BeginInit();
            this.pageOperation.SuspendLayout();
            this.pageData.SuspendLayout();
            this.pageSoil.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.experimentsTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.experimentDetails);
            this.splitContainer1.Size = new System.Drawing.Size(828, 557);
            this.splitContainer1.SplitterDistance = 173;
            this.splitContainer1.TabIndex = 5;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentsTree.HideSelection = false;
            this.experimentsTree.Location = new System.Drawing.Point(0, 0);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(173, 557);
            this.experimentsTree.TabIndex = 4;
            // 
            // experimentDetails
            // 
            this.experimentDetails.Controls.Add(this.pageSummary);
            this.experimentDetails.Controls.Add(this.pageDesign);
            this.experimentDetails.Controls.Add(this.pageOperation);
            this.experimentDetails.Controls.Add(this.pageData);
            this.experimentDetails.Controls.Add(this.pageSoil);
            this.experimentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentDetails.Location = new System.Drawing.Point(0, 0);
            this.experimentDetails.Name = "experimentDetails";
            this.experimentDetails.SelectedIndex = 0;
            this.experimentDetails.Size = new System.Drawing.Size(651, 557);
            this.experimentDetails.TabIndex = 3;
            // 
            // pageSummary
            // 
            this.pageSummary.BackColor = System.Drawing.Color.LightGray;
            this.pageSummary.Controls.Add(this.summariser);
            this.pageSummary.Location = new System.Drawing.Point(4, 22);
            this.pageSummary.Name = "pageSummary";
            this.pageSummary.Padding = new System.Windows.Forms.Padding(3);
            this.pageSummary.Size = new System.Drawing.Size(643, 531);
            this.pageSummary.TabIndex = 7;
            this.pageSummary.Text = "Summary";
            // 
            // pageDesign
            // 
            this.pageDesign.Controls.Add(this.designBox);
            this.pageDesign.Controls.Add(this.designData);
            this.pageDesign.Location = new System.Drawing.Point(4, 22);
            this.pageDesign.Name = "pageDesign";
            this.pageDesign.Padding = new System.Windows.Forms.Padding(3);
            this.pageDesign.Size = new System.Drawing.Size(643, 531);
            this.pageDesign.TabIndex = 8;
            this.pageDesign.Text = "Design";
            this.pageDesign.UseVisualStyleBackColor = true;
            // 
            // designBox
            // 
            this.designBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designBox.Location = new System.Drawing.Point(0, 0);
            this.designBox.Name = "designBox";
            this.designBox.ReadOnly = true;
            this.designBox.Size = new System.Drawing.Size(637, 20);
            this.designBox.TabIndex = 26;
            // 
            // designData
            // 
            this.designData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designData.Location = new System.Drawing.Point(0, 26);
            this.designData.Name = "designData";
            this.designData.Size = new System.Drawing.Size(637, 502);
            this.designData.TabIndex = 24;
            // 
            // pageOperation
            // 
            this.pageOperation.BackColor = System.Drawing.Color.LightGray;
            this.pageOperation.Controls.Add(this.operations);
            this.pageOperation.Location = new System.Drawing.Point(4, 22);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.pageOperation.Size = new System.Drawing.Size(643, 531);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            // 
            // pageData
            // 
            this.pageData.BackColor = System.Drawing.Color.LightGray;
            this.pageData.Controls.Add(this.traitChart);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(3);
            this.pageData.Size = new System.Drawing.Size(643, 531);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // pageSoil
            // 
            this.pageSoil.Controls.Add(this.soilsChart);
            this.pageSoil.Location = new System.Drawing.Point(4, 22);
            this.pageSoil.Name = "pageSoil";
            this.pageSoil.Padding = new System.Windows.Forms.Padding(3);
            this.pageSoil.Size = new System.Drawing.Size(643, 531);
            this.pageSoil.TabIndex = 9;
            this.pageSoil.Text = "Soil";
            this.pageSoil.UseVisualStyleBackColor = true;
            // 
            // operations
            // 
            this.operations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operations.Location = new System.Drawing.Point(3, 3);
            this.operations.Name = "operations";
            this.operations.Size = new System.Drawing.Size(637, 525);
            this.operations.TabIndex = 0;
            // 
            // traitChart
            // 
            this.traitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traitChart.Location = new System.Drawing.Point(3, 3);
            this.traitChart.Name = "traitChart";
            this.traitChart.Size = new System.Drawing.Size(637, 525);
            this.traitChart.TabIndex = 0;
            // 
            // soilsChart
            // 
            this.soilsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soilsChart.Location = new System.Drawing.Point(3, 3);
            this.soilsChart.Name = "soilsChart";
            this.soilsChart.Size = new System.Drawing.Size(637, 525);
            this.soilsChart.TabIndex = 0;
            // 
            // experimentSummariser1
            // 
            this.summariser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summariser.Location = new System.Drawing.Point(3, 3);
            this.summariser.MinimumSize = new System.Drawing.Size(637, 0);
            this.summariser.Name = "experimentSummariser1";
            this.summariser.Size = new System.Drawing.Size(637, 525);
            this.summariser.TabIndex = 0;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(828, 557);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.experimentDetails.ResumeLayout(false);
            this.pageSummary.ResumeLayout(false);
            this.pageDesign.ResumeLayout(false);
            this.pageDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).EndInit();
            this.pageOperation.ResumeLayout(false);
            this.pageData.ResumeLayout(false);
            this.pageSoil.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView experimentsTree;
        private System.Windows.Forms.TabControl experimentDetails;
        private System.Windows.Forms.TabPage pageDesign;
        private System.Windows.Forms.TextBox designBox;
        private System.Windows.Forms.DataGridView designData;
        private System.Windows.Forms.TabPage pageOperation;
        private System.Windows.Forms.TabPage pageData;
        private TraitChart traitChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private OperationsChart operations;
        private System.Windows.Forms.TabPage pageSoil;
        private SoilChart soilsChart;
        private System.Windows.Forms.TabPage pageSummary;
        private Experiments.ExperimentSummariser summariser;
    }
}
