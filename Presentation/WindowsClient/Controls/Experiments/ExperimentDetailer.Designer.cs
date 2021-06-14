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
            this.container = new System.Windows.Forms.SplitContainer();
            this.experimentsTree = new System.Windows.Forms.TreeView();
            this.experimentDetails = new System.Windows.Forms.TabControl();
            this.pageSummary = new System.Windows.Forms.TabPage();
            this.summariser = new WindowsClient.Controls.ExperimentSummariser();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.operations = new WindowsClient.Controls.OperationsChart();
            this.pageData = new System.Windows.Forms.TabPage();
            this.traitChart = new WindowsClient.Controls.TraitChart();
            this.pageSoil = new System.Windows.Forms.TabPage();
            this.soilsChart = new WindowsClient.Controls.SoilChart();
            ((System.ComponentModel.ISupportInitialize)(this.container)).BeginInit();
            this.container.Panel1.SuspendLayout();
            this.container.Panel2.SuspendLayout();
            this.container.SuspendLayout();
            this.experimentDetails.SuspendLayout();
            this.pageSummary.SuspendLayout();
            this.pageOperation.SuspendLayout();
            this.pageData.SuspendLayout();
            this.pageSoil.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.container.Name = "container";
            // 
            // container.Panel1
            // 
            this.container.Panel1.Controls.Add(this.experimentsTree);
            // 
            // container.Panel2
            // 
            this.container.Panel2.Controls.Add(this.experimentDetails);
            this.container.Size = new System.Drawing.Size(966, 643);
            this.container.SplitterDistance = 201;
            this.container.SplitterWidth = 5;
            this.container.TabIndex = 5;
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
            // pageOperation
            // 
            this.pageOperation.BackColor = System.Drawing.Color.LightGray;
            this.pageOperation.Controls.Add(this.operations);
            this.pageOperation.Location = new System.Drawing.Point(4, 24);
            this.pageOperation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageOperation.Size = new System.Drawing.Size(660, 468);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            // 
            // operations
            // 
            this.operations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operations.Location = new System.Drawing.Point(4, 3);
            this.operations.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.operations.Name = "operations";
            this.operations.Size = new System.Drawing.Size(652, 462);
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
            this.pageData.Size = new System.Drawing.Size(660, 468);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // traitChart
            // 
            this.traitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traitChart.Location = new System.Drawing.Point(4, 3);
            this.traitChart.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.traitChart.Name = "traitChart";
            this.traitChart.Size = new System.Drawing.Size(652, 462);
            this.traitChart.TabIndex = 0;
            // 
            // pageSoil
            // 
            this.pageSoil.Controls.Add(this.soilsChart);
            this.pageSoil.Location = new System.Drawing.Point(4, 24);
            this.pageSoil.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSoil.Name = "pageSoil";
            this.pageSoil.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSoil.Size = new System.Drawing.Size(660, 468);
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
            this.soilsChart.Size = new System.Drawing.Size(652, 462);
            this.soilsChart.TabIndex = 0;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(966, 643);
            this.container.Panel1.ResumeLayout(false);
            this.container.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.container)).EndInit();
            this.container.ResumeLayout(false);
            this.experimentDetails.ResumeLayout(false);
            this.pageSummary.ResumeLayout(false);
            this.pageOperation.ResumeLayout(false);
            this.pageData.ResumeLayout(false);
            this.pageSoil.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView experimentsTree;
        private System.Windows.Forms.TabControl experimentDetails;
        private System.Windows.Forms.TabPage pageOperation;
        private System.Windows.Forms.TabPage pageData;
        private TraitChart traitChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private OperationsChart operations;
        private System.Windows.Forms.TabPage pageSoil;
        private SoilChart soilsChart;
        private System.Windows.Forms.TabPage pageSummary;
        private ExperimentSummariser summariser;
        private System.Windows.Forms.SplitContainer container;
    }
}
