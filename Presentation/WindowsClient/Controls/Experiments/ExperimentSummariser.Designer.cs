
namespace WindowsClient.Controls.Experiments
{
    partial class ExperimentSummariser
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sowingPanel = new System.Windows.Forms.Panel();
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
            this.sowingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 55;
            this.label2.Text = "Details:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 54;
            this.label1.Text = "Sowing:";
            // 
            // sowingPanel
            // 
            this.sowingPanel.BackColor = System.Drawing.Color.Silver;
            this.sowingPanel.Controls.Add(this.sowingMethodBox);
            this.sowingPanel.Controls.Add(this.sowingPopBox);
            this.sowingPanel.Controls.Add(this.sowingDateBox);
            this.sowingPanel.Controls.Add(this.sowingRowBox);
            this.sowingPanel.Controls.Add(this.sowingDepthBox);
            this.sowingPanel.Location = new System.Drawing.Point(321, 73);
            this.sowingPanel.Name = "sowingPanel";
            this.sowingPanel.Size = new System.Drawing.Size(312, 208);
            this.sowingPanel.TabIndex = 53;
            // 
            // sowingMethodBox
            // 
            this.sowingMethodBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sowingMethodBox.Content = "";
            this.sowingMethodBox.Label = "Method:";
            this.sowingMethodBox.Location = new System.Drawing.Point(3, 4);
            this.sowingMethodBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingMethodBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingMethodBox.Name = "sowingMethodBox";
            this.sowingMethodBox.ReadOnly = true;
            this.sowingMethodBox.Size = new System.Drawing.Size(305, 36);
            this.sowingMethodBox.TabIndex = 20;
            // 
            // sowingPopBox
            // 
            this.sowingPopBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sowingPopBox.Content = "";
            this.sowingPopBox.Label = "Population:";
            this.sowingPopBox.Location = new System.Drawing.Point(3, 167);
            this.sowingPopBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingPopBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingPopBox.Name = "sowingPopBox";
            this.sowingPopBox.ReadOnly = true;
            this.sowingPopBox.Size = new System.Drawing.Size(305, 36);
            this.sowingPopBox.TabIndex = 19;
            // 
            // sowingDateBox
            // 
            this.sowingDateBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sowingDateBox.Content = "";
            this.sowingDateBox.Label = "Date:";
            this.sowingDateBox.Location = new System.Drawing.Point(3, 46);
            this.sowingDateBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingDateBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingDateBox.Name = "sowingDateBox";
            this.sowingDateBox.ReadOnly = true;
            this.sowingDateBox.Size = new System.Drawing.Size(305, 36);
            this.sowingDateBox.TabIndex = 16;
            // 
            // sowingRowBox
            // 
            this.sowingRowBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sowingRowBox.Content = "";
            this.sowingRowBox.Label = "Row space:";
            this.sowingRowBox.Location = new System.Drawing.Point(2, 130);
            this.sowingRowBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingRowBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingRowBox.Name = "sowingRowBox";
            this.sowingRowBox.ReadOnly = true;
            this.sowingRowBox.Size = new System.Drawing.Size(306, 36);
            this.sowingRowBox.TabIndex = 18;
            // 
            // sowingDepthBox
            // 
            this.sowingDepthBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sowingDepthBox.Content = "";
            this.sowingDepthBox.Label = "Depth:";
            this.sowingDepthBox.Location = new System.Drawing.Point(3, 88);
            this.sowingDepthBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.sowingDepthBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.sowingDepthBox.Name = "sowingDepthBox";
            this.sowingDepthBox.ReadOnly = true;
            this.sowingDepthBox.Size = new System.Drawing.Size(305, 36);
            this.sowingDepthBox.TabIndex = 17;
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(3, 27);
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(630, 20);
            this.descriptionBox.TabIndex = 52;
            // 
            // researchersLabel
            // 
            this.researchersLabel.AutoSize = true;
            this.researchersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.researchersLabel.Location = new System.Drawing.Point(3, 291);
            this.researchersLabel.Name = "researchersLabel";
            this.researchersLabel.Size = new System.Drawing.Size(105, 17);
            this.researchersLabel.TabIndex = 51;
            this.researchersLabel.Text = "Researchers:";
            // 
            // researchersBox
            // 
            this.researchersBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.researchersBox.FormattingEnabled = true;
            this.researchersBox.IntegralHeight = false;
            this.researchersBox.Location = new System.Drawing.Point(3, 311);
            this.researchersBox.Name = "researchersBox";
            this.researchersBox.Size = new System.Drawing.Size(312, 206);
            this.researchersBox.TabIndex = 50;
            // 
            // notesLabel
            // 
            this.notesLabel.AutoSize = true;
            this.notesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notesLabel.Location = new System.Drawing.Point(321, 291);
            this.notesLabel.Name = "notesLabel";
            this.notesLabel.Size = new System.Drawing.Size(55, 17);
            this.notesLabel.TabIndex = 49;
            this.notesLabel.Text = "Notes:";
            // 
            // ratingBox
            // 
            this.ratingBox.Content = "";
            this.ratingBox.Label = "Rating:";
            this.ratingBox.Location = new System.Drawing.Point(165, 199);
            this.ratingBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.ratingBox.MinimumSize = new System.Drawing.Size(150, 36);
            this.ratingBox.Name = "ratingBox";
            this.ratingBox.ReadOnly = true;
            this.ratingBox.Size = new System.Drawing.Size(150, 36);
            this.ratingBox.TabIndex = 48;
            // 
            // repsBox
            // 
            this.repsBox.Content = "";
            this.repsBox.Label = "Replicates:";
            this.repsBox.Location = new System.Drawing.Point(3, 199);
            this.repsBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.repsBox.MinimumSize = new System.Drawing.Size(150, 36);
            this.repsBox.Name = "repsBox";
            this.repsBox.ReadOnly = true;
            this.repsBox.Size = new System.Drawing.Size(150, 36);
            this.repsBox.TabIndex = 47;
            // 
            // endBox
            // 
            this.endBox.Content = "";
            this.endBox.Label = "End date:";
            this.endBox.Location = new System.Drawing.Point(165, 241);
            this.endBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.endBox.MinimumSize = new System.Drawing.Size(150, 36);
            this.endBox.Name = "endBox";
            this.endBox.ReadOnly = true;
            this.endBox.Size = new System.Drawing.Size(150, 36);
            this.endBox.TabIndex = 46;
            // 
            // startBox
            // 
            this.startBox.Content = "";
            this.startBox.Label = "Start date:";
            this.startBox.Location = new System.Drawing.Point(5, 241);
            this.startBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.startBox.MinimumSize = new System.Drawing.Size(150, 36);
            this.startBox.Name = "startBox";
            this.startBox.ReadOnly = true;
            this.startBox.Size = new System.Drawing.Size(150, 36);
            this.startBox.TabIndex = 45;
            // 
            // metBox
            // 
            this.metBox.Content = "";
            this.metBox.Label = "Met Station:";
            this.metBox.Location = new System.Drawing.Point(3, 157);
            this.metBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.metBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.metBox.Name = "metBox";
            this.metBox.ReadOnly = true;
            this.metBox.Size = new System.Drawing.Size(312, 36);
            this.metBox.TabIndex = 44;
            // 
            // fieldBox
            // 
            this.fieldBox.Content = "";
            this.fieldBox.Label = "Field:";
            this.fieldBox.Location = new System.Drawing.Point(3, 115);
            this.fieldBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.fieldBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.fieldBox.Name = "fieldBox";
            this.fieldBox.ReadOnly = true;
            this.fieldBox.Size = new System.Drawing.Size(312, 36);
            this.fieldBox.TabIndex = 43;
            // 
            // cropBox
            // 
            this.cropBox.Content = "";
            this.cropBox.Label = "Crop:";
            this.cropBox.Location = new System.Drawing.Point(3, 77);
            this.cropBox.MaximumSize = new System.Drawing.Size(1000, 36);
            this.cropBox.MinimumSize = new System.Drawing.Size(200, 36);
            this.cropBox.Name = "cropBox";
            this.cropBox.ReadOnly = true;
            this.cropBox.Size = new System.Drawing.Size(312, 36);
            this.cropBox.TabIndex = 42;
            // 
            // experimentLabel
            // 
            this.experimentLabel.AutoSize = true;
            this.experimentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.experimentLabel.Location = new System.Drawing.Point(3, 0);
            this.experimentLabel.Name = "experimentLabel";
            this.experimentLabel.Size = new System.Drawing.Size(123, 24);
            this.experimentLabel.TabIndex = 41;
            this.experimentLabel.Text = "Experiment:";
            // 
            // notesBox
            // 
            this.notesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.notesBox.Location = new System.Drawing.Point(321, 311);
            this.notesBox.Name = "notesBox";
            this.notesBox.ReadOnly = true;
            this.notesBox.Size = new System.Drawing.Size(312, 206);
            this.notesBox.TabIndex = 40;
            this.notesBox.Text = "";
            // 
            // ExperimentSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sowingPanel);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.researchersLabel);
            this.Controls.Add(this.researchersBox);
            this.Controls.Add(this.notesLabel);
            this.Controls.Add(this.ratingBox);
            this.Controls.Add(this.repsBox);
            this.Controls.Add(this.endBox);
            this.Controls.Add(this.startBox);
            this.Controls.Add(this.metBox);
            this.Controls.Add(this.fieldBox);
            this.Controls.Add(this.cropBox);
            this.Controls.Add(this.experimentLabel);
            this.Controls.Add(this.notesBox);
            this.MinimumSize = new System.Drawing.Size(637, 0);
            this.Name = "ExperimentSummary";
            this.Size = new System.Drawing.Size(637, 520);
            this.sowingPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel sowingPanel;
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
    }
}
