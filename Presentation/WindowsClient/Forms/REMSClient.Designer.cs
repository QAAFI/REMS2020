namespace WindowsClient
{
    partial class REMSClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(REMSClient));
            this.notebook = new System.Windows.Forms.TabControl();
            this.homeTab = new System.Windows.Forms.TabPage();
            this.homeScreen = new WindowsClient.Controls.HomeScreen();
            this.notebook.SuspendLayout();
            this.homeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // notebook
            // 
            this.notebook.Controls.Add(this.homeTab);
            this.notebook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(792, 460);
            this.notebook.TabIndex = 2;
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.homeScreen);
            this.homeTab.Location = new System.Drawing.Point(4, 24);
            this.homeTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.homeTab.Name = "homeTab";
            this.homeTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.homeTab.Size = new System.Drawing.Size(784, 432);
            this.homeTab.TabIndex = 7;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // homeScreen
            // 
            this.homeScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeScreen.Location = new System.Drawing.Point(4, 3);
            this.homeScreen.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.homeScreen.Name = "homeScreen";
            this.homeScreen.Size = new System.Drawing.Size(776, 426);
            this.homeScreen.TabIndex = 0;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 460);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "REMSClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "REMS 2020";
            this.notebook.ResumeLayout(false);
            this.homeTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage homeTab;
        private Controls.HomeScreen homeScreen;
    }
}

