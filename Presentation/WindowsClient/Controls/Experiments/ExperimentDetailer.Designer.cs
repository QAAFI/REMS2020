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
            this.sowingPanel = new System.Windows.Forms.Panel();
            this.sowingLabel = new System.Windows.Forms.Label();
            this.sowingMethodBox = new WindowsClient.Controls.LabeledBox();
            this.sowingPopBox = new WindowsClient.Controls.LabeledBox();
            this.sowingDateBox = new WindowsClient.Controls.LabeledBox();
            this.sowingRowBox = new WindowsClient.Controls.LabeledBox();
            this.sowingDepthBox = new WindowsClient.Controls.LabeledBox();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.researchersLabel = new System.Windows.Forms.Label();
            this.researchersBox = new System.Windows.Forms.ListBox();
            this.notesLabel = new System.Windows.Forms.Label();
            this.ratingBox = new WindowsClient.Controls.LabeledBox();
            this.repsBox = new WindowsClient.Controls.LabeledBox();
            this.endBox = new WindowsClient.Controls.LabeledBox();
            this.startBox = new WindowsClient.Controls.LabeledBox();
            this.metBox = new WindowsClient.Controls.LabeledBox();
            this.fieldBox = new WindowsClient.Controls.LabeledBox();
            this.cropBox = new WindowsClient.Controls.LabeledBox();
            this.experimentLabel = new System.Windows.Forms.Label();
            this.notesBox = new System.Windows.Forms.RichTextBox();
            this.pageDesign = new System.Windows.Forms.TabPage();
            this.designBox = new System.Windows.Forms.TextBox();
            this.designData = new System.Windows.Forms.DataGridView();
            this.pageOperation = new System.Windows.Forms.TabPage();
            this.pageData = new System.Windows.Forms.TabPage();
            this.traitChart = new WindowsClient.Controls.TraitChart();
            this.operations = new WindowsClient.Controls.OperationsChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.experimentDetails.SuspendLayout();
            this.pageSummary.SuspendLayout();
            this.sowingPanel.SuspendLayout();
            this.pageDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).BeginInit();
            this.pageOperation.SuspendLayout();
            this.pageData.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(700, 615);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 5;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentsTree.HideSelection = false;
            this.experimentsTree.Location = new System.Drawing.Point(0, 0);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(147, 615);
            this.experimentsTree.TabIndex = 4;
            // 
            // experimentDetails
            // 
            this.experimentDetails.Controls.Add(this.pageSummary);
            this.experimentDetails.Controls.Add(this.pageDesign);
            this.experimentDetails.Controls.Add(this.pageOperation);
            this.experimentDetails.Controls.Add(this.pageData);
            this.experimentDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentDetails.Location = new System.Drawing.Point(0, 0);
            this.experimentDetails.Name = "experimentDetails";
            this.experimentDetails.SelectedIndex = 0;
            this.experimentDetails.Size = new System.Drawing.Size(549, 615);
            this.experimentDetails.TabIndex = 3;
            // 
            // pageSummary
            // 
            this.pageSummary.BackColor = System.Drawing.Color.LightGray;
            this.pageSummary.Controls.Add(this.sowingPanel);
            this.pageSummary.Controls.Add(this.descriptionBox);
            this.pageSummary.Controls.Add(this.researchersLabel);
            this.pageSummary.Controls.Add(this.researchersBox);
            this.pageSummary.Controls.Add(this.notesLabel);
            this.pageSummary.Controls.Add(this.ratingBox);
            this.pageSummary.Controls.Add(this.repsBox);
            this.pageSummary.Controls.Add(this.endBox);
            this.pageSummary.Controls.Add(this.startBox);
            this.pageSummary.Controls.Add(this.metBox);
            this.pageSummary.Controls.Add(this.fieldBox);
            this.pageSummary.Controls.Add(this.cropBox);
            this.pageSummary.Controls.Add(this.experimentLabel);
            this.pageSummary.Controls.Add(this.notesBox);
            this.pageSummary.Location = new System.Drawing.Point(4, 22);
            this.pageSummary.Name = "pageSummary";
            this.pageSummary.Padding = new System.Windows.Forms.Padding(3);
            this.pageSummary.Size = new System.Drawing.Size(541, 589);
            this.pageSummary.TabIndex = 7;
            this.pageSummary.Text = "Summary";
            // 
            // sowingPanel
            // 
            this.sowingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sowingPanel.BackColor = System.Drawing.Color.Silver;
            this.sowingPanel.Controls.Add(this.sowingLabel);
            this.sowingPanel.Controls.Add(this.sowingMethodBox);
            this.sowingPanel.Controls.Add(this.sowingPopBox);
            this.sowingPanel.Controls.Add(this.sowingDateBox);
            this.sowingPanel.Controls.Add(this.sowingRowBox);
            this.sowingPanel.Controls.Add(this.sowingDepthBox);
            this.sowingPanel.Location = new System.Drawing.Point(10, 347);
            this.sowingPanel.Name = "sowingPanel";
            this.sowingPanel.Size = new System.Drawing.Size(368, 236);
            this.sowingPanel.TabIndex = 21;
            // 
            // sowingLabel
            // 
            this.sowingLabel.AutoSize = true;
            this.sowingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sowingLabel.Location = new System.Drawing.Point(133, 12);
            this.sowingLabel.Name = "sowingLabel";
            this.sowingLabel.Size = new System.Drawing.Size(109, 17);
            this.sowingLabel.TabIndex = 22;
            this.sowingLabel.Text = "     Sowing     ";
            // 
            // sowingMethodBox
            // 
            this.sowingMethodBox.Content = "";
            this.sowingMethodBox.Label = "Method:";
            this.sowingMethodBox.Location = new System.Drawing.Point(4, 32);
            this.sowingMethodBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingMethodBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingMethodBox.Name = "sowingMethodBox";
            this.sowingMethodBox.ReadOnly = true;
            this.sowingMethodBox.Size = new System.Drawing.Size(361, 36);
            this.sowingMethodBox.TabIndex = 20;
            // 
            // sowingPopBox
            // 
            this.sowingPopBox.Content = "";
            this.sowingPopBox.Label = "Population:";
            this.sowingPopBox.Location = new System.Drawing.Point(4, 195);
            this.sowingPopBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingPopBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingPopBox.Name = "sowingPopBox";
            this.sowingPopBox.ReadOnly = true;
            this.sowingPopBox.Size = new System.Drawing.Size(361, 36);
            this.sowingPopBox.TabIndex = 19;
            // 
            // sowingDateBox
            // 
            this.sowingDateBox.Content = "";
            this.sowingDateBox.Label = "Date:";
            this.sowingDateBox.Location = new System.Drawing.Point(4, 74);
            this.sowingDateBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingDateBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingDateBox.Name = "sowingDateBox";
            this.sowingDateBox.ReadOnly = true;
            this.sowingDateBox.Size = new System.Drawing.Size(361, 36);
            this.sowingDateBox.TabIndex = 16;
            // 
            // sowingRowBox
            // 
            this.sowingRowBox.Content = "";
            this.sowingRowBox.Label = "Row space:";
            this.sowingRowBox.Location = new System.Drawing.Point(3, 158);
            this.sowingRowBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingRowBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingRowBox.Name = "sowingRowBox";
            this.sowingRowBox.ReadOnly = true;
            this.sowingRowBox.Size = new System.Drawing.Size(362, 36);
            this.sowingRowBox.TabIndex = 18;
            // 
            // sowingDepthBox
            // 
            this.sowingDepthBox.Content = "";
            this.sowingDepthBox.Label = "Depth:";
            this.sowingDepthBox.Location = new System.Drawing.Point(4, 116);
            this.sowingDepthBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingDepthBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingDepthBox.Name = "sowingDepthBox";
            this.sowingDepthBox.ReadOnly = true;
            this.sowingDepthBox.Size = new System.Drawing.Size(361, 36);
            this.sowingDepthBox.TabIndex = 17;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionBox.Location = new System.Drawing.Point(6, 30);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(530, 20);
            this.descriptionBox.TabIndex = 15;
            // 
            // researchersLabel
            // 
            this.researchersLabel.AutoSize = true;
            this.researchersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.researchersLabel.Location = new System.Drawing.Point(384, 68);
            this.researchersLabel.Name = "researchersLabel";
            this.researchersLabel.Size = new System.Drawing.Size(105, 17);
            this.researchersLabel.TabIndex = 14;
            this.researchersLabel.Text = "Researchers:";
            // 
            // researchersBox
            // 
            this.researchersBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.researchersBox.FormattingEnabled = true;
            this.researchersBox.IntegralHeight = false;
            this.researchersBox.Location = new System.Drawing.Point(384, 88);
            this.researchersBox.Name = "researchersBox";
            this.researchersBox.Size = new System.Drawing.Size(152, 126);
            this.researchersBox.TabIndex = 13;
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesLabel.Location = new System.Drawing.Point(381, 217);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(55, 17);
            this.notesLabel.TabIndex = 12;
            this.notesLabel.Text = "Notes:";
            // 
            // ratingBox
            // 
            this.ratingBox.Content = "";
            this.ratingBox.Label = "Rating:";
            this.ratingBox.Location = new System.Drawing.Point(6, 220);
            this.ratingBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.ratingBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.ratingBox.Name = "ratingBox";
            this.ratingBox.ReadOnly = true;
            this.ratingBox.Size = new System.Drawing.Size(372, 36);
            this.ratingBox.TabIndex = 10;
            // 
            // repsBox
            // 
            this.repsBox.Content = "";
            this.repsBox.Label = "Replicates:";
            this.repsBox.Location = new System.Drawing.Point(6, 178);
            this.repsBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.repsBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.repsBox.Name = "repsBox";
            this.repsBox.ReadOnly = true;
            this.repsBox.Size = new System.Drawing.Size(372, 36);
            this.repsBox.TabIndex = 9;
            // 
            // endBox
            // 
            this.endBox.Content = "";
            this.endBox.Label = "End date:";
            this.endBox.Location = new System.Drawing.Point(6, 305);
            this.endBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.endBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.endBox.Name = "endBox";
            this.endBox.ReadOnly = true;
            this.endBox.Size = new System.Drawing.Size(372, 36);
            this.endBox.TabIndex = 8;
            // 
            // startBox
            // 
            this.startBox.Content = "";
            this.startBox.Label = "Start date:";
            this.startBox.Location = new System.Drawing.Point(6, 262);
            this.startBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.startBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.startBox.Name = "startBox";
            this.startBox.ReadOnly = true;
            this.startBox.Size = new System.Drawing.Size(372, 36);
            this.startBox.TabIndex = 7;
            // 
            // metBox
            // 
            this.metBox.Content = "";
            this.metBox.Label = "Met Station:";
            this.metBox.Location = new System.Drawing.Point(6, 136);
            this.metBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.metBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.metBox.Name = "metBox";
            this.metBox.ReadOnly = true;
            this.metBox.Size = new System.Drawing.Size(372, 36);
            this.metBox.TabIndex = 6;
            // 
            // fieldBox
            // 
            this.fieldBox.Content = "";
            this.fieldBox.Label = "Field:";
            this.fieldBox.Location = new System.Drawing.Point(6, 94);
            this.fieldBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.fieldBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.fieldBox.Name = "fieldBox";
            this.fieldBox.ReadOnly = true;
            this.fieldBox.Size = new System.Drawing.Size(372, 36);
            this.fieldBox.TabIndex = 5;
            // 
            // cropBox
            // 
            this.cropBox.Content = "";
            this.cropBox.Label = "Crop:";
            this.cropBox.Location = new System.Drawing.Point(6, 56);
            this.cropBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.cropBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.cropBox.Name = "cropBox";
            this.cropBox.ReadOnly = true;
            this.cropBox.Size = new System.Drawing.Size(372, 36);
            this.cropBox.TabIndex = 4;
            // 
            // experimentLabel
            // 
            this.experimentLabel.AutoSize = true;
            this.experimentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.experimentLabel.Location = new System.Drawing.Point(6, 3);
            this.experimentLabel.Name = "experimentLabel";
            this.experimentLabel.Size = new System.Drawing.Size(123, 24);
            this.experimentLabel.TabIndex = 2;
            this.experimentLabel.Text = "Experiment:";
            // 
            // notesBox
            // 
            this.notesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notesBox.Location = new System.Drawing.Point(384, 235);
            this.notesBox.Name = "notesBox";
            this.notesBox.ReadOnly = true;
            this.notesBox.Size = new System.Drawing.Size(151, 348);
            this.notesBox.TabIndex = 1;
            this.notesBox.Text = "";
            // 
            // pageDesign
            // 
            this.pageDesign.Controls.Add(this.designBox);
            this.pageDesign.Controls.Add(this.designData);
            this.pageDesign.Location = new System.Drawing.Point(4, 22);
            this.pageDesign.Name = "pageDesign";
            this.pageDesign.Padding = new System.Windows.Forms.Padding(3);
            this.pageDesign.Size = new System.Drawing.Size(541, 589);
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
            this.designBox.Size = new System.Drawing.Size(477, 20);
            this.designBox.TabIndex = 26;
            // 
            // designData
            // 
            this.designData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designData.Location = new System.Drawing.Point(0, 19);
            this.designData.Name = "designData";
            this.designData.Size = new System.Drawing.Size(477, 568);
            this.designData.TabIndex = 24;
            // 
            // pageOperation
            // 
            this.pageOperation.BackColor = System.Drawing.Color.LightGray;
            this.pageOperation.Controls.Add(this.operations);
            this.pageOperation.Location = new System.Drawing.Point(4, 22);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.pageOperation.Size = new System.Drawing.Size(541, 589);
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
            this.pageData.Size = new System.Drawing.Size(541, 589);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // traitChart
            // 
            this.traitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traitChart.Location = new System.Drawing.Point(3, 3);
            this.traitChart.Name = "traitChart";
            this.traitChart.Size = new System.Drawing.Size(535, 583);
            this.traitChart.TabIndex = 0;
            // 
            // operations
            // 
            this.operations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operations.Location = new System.Drawing.Point(3, 3);
            this.operations.Name = "operations";
            this.operations.Size = new System.Drawing.Size(535, 583);
            this.operations.TabIndex = 0;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(700, 615);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.experimentDetails.ResumeLayout(false);
            this.pageSummary.ResumeLayout(false);
            this.pageSummary.PerformLayout();
            this.sowingPanel.ResumeLayout(false);
            this.sowingPanel.PerformLayout();
            this.pageDesign.ResumeLayout(false);
            this.pageDesign.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designData)).EndInit();
            this.pageOperation.ResumeLayout(false);
            this.pageData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView experimentsTree;
        private System.Windows.Forms.TabControl experimentDetails;
        private System.Windows.Forms.TabPage pageSummary;
        private System.Windows.Forms.Panel sowingPanel;
        private System.Windows.Forms.Label sowingLabel;
        private LabeledBox sowingMethodBox;
        private LabeledBox sowingPopBox;
        private LabeledBox sowingDateBox;
        private LabeledBox sowingRowBox;
        private LabeledBox sowingDepthBox;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label researchersLabel;
        private System.Windows.Forms.ListBox researchersBox;
        private System.Windows.Forms.Label notesLabel;
        private LabeledBox ratingBox;
        private LabeledBox repsBox;
        private LabeledBox endBox;
        private LabeledBox startBox;
        private LabeledBox metBox;
        private LabeledBox fieldBox;
        private LabeledBox cropBox;
        private System.Windows.Forms.Label experimentLabel;
        private System.Windows.Forms.RichTextBox notesBox;
        private System.Windows.Forms.TabPage pageDesign;
        private System.Windows.Forms.TextBox designBox;
        private System.Windows.Forms.DataGridView designData;
        private System.Windows.Forms.TabPage pageOperation;
        private System.Windows.Forms.TabPage pageData;
        private TraitChart traitChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private OperationsChart operations;
    }
}
