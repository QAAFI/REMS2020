namespace WindowsClient.Controls
{
    partial class ExperimentDetailer
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
            this.container = new System.Windows.Forms.SplitContainer();
            this.experimentsTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.container)).BeginInit();
            this.container.Panel1.SuspendLayout();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.container.Location = new System.Drawing.Point(0, 0);
            this.container.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.container.Name = "container";
            // 
            // container.Panel1
            // 
            this.container.Panel1.Controls.Add(this.experimentsTree);
            this.container.Size = new System.Drawing.Size(966, 643);
            this.container.SplitterDistance = 201;
            this.container.SplitterWidth = 5;
            this.container.TabIndex = 5;
            // 
            // experimentsTree
            // 
            this.experimentsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.experimentsTree.HideSelection = false;
            this.experimentsTree.Location = new System.Drawing.Point(0, 0);
            this.experimentsTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.experimentsTree.Name = "experimentsTree";
            this.experimentsTree.Size = new System.Drawing.Size(201, 643);
            this.experimentsTree.TabIndex = 4;
            // 
            // ExperimentDetailer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.container);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ExperimentDetailer";
            this.Size = new System.Drawing.Size(966, 643);
            this.container.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.container)).EndInit();
            this.container.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView experimentsTree;
        private System.Windows.Forms.SplitContainer container;
    }
}
