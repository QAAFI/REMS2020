namespace WindowsClient
{
    partial class PropertyControl
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
            this.propertyLabel = new System.Windows.Forms.Label();
            this.valueBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // propertyLabel
            // 
            this.propertyLabel.AutoSize = true;
            this.propertyLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.propertyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.propertyLabel.Font = new System.Drawing.Font(propertyLabel.Font, System.Drawing.FontStyle.Bold);
            this.propertyLabel.Location = new System.Drawing.Point(0, 0);
            this.propertyLabel.Margin = new System.Windows.Forms.Padding(0);
            this.propertyLabel.MaximumSize = new System.Drawing.Size(150, 20);
            this.propertyLabel.MinimumSize = new System.Drawing.Size(150, 20);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(150, 20);
            this.propertyLabel.TabIndex = 0;
            this.propertyLabel.Text = "HereIsSomeLongSampleText";
            this.propertyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueBox
            // 
            this.valueBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valueBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueBox.Location = new System.Drawing.Point(150, 0);
            this.valueBox.Margin = new System.Windows.Forms.Padding(0);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(150, 20);
            this.valueBox.TabIndex = 1;
            // 
            // PropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.propertyLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "PropertyControl";
            this.Size = new System.Drawing.Size(300, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label propertyLabel;
        private System.Windows.Forms.TextBox valueBox;
    }
}
