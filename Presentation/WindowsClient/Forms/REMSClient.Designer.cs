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
            this.expsPage = new System.Windows.Forms.TabPage();
            this.experimentDetailer = new WindowsClient.Controls.ExperimentDetailer();
            this.infoPage = new System.Windows.Forms.TabPage();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.relationsListBox = new System.Windows.Forms.ListBox();
            this.notebook = new System.Windows.Forms.TabControl();
            this.homePage = new System.Windows.Forms.TabPage();
            this.homeScreen = new WindowsClient.Controls.HomeScreen();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.expsPage.SuspendLayout();
            this.infoPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.notebook.SuspendLayout();
            this.homePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // expsPage
            // 
            this.expsPage.Controls.Add(this.experimentDetailer);
            this.expsPage.Location = new System.Drawing.Point(4, 22);
            this.expsPage.Name = "expsPage";
            this.expsPage.Padding = new System.Windows.Forms.Padding(3);
            this.expsPage.Size = new System.Drawing.Size(749, 623);
            this.expsPage.TabIndex = 4;
            this.expsPage.Text = "Experiments";
            this.expsPage.UseVisualStyleBackColor = true;
            // 
            // experimentDetailer
            // 
            this.experimentDetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentDetailer.Location = new System.Drawing.Point(3, 3);
            this.experimentDetailer.Name = "experimentDetailer";
            this.experimentDetailer.Size = new System.Drawing.Size(743, 617);
            this.experimentDetailer.TabIndex = 0;
            // 
            // infoPage
            // 
            this.infoPage.BackColor = System.Drawing.Color.Transparent;
            this.infoPage.Controls.Add(this.splitContainer1);
            this.infoPage.Location = new System.Drawing.Point(4, 22);
            this.infoPage.Name = "infoPage";
            this.infoPage.Padding = new System.Windows.Forms.Padding(3);
            this.infoPage.Size = new System.Drawing.Size(749, 623);
            this.infoPage.TabIndex = 0;
            this.infoPage.Text = "Information";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(82, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
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
            this.dataGridView.Size = new System.Drawing.Size(578, 617);
            this.dataGridView.TabIndex = 1;
            // 
            // relationsListBox
            // 
            this.relationsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.relationsListBox.FormattingEnabled = true;
            this.relationsListBox.IntegralHeight = false;
            this.relationsListBox.Location = new System.Drawing.Point(3, 32);
            this.relationsListBox.Name = "relationsListBox";
            this.relationsListBox.ScrollAlwaysVisible = true;
            this.relationsListBox.Size = new System.Drawing.Size(155, 582);
            this.relationsListBox.TabIndex = 3;
            this.relationsListBox.SelectedIndexChanged += new System.EventHandler(this.OnRelationsIndexChanged);
            // 
            // notebook
            // 
            this.notebook.Controls.Add(this.homePage);
            this.notebook.Controls.Add(this.infoPage);
            this.notebook.Controls.Add(this.expsPage);
            this.notebook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notebook.Location = new System.Drawing.Point(0, 0);
            this.notebook.Name = "notebook";
            this.notebook.SelectedIndex = 0;
            this.notebook.Size = new System.Drawing.Size(757, 649);
            this.notebook.TabIndex = 2;
            // 
            // homePage
            // 
            this.homePage.Controls.Add(this.homeScreen);
            this.homePage.Location = new System.Drawing.Point(4, 22);
            this.homePage.Name = "homePage";
            this.homePage.Padding = new System.Windows.Forms.Padding(3);
            this.homePage.Size = new System.Drawing.Size(749, 623);
            this.homePage.TabIndex = 7;
            this.homePage.Text = "Home";
            this.homePage.UseVisualStyleBackColor = true;
            // 
            // homeScreen
            // 
            this.homeScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeScreen.Location = new System.Drawing.Point(3, 3);
            this.homeScreen.Name = "homeScreen";
            this.homeScreen.Size = new System.Drawing.Size(743, 617);
            this.homeScreen.TabIndex = 0;
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
            this.splitContainer1.Panel1.Controls.Add(this.btnOpen);
            this.splitContainer1.Panel1.Controls.Add(this.btnNew);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(743, 617);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 6;
            // 
            // REMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 649);
            this.Controls.Add(this.notebook);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "REMSClient";
            this.Text = "REMS 2020";
            this.expsPage.ResumeLayout(false);
            this.infoPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.notebook.ResumeLayout(false);
            this.homePage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage expsPage;
        private Controls.ExperimentDetailer experimentDetailer;
        private System.Windows.Forms.TabPage infoPage;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ListBox relationsListBox;
        private System.Windows.Forms.TabControl notebook;
        private System.Windows.Forms.TabPage homePage;
        private Controls.HomeScreen homeScreen;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

