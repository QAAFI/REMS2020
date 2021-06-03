namespace WindowsClient.Controls
{
    partial class TrackerBar
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
            TaskBegun = null;

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
            this.pctLabel = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.runBtn = new System.Windows.Forms.Button();
            this.bar = new System.Windows.Forms.ProgressBar();
            this.panel = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctLabel
            // 
            this.pctLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctLabel.BackColor = System.Drawing.Color.Transparent;
            this.pctLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pctLabel.Location = new System.Drawing.Point(198, 2);
            this.pctLabel.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pctLabel.Name = "pctLabel";
            this.pctLabel.Padding = new System.Windows.Forms.Padding(3);
            this.pctLabel.Size = new System.Drawing.Size(47, 26);
            this.pctLabel.TabIndex = 7;
            this.pctLabel.Text = "0%";
            this.pctLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(94, 8);
            this.label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(34, 15);
            this.label.TabIndex = 6;
            this.label.Text = "         ";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // runBtn
            // 
            this.runBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.runBtn.Location = new System.Drawing.Point(0, 1);
            this.runBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(86, 29);
            this.runBtn.TabIndex = 9;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.RunClicked);
            // 
            // bar
            // 
            this.bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bar.Location = new System.Drawing.Point(0, 34);
            this.bar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(245, 25);
            this.bar.TabIndex = 10;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.bar);
            this.panel.Controls.Add(this.runBtn);
            this.panel.Controls.Add(this.pctLabel);
            this.panel.Controls.Add(this.label);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(247, 61);
            this.panel.TabIndex = 11;
            // 
            // TrackerBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TrackerBar";
            this.Size = new System.Drawing.Size(247, 61);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label pctLabel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button runBtn;
        private System.Windows.Forms.ProgressBar bar;
        private System.Windows.Forms.Panel panel;
    }
}
