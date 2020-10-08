namespace WindowsClient.Forms
{
    partial class ProgressDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.pctLabel = new System.Windows.Forms.Label();
            this.barPanel = new System.Windows.Forms.Panel();
            this.bar = new System.Windows.Forms.Panel();
            this.barPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(12, 17);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(61, 13);
            this.label.TabIndex = 2;
            this.label.Text = "Initialising...";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pctLabel
            // 
            this.pctLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctLabel.Location = new System.Drawing.Point(315, 17);
            this.pctLabel.Name = "pctLabel";
            this.pctLabel.Size = new System.Drawing.Size(35, 13);
            this.pctLabel.TabIndex = 3;
            this.pctLabel.Text = "0%";
            this.pctLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // barPanel
            // 
            this.barPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.barPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barPanel.Controls.Add(this.bar);
            this.barPanel.Location = new System.Drawing.Point(12, 42);
            this.barPanel.Name = "barPanel";
            this.barPanel.Size = new System.Drawing.Size(338, 28);
            this.barPanel.TabIndex = 4;
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.LimeGreen;
            this.bar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bar.Location = new System.Drawing.Point(-1, -1);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(338, 28);
            this.bar.TabIndex = 0;
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 82);
            this.Controls.Add(this.barPanel);
            this.Controls.Add(this.pctLabel);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.barPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label pctLabel;
        private System.Windows.Forms.Panel barPanel;
        private System.Windows.Forms.Panel bar;
    }
}