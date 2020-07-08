namespace WindowsClient
{
    partial class ItemSelector
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
            this.combo = new System.Windows.Forms.ComboBox();
            this.instructions = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // combo
            // 
            this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo.FormattingEnabled = true;
            this.combo.Items.AddRange(new object[] {
            "None"});
            this.combo.Location = new System.Drawing.Point(12, 40);
            this.combo.Name = "combo";
            this.combo.Size = new System.Drawing.Size(387, 21);
            this.combo.TabIndex = 0;
            this.combo.Tag = "";
            // 
            // instructions
            // 
            this.instructions.AutoSize = true;
            this.instructions.Location = new System.Drawing.Point(12, 9);
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(60, 13);
            this.instructions.TabIndex = 1;
            this.instructions.Text = "sample text";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(323, 97);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // confirmBtn
            // 
            this.confirmBtn.Location = new System.Drawing.Point(242, 97);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(75, 23);
            this.confirmBtn.TabIndex = 3;
            this.confirmBtn.Text = "Confirm";
            this.confirmBtn.UseVisualStyleBackColor = true;
            // 
            // ItemSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 130);
            this.Controls.Add(this.confirmBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.combo);
            this.Name = "ItemSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combo;
        private System.Windows.Forms.Label instructions;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button confirmBtn;
    }
}

