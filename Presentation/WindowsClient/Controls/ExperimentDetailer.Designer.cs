﻿namespace WindowsClient.Controls
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
            this.operationsChart = new Steema.TeeChart.TChart();
            this.pageData = new System.Windows.Forms.TabPage();
            this.traitChart = new WindowsClient.Controls.TraitChart();
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
            this.splitContainer1.Size = new System.Drawing.Size(920, 620);
            this.splitContainer1.SplitterDistance = 162;
            this.splitContainer1.TabIndex = 5;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentsTree.HideSelection = false;
            this.experimentsTree.Location = new System.Drawing.Point(0, 0);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(162, 620);
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
            this.experimentDetails.Size = new System.Drawing.Size(754, 620);
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
            this.pageSummary.Size = new System.Drawing.Size(746, 594);
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
            this.sowingPanel.Size = new System.Drawing.Size(368, 241);
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
            this.descriptionBox.Size = new System.Drawing.Size(735, 20);
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
            this.researchersBox.Size = new System.Drawing.Size(357, 126);
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
            this.notesBox.Size = new System.Drawing.Size(356, 353);
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
            this.pageDesign.Size = new System.Drawing.Size(746, 594);
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
            this.designBox.Size = new System.Drawing.Size(682, 20);
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
            this.designData.Size = new System.Drawing.Size(682, 573);
            this.designData.TabIndex = 24;
            // 
            // pageOperation
            // 
            this.pageOperation.BackColor = System.Drawing.Color.LightGray;
            this.pageOperation.Controls.Add(this.operationsChart);
            this.pageOperation.Location = new System.Drawing.Point(4, 22);
            this.pageOperation.Name = "pageOperation";
            this.pageOperation.Padding = new System.Windows.Forms.Padding(3);
            this.pageOperation.Size = new System.Drawing.Size(746, 594);
            this.pageOperation.TabIndex = 3;
            this.pageOperation.Text = "Operations";
            // 
            // operationsChart
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.FixedLabelSize = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Angle = 60;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.Bottom.Labels.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Labels.Brush.Visible = true;
            this.operationsChart.Axes.Bottom.Labels.ClipText = false;
            this.operationsChart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            this.operationsChart.Axes.Bottom.Labels.ExactDateTime = false;
            this.operationsChart.Axes.Bottom.Labels.Exponent = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.Bottom.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Bottom.Labels.Font.Size = 9;
            this.operationsChart.Axes.Bottom.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Bottom.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Bottom.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Ticks.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Angle = 0;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.Bottom.Title.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Title.Brush.Visible = true;
            this.operationsChart.Axes.Bottom.Title.ClipText = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.Bottom.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Bottom.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Bottom.Title.Font.Size = 11;
            this.operationsChart.Axes.Bottom.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Bottom.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Bottom.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Bottom.Title.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Bottom.Title.TextAlign = System.Drawing.StringAlignment.Center;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.Depth.Labels.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.Depth.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Depth.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Depth.Labels.Font.Size = 9;
            this.operationsChart.Axes.Depth.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Depth.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Depth.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Angle = 0;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.Depth.Title.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.Depth.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Depth.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Depth.Title.Font.Size = 11;
            this.operationsChart.Axes.Depth.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Depth.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Depth.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Depth.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.DepthTop.Labels.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.DepthTop.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.DepthTop.Labels.Font.Size = 9;
            this.operationsChart.Axes.DepthTop.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.DepthTop.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Angle = 0;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.DepthTop.Title.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.DepthTop.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.DepthTop.Title.Font.Size = 11;
            this.operationsChart.Axes.DepthTop.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.DepthTop.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.DepthTop.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.DepthTop.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.Left.Labels.Brush.Solid = true;
            this.operationsChart.Axes.Left.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.Left.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.Left.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Left.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Left.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Left.Labels.Font.Size = 9;
            this.operationsChart.Axes.Left.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Left.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Left.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Left.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Left.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Angle = 90;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.Left.Title.Brush.Solid = true;
            this.operationsChart.Axes.Left.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.Left.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.Left.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Left.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Left.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Left.Title.Font.Size = 11;
            this.operationsChart.Axes.Left.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Left.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Left.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Left.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Left.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.Right.Labels.Brush.Solid = true;
            this.operationsChart.Axes.Right.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.Right.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.Right.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Right.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Right.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Right.Labels.Font.Size = 9;
            this.operationsChart.Axes.Right.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Right.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Right.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Right.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Right.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Angle = 270;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.Right.Title.Brush.Solid = true;
            this.operationsChart.Axes.Right.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.Right.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.Right.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Right.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Right.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Right.Title.Font.Size = 11;
            this.operationsChart.Axes.Right.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Right.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Right.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Right.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Right.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Axes.Top.Labels.Brush.Solid = true;
            this.operationsChart.Axes.Top.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Axes.Top.Labels.Font.Brush.Solid = true;
            this.operationsChart.Axes.Top.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Top.Labels.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Top.Labels.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Top.Labels.Font.Size = 9;
            this.operationsChart.Axes.Top.Labels.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Top.Labels.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Top.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Top.Labels.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Top.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Angle = 0;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Axes.Top.Title.Brush.Solid = true;
            this.operationsChart.Axes.Top.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Axes.Top.Title.Font.Brush.Solid = true;
            this.operationsChart.Axes.Top.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Top.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Top.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Axes.Top.Title.Font.Size = 11;
            this.operationsChart.Axes.Top.Title.Font.SizeFloat = 11F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Axes.Top.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Axes.Top.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Axes.Top.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Axes.Top.Title.Shadow.Brush.Visible = true;
            this.operationsChart.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Footer.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Footer.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Footer.Brush.Solid = true;
            this.operationsChart.Footer.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Footer.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Footer.Font.Brush.Color = System.Drawing.Color.Red;
            this.operationsChart.Footer.Font.Brush.Solid = true;
            this.operationsChart.Footer.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Footer.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Footer.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Footer.Font.Size = 8;
            this.operationsChart.Footer.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Footer.ImageBevel.Brush.Solid = true;
            this.operationsChart.Footer.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Footer.Shadow.Brush.Solid = true;
            this.operationsChart.Footer.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Header.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Header.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.operationsChart.Header.Brush.Solid = true;
            this.operationsChart.Header.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Header.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.Header.Font.Brush.Solid = true;
            this.operationsChart.Header.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Header.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Header.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Header.Font.Size = 12;
            this.operationsChart.Header.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Header.ImageBevel.Brush.Solid = true;
            this.operationsChart.Header.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.operationsChart.Header.Shadow.Brush.Solid = true;
            this.operationsChart.Header.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Legend.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Legend.Brush.Solid = true;
            this.operationsChart.Legend.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Legend.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.operationsChart.Legend.Font.Brush.Solid = true;
            this.operationsChart.Legend.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Legend.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Legend.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Legend.Font.Size = 9;
            this.operationsChart.Legend.Font.SizeFloat = 9F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Legend.ImageBevel.Brush.Solid = true;
            this.operationsChart.Legend.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.operationsChart.Legend.Shadow.Brush.Solid = true;
            this.operationsChart.Legend.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Legend.Symbol.Shadow.Brush.Solid = true;
            this.operationsChart.Legend.Symbol.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Legend.Title.Brush.Solid = true;
            this.operationsChart.Legend.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black;
            this.operationsChart.Legend.Title.Font.Brush.Solid = true;
            this.operationsChart.Legend.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Legend.Title.Font.Shadow.Brush.Solid = true;
            this.operationsChart.Legend.Title.Font.Shadow.Brush.Visible = true;
            this.operationsChart.Legend.Title.Font.Size = 8;
            this.operationsChart.Legend.Title.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Legend.Title.ImageBevel.Brush.Solid = true;
            this.operationsChart.Legend.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Legend.Title.Shadow.Brush.Solid = true;
            this.operationsChart.Legend.Title.Shadow.Brush.Visible = true;
            this.operationsChart.Location = new System.Drawing.Point(3, 3);
            this.operationsChart.Name = "operationsChart";
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Panel.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.operationsChart.Panel.Brush.Solid = true;
            this.operationsChart.Panel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Panel.ImageBevel.Brush.Solid = true;
            this.operationsChart.Panel.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Panel.Shadow.Brush.Solid = true;
            this.operationsChart.Panel.Shadow.Brush.Visible = true;
            this.operationsChart.Size = new System.Drawing.Size(740, 588);
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubFooter.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.SubFooter.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.SubFooter.Brush.Solid = true;
            this.operationsChart.SubFooter.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.SubFooter.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.SubFooter.Font.Brush.Color = System.Drawing.Color.Red;
            this.operationsChart.SubFooter.Font.Brush.Solid = true;
            this.operationsChart.SubFooter.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.SubFooter.Font.Shadow.Brush.Solid = true;
            this.operationsChart.SubFooter.Font.Shadow.Brush.Visible = true;
            this.operationsChart.SubFooter.Font.Size = 8;
            this.operationsChart.SubFooter.Font.SizeFloat = 8F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.SubFooter.ImageBevel.Brush.Solid = true;
            this.operationsChart.SubFooter.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.SubFooter.Shadow.Brush.Solid = true;
            this.operationsChart.SubFooter.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubHeader.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.operationsChart.SubHeader.Brush.Solid = true;
            this.operationsChart.SubHeader.Brush.Visible = true;
            // 
            // 
            // 
            this.operationsChart.SubHeader.Font.Bold = false;
            // 
            // 
            // 
            this.operationsChart.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.operationsChart.SubHeader.Font.Brush.Solid = true;
            this.operationsChart.SubHeader.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.SubHeader.Font.Shadow.Brush.Solid = true;
            this.operationsChart.SubHeader.Font.Shadow.Brush.Visible = true;
            this.operationsChart.SubHeader.Font.Size = 12;
            this.operationsChart.SubHeader.Font.SizeFloat = 12F;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.SubHeader.ImageBevel.Brush.Solid = true;
            this.operationsChart.SubHeader.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.operationsChart.SubHeader.Shadow.Brush.Solid = true;
            this.operationsChart.SubHeader.Shadow.Brush.Visible = true;
            this.operationsChart.TabIndex = 0;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Back.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Walls.Back.Brush.Color = System.Drawing.Color.Silver;
            this.operationsChart.Walls.Back.Brush.Solid = true;
            this.operationsChart.Walls.Back.Brush.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Walls.Back.ImageBevel.Brush.Solid = true;
            this.operationsChart.Walls.Back.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Walls.Back.Shadow.Brush.Solid = true;
            this.operationsChart.Walls.Back.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Bottom.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Walls.Bottom.Brush.Color = System.Drawing.Color.White;
            this.operationsChart.Walls.Bottom.Brush.Solid = true;
            this.operationsChart.Walls.Bottom.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Walls.Bottom.ImageBevel.Brush.Solid = true;
            this.operationsChart.Walls.Bottom.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Walls.Bottom.Shadow.Brush.Solid = true;
            this.operationsChart.Walls.Bottom.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Left.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow;
            this.operationsChart.Walls.Left.Brush.Solid = true;
            this.operationsChart.Walls.Left.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Walls.Left.ImageBevel.Brush.Solid = true;
            this.operationsChart.Walls.Left.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Walls.Left.Shadow.Brush.Solid = true;
            this.operationsChart.Walls.Left.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Right.Bevel.Outer = Steema.TeeChart.Drawing.BevelStyles.None;
            // 
            // 
            // 
            this.operationsChart.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow;
            this.operationsChart.Walls.Right.Brush.Solid = true;
            this.operationsChart.Walls.Right.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.operationsChart.Walls.Right.ImageBevel.Brush.Solid = true;
            this.operationsChart.Walls.Right.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.operationsChart.Walls.Right.Shadow.Brush.Solid = true;
            this.operationsChart.Walls.Right.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.operationsChart.Zoom.Brush.Color = System.Drawing.Color.LightBlue;
            this.operationsChart.Zoom.Brush.Solid = true;
            this.operationsChart.Zoom.Brush.Visible = true;
            // 
            // pageData
            // 
            this.pageData.BackColor = System.Drawing.Color.LightGray;
            this.pageData.Controls.Add(this.traitChart);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(3);
            this.pageData.Size = new System.Drawing.Size(746, 594);
            this.pageData.TabIndex = 5;
            this.pageData.Text = "Data";
            // 
            // traitChart
            // 
            this.traitChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.traitChart.Location = new System.Drawing.Point(3, 3);
            this.traitChart.Name = "traitChart";
            this.traitChart.Node = null;
            this.traitChart.Size = new System.Drawing.Size(740, 588);
            this.traitChart.TabIndex = 0;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(920, 620);
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
        private Steema.TeeChart.TChart operationsChart;
        private System.Windows.Forms.TabPage pageData;
        private TraitChart traitChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}