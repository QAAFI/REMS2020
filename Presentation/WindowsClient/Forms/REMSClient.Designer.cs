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
            this.detailsTab = new System.Windows.Forms.TabPage();
            this.detailer = new WindowsClient.Controls.ExperimentDetailer();
            this.importTab = new System.Windows.Forms.TabPage();
            this.importer = new WindowsClient.Controls.Importer();
            this.notebook.SuspendLayout();
            this.homeTab.SuspendLayout();
            this.detailsTab.SuspendLayout();
            this.importTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // notebook
            // 
            this.notebook.Controls.Add(this.homeTab);
            this.notebook.Controls.Add(this.detailsTab);
            this.notebook.Controls.Add(this.importTab);
            this.notebook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(1241, 786);
            this.notebook.TabIndex = 2;
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.homeScreen);
            this.homeTab.Location = new System.Drawing.Point(4, 24);
            this.homeTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.homeTab.Name = "homeTab";
            this.homeTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.homeTab.Size = new System.Drawing.Size(1233, 758);
            this.homeTab.TabIndex = 7;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // homeScreen
            // 
            this.homeScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeScreen.Location = new System.Drawing.Point(4, 3);
            this.homeScreen.Manager = null;
            this.homeScreen.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.homeScreen.Name = "homeScreen";
            this.homeScreen.Size = new System.Drawing.Size(1225, 752);
            this.homeScreen.TabIndex = 0;
            // 
            // detailsTab
            // 
            this.detailsTab.Controls.Add(this.detailer);
            this.detailsTab.Location = new System.Drawing.Point(4, 24);
            this.detailsTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.detailsTab.Name = "detailsTab";
            this.detailsTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.detailsTab.Size = new System.Drawing.Size(1233, 758);
            this.detailsTab.TabIndex = 9;
            this.detailsTab.Text = "Experiment details";
            this.detailsTab.UseVisualStyleBackColor = true;
            // 
            // detailer
            // 
            this.detailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailer.Location = new System.Drawing.Point(4, 3);
            this.detailer.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.detailer.Name = "detailer";
            this.detailer.Size = new System.Drawing.Size(1225, 752);
            this.detailer.TabIndex = 0;
            // 
            // importTab
            // 
            this.importTab.Controls.Add(this.importer);
            this.importTab.Location = new System.Drawing.Point(4, 24);
            this.importTab.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importTab.Name = "importTab";
            this.importTab.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importTab.Size = new System.Drawing.Size(1233, 758);
            this.importTab.TabIndex = 8;
            this.importTab.Text = "Import";
            this.importTab.UseVisualStyleBackColor = true;
            // 
            // importer
            // 
            this.importer.Data = null;
            this.importer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importer.Folder = "C:\\Users\\uqmstow1\\Documents";
            this.importer.Location = new System.Drawing.Point(4, 3);
            this.importer.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.importer.Name = "importer";
            this.importer.Size = new System.Drawing.Size(1225, 752);
            this.importer.TabIndex = 0;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 786);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "REMSClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "REMS 2020";
            this.notebook.ResumeLayout(false);
            this.homeTab.ResumeLayout(false);
            this.detailsTab.ResumeLayout(false);
            this.importTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage homeTab;
        private Controls.HomeScreen homeScreen;
        private System.Windows.Forms.TabPage importTab;
        private Controls.Importer importer;
        private System.Windows.Forms.TabPage detailsTab;
        private Controls.ExperimentDetailer detailer;
    }
}

