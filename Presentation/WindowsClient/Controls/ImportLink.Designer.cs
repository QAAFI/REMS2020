namespace WindowsClient.Controls
{
    partial class ImportLink
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
            this.label = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.image = new System.Windows.Forms.PictureBox();
            this.fileBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.image)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(3, 3);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(204, 21);
            this.label.TabIndex = 1;
            this.label.Text = "Information:";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button
            // 
            this.button.BackgroundImage = global::WindowsClient.Properties.Resources.select_icon;
            this.button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button.Location = new System.Drawing.Point(3, 27);
            this.button.MaximumSize = new System.Drawing.Size(24, 24);
            this.button.MinimumSize = new System.Drawing.Size(24, 24);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(24, 24);
            this.button.TabIndex = 2;
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.OnClick);
            // 
            // image
            // 
            this.image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.image.BackgroundImage = global::WindowsClient.Properties.Resources.InvalidOn;
            this.image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.image.Location = new System.Drawing.Point(213, 5);
            this.image.Name = "image";
            this.image.Size = new System.Drawing.Size(16, 16);
            this.image.TabIndex = 0;
            this.image.TabStop = false;
            // 
            // fileBox
            // 
            this.fileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileBox.Location = new System.Drawing.Point(29, 27);
            this.fileBox.MaximumSize = new System.Drawing.Size(1000, 24);
            this.fileBox.MinimumSize = new System.Drawing.Size(200, 24);
            this.fileBox.Multiline = true;
            this.fileBox.Name = "fileBox";
            this.fileBox.ReadOnly = true;
            this.fileBox.Size = new System.Drawing.Size(200, 24);
            this.fileBox.TabIndex = 3;
            // 
            // ImportLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileBox);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label);
            this.Controls.Add(this.image);
            this.MaximumSize = new System.Drawing.Size(0, 54);
            this.MinimumSize = new System.Drawing.Size(232, 54);
            this.Name = "ImportLink";
            this.Size = new System.Drawing.Size(232, 54);
            ((System.ComponentModel.ISupportInitialize)(this.image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.TextBox fileBox;
    }
}
