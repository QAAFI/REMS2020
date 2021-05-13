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
            this.summariser = new WindowsClient.Controls.Experiments.ExperimentSummariser();
            this.pageDesign = new System.Windows.Forms.TabPage();
            this.designBox = new System.Windows.Forms.TextBox();
            this.designData = new System.Windows.Forms.DataGridView();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.operations = new WindowsClient.Controls.OperationsChart();
            this.pageData = new System.Windows.Forms.TabPage();
            this.traitChart = new WindowsClient.Controls.TraitChart();
            this.pageSoil = new System.Windows.Forms.TabPage();
            this.soilsChart = new WindowsClient.Controls.SoilChart();
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
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.experimentsTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.experimentDetails);
            this.splitContainer1.Size = new System.Drawing.Size(966, 643);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentsTree.HideSelection = false;
            this.experimentsTree.Location = new System.Drawing.Point(0, 0);
            this.experimentsTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(201, 643);
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
            this.experimentDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.experimentDetails.Name = "experimentDetails";
            this.experimentDetails.SelectedIndex = 0;
            this.experimentDetails.Size = new System.Drawing.Size(760, 643);
            this.experimentDetails.TabIndex = 3;
            // 
            // pageSummary
            // 
            this.pageSummary.BackColor = System.Drawing.Color.LightGray;
            this.pageSummary.Controls.Add(this.summariser);
            this.pageSummary.Location = new System.Drawing.Point(4, 24);
            this.pageSummary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSummary.Name = "pageSummary";
            this.pageSummary.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSummary.Size = new System.Drawing.Size(752, 615);
            this.pageSummary.TabIndex = 7;
            this.pageSummary.Text = "Summary";
            // 
            // summariser
            // 
            this.summariser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summariser.Location = new System.Drawing.Point(4, 3);
            this.summariser.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.summariser.MinimumSize = new System.Drawing.Size(743, 0);
            this.summariser.Name = "summariser";
            this.summariser.Size = new System.Drawing.Size(744, 609);
            this.summariser.TabIndex = 0;
            // 
            // pageDesign
            // 
            this.pageDesign.Controls.Add(this.designBox);
            this.pageDesign.Controls.Add(this.designData);
            this.pageDesign.Location = new System.Drawing.Point(4, 24);
            this.pageDesign.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageDesign.Name = "pageDesign";
            this.pageDesign.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageDesign.Size = new System.Drawing.Size(752, 615);
            this.pageDesign.TabIndex = 8;
            this.pageDesign.Text = "Design";
            this.pageDesign.UseVisualStyleBackColor = true;
            // 
            // designBox
            // 
            this.designBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designBox.Location = new System.Drawing.Point(0, 0);
            this.designBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.designBox.Name = "designBox";
            this.designBox.ReadOnly = true;
            this.designBox.Size = new System.Drawing.Size(744, 23);
            this.designBox.TabIndex = 26;
            // 
            // designData
            // 
            this.designData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designData.Location = new System.Drawing.Point(0, 30);
            this.designData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.designData.Name = "designData";
            this.designData.Size = new System.Drawing.Size(745, 581);
            this.designData.TabIndex = 24;
            // 
            // pageOperation
            // 
            this.pageOperation.BackColor = System.Drawing.Color.LightGray;
            this.pageOperation.Controls.Add(this.operations);
            this.pageOperation.Location = new System.Drawing.Point(4, 24);
            this.pageOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageOperation.Size = new System.Drawing.Size(752, 615);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            // 
            // operations
            // 
            this.operations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operations.Location = new System.Drawing.Point(4, 3);
            this.operations.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.operations.Name = "operations";
            this.operations.Size = new System.Drawing.Size(744, 609);
            this.operations.TabIndex = 0;
            // 
            // pageData
            // 
            this.pageData.BackColor = System.Drawing.Color.LightGray;
            this.pageData.Controls.Add(this.traitChart);
            this.pageData.Location = new System.Drawing.Point(4, 24);
            this.pageData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageData.Size = new System.Drawing.Size(752, 615);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // traitChart
            // 
            this.traitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traitChart.Location = new System.Drawing.Point(4, 3);
            this.traitChart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.traitChart.Name = "traitChart";
            this.traitChart.Size = new System.Drawing.Size(744, 609);
            this.traitChart.TabIndex = 0;
            // 
            // pageSoil
            // 
            this.pageSoil.Controls.Add(this.soilsChart);
            this.pageSoil.Location = new System.Drawing.Point(4, 24);
            this.pageSoil.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSoil.Name = "pageSoil";
            this.pageSoil.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSoil.Size = new System.Drawing.Size(752, 615);
            this.pageSoil.TabIndex = 9;
            this.pageSoil.Text = "Soil";
            this.pageSoil.UseVisualStyleBackColor = true;
            // 
            // soilsChart
            // 
            this.soilsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.soilsChart.Location = new System.Drawing.Point(4, 3);
            this.soilsChart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.soilsChart.Name = "soilsChart";
            this.soilsChart.Size = new System.Drawing.Size(744, 609);
            this.soilsChart.TabIndex = 0;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(966, 643);
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
