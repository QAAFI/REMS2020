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
            this.barPanel = new System.Windows.Forms.Panel();
            this.bar = new System.Windows.Forms.Panel();
            this.label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.runBtn = new System.Windows.Forms.Button();
            this.barPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctLabel
            // 
            this.pctLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctLabel.Location = new System.Drawing.Point(125, 6);
            this.pctLabel.Name = "pctLabel";
            this.pctLabel.Size = new System.Drawing.Size(35, 19);
            this.pctLabel.TabIndex = 7;
            this.pctLabel.Text = "0%";
            this.pctLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pctLabel.UseWaitCursor = true;
            // 
            // barPanel
            // 
            this.barPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.barPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barPanel.Controls.Add(this.bar);
            this.barPanel.Location = new System.Drawing.Point(3, 32);
            this.barPanel.Name = "barPanel";
            this.barPanel.Size = new System.Drawing.Size(238, 28);
            this.barPanel.TabIndex = 8;
            this.barPanel.UseWaitCursor = true;
            // 
            // bar
            // 
            this.bar.BackColor = System.Drawing.Color.LimeGreen;
            this.bar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bar.Location = new System.Drawing.Point(-1, -1);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(415, 28);
            this.bar.TabIndex = 0;
            this.bar.UseWaitCursor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(61, 13);
            this.label.TabIndex = 6;
            this.label.Text = "Initialising...";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.runBtn);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.barPanel);
            this.panel1.Controls.Add(this.pctLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 63);
            this.panel1.TabIndex = 10;
            // 
            // runBtn
            // 
            this.runBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.runBtn.Location = new System.Drawing.Point(166, 4);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(75, 23);
            this.runBtn.TabIndex = 9;
            this.runBtn.Text = "Run";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.RunClicked);
            // 
            // TrackerBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "TrackerBar";
            this.Size = new System.Drawing.Size(244, 63);
            this.barPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label pctLabel;
        private System.Windows.Forms.Panel barPanel;
        private System.Windows.Forms.Panel bar;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button runBtn;
    }
}
