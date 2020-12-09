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
            this.tablesPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.notebook = new System.Windows.Forms.TabControl();
            this.homePage = new System.Windows.Forms.TabPage();
            this.homeScreen = new WindowsClient.Controls.HomeScreen();
            this.tablesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
            this.homePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesPage
            // 
            this.tablesPage.BackColor = System.Drawing.Color.Transparent;
            this.tablesPage.Controls.Add(this.splitContainer1);
            this.tablesPage.Location = new System.Drawing.Point(4, 22);
            this.tablesPage.Name = "tablesPage";
            this.tablesPage.Padding = new System.Windows.Forms.Padding(3);
            this.tablesPage.Size = new System.Drawing.Size(1056, 655);
            this.tablesPage.TabIndex = 0;
            this.tablesPage.Text = "Tables";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.relationsListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 649);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 6;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.IntegralHeight = false;
            this.relationsListBox.Location = new System.Drawing.Point(0, 0);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.ScrollAlwaysVisible = true;
            this.relationsListBox.Size = new System.Drawing.Size(227, 649);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.OnRelationsIndexChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(819, 649);
            this.dataGridView.TabIndex = 1;
            // 
            // notebook
            // 
            this.notebook.Controls.Add(this.homePage);
            this.notebook.Controls.Add(this.tablesPage);
            this.notebook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(1064, 681);
            this.notebook.TabIndex = 2;
            // 
            // homePage
            // 
            this.homePage.Controls.Add(this.homeScreen);
            this.homePage.Location = new System.Drawing.Point(4, 22);
            this.homePage.Name = "homePage";
            this.homePage.Padding = new System.Windows.Forms.Padding(3);
            this.homePage.Size = new System.Drawing.Size(1056, 655);
            this.homePage.TabIndex = 7;
            this.homePage.Text = "Home";
            this.homePage.UseVisualStyleBackColor = true;
            // 
            // homeScreen
            // 
            this.homeScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeScreen.Location = new System.Drawing.Point(3, 3);
            this.homeScreen.Name = "homeScreen";
            this.homeScreen.Size = new System.Drawing.Size(1050, 649);
            this.homeScreen.TabIndex = 0;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "REMSClient";
            this.Text = "REMS 2020";
            this.tablesPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
            this.homePage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tablesPage;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage homePage;
        private Controls.HomeScreen homeScreen;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

