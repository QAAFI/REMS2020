namespace WindowsClient.Forms
{
    partial class FileSelector
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
            this.infoBox = new System.Windows.Forms.TextBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.expsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.infoBtn = new System.Windows.Forms.Button();
            this.expsBox = new System.Windows.Forms.TextBox();
            this.dataBox = new System.Windows.Forms.TextBox();
            this.expsBtn = new System.Windows.Forms.Button();
            this.dataBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.importBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // infoBox
            // 
            this.infoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoBox.Location = new System.Drawing.Point(105, 10);
            this.infoBox.Margin = new System.Windows.Forms.Padding(4);
            this.infoBox.Name = "infoBox";
            this.infoBox.Size = new System.Drawing.Size(336, 24);
            this.infoBox.TabIndex = 0;
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLabel.Location = new System.Drawing.Point(12, 13);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(86, 18);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "Information:";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // expsLabel
            // 
            this.expsLabel.AutoSize = true;
            this.expsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expsLabel.Location = new System.Drawing.Point(4, 48);
            this.expsLabel.Name = "expsLabel";
            this.expsLabel.Size = new System.Drawing.Size(94, 18);
            this.expsLabel.TabIndex = 3;
            this.expsLabel.Text = "Experiments:";
            this.expsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Data:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // infoBtn
            // 
            this.infoBtn.Image = global::WindowsClient.Properties.Resources.select_icon;
            this.infoBtn.Location = new System.Drawing.Point(448, 10);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(24, 24);
            this.infoBtn.TabIndex = 6;
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.InfoClicked);
            // 
            // expsBox
            // 
            this.expsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expsBox.Location = new System.Drawing.Point(105, 45);
            this.expsBox.Margin = new System.Windows.Forms.Padding(4);
            this.expsBox.Name = "expsBox";
            this.expsBox.Size = new System.Drawing.Size(336, 24);
            this.expsBox.TabIndex = 7;
            // 
            // dataBox
            // 
            this.dataBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataBox.Location = new System.Drawing.Point(105, 80);
            this.dataBox.Margin = new System.Windows.Forms.Padding(4);
            this.dataBox.Name = "dataBox";
            this.dataBox.Size = new System.Drawing.Size(336, 24);
            this.dataBox.TabIndex = 8;
            // 
            // expsBtn
            // 
            this.expsBtn.Image = global::WindowsClient.Properties.Resources.select_icon;
            this.expsBtn.Location = new System.Drawing.Point(448, 45);
            this.expsBtn.Name = "expsBtn";
            this.expsBtn.Size = new System.Drawing.Size(24, 24);
            this.expsBtn.TabIndex = 9;
            this.expsBtn.UseVisualStyleBackColor = true;
            this.expsBtn.Click += new System.EventHandler(this.ExpsClicked);
            // 
            // dataBtn
            // 
            this.dataBtn.Image = global::WindowsClient.Properties.Resources.select_icon;
            this.dataBtn.Location = new System.Drawing.Point(448, 80);
            this.dataBtn.Name = "dataBtn";
            this.dataBtn.Size = new System.Drawing.Size(24, 24);
            this.dataBtn.TabIndex = 10;
            this.dataBtn.UseVisualStyleBackColor = true;
            this.dataBtn.Click += new System.EventHandler(this.DataClicked);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(366, 111);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 11;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelClicked);
            // 
            // importBtn
            // 
            this.importBtn.Location = new System.Drawing.Point(285, 111);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(75, 23);
            this.importBtn.TabIndex = 12;
            this.importBtn.Text = "&Import";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.ImportClicked);
            // 
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 141);
            this.Controls.Add(this.importBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.dataBtn);
            this.Controls.Add(this.expsBtn);
            this.Controls.Add(this.dataBox);
            this.Controls.Add(this.expsBox);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.expsLabel);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.infoBox);
            this.Name = "FileSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox infoBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Label expsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button infoBtn;
        private System.Windows.Forms.TextBox expsBox;
        private System.Windows.Forms.TextBox dataBox;
        private System.Windows.Forms.Button expsBtn;
        private System.Windows.Forms.Button dataBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button importBtn;
    }
}