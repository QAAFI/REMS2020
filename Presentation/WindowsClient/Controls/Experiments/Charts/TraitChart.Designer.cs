﻿namespace WindowsClient.Controls
{
    partial class TraitChart
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.chartBox = new System.Windows.Forms.ComboBox();
            this.plotsBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.traitsBox = new System.Windows.Forms.ListBox();
            this.tChart = new Steema.TeeChart.TChart();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.chartBox);
            this.splitContainer1.Panel1.Controls.Add(this.plotsBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.traitsBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tChart);
            this.splitContainer1.Size = new System.Drawing.Size(821, 661);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Chart:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chartBox
            // 
            this.chartBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartBox.FormattingEnabled = true;
            this.chartBox.Items.AddRange(new object[] {
            "Series",
            "Line",
            "Bar",
            "Scatter"});
            this.chartBox.Location = new System.Drawing.Point(45, 32);
            this.chartBox.Name = "chartBox";
            this.chartBox.Size = new System.Drawing.Size(149, 23);
            this.chartBox.TabIndex = 3;
            // 
            // plotsBox
            // 
            this.plotsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plotsBox.FormattingEnabled = true;
            this.plotsBox.Items.AddRange(new object[] {
            "Mean"});
            this.plotsBox.Location = new System.Drawing.Point(45, 3);
            this.plotsBox.Name = "plotsBox";
            this.plotsBox.Size = new System.Drawing.Size(150, 23);
            this.plotsBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Plots:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // traitsBox
            // 
            this.traitsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.traitsBox.FormattingEnabled = true;
            this.traitsBox.IntegralHeight = false;
            this.traitsBox.ItemHeight = 15;
            this.traitsBox.Location = new System.Drawing.Point(0, 61);
            this.traitsBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.traitsBox.Name = "traitsBox";
            this.traitsBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.traitsBox.Size = new System.Drawing.Size(194, 600);
            this.traitsBox.TabIndex = 0;
            // 
            // tChart
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Grid.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Axis = this.tChart.Axes.Bottom;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Bottom.Labels.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Bottom.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Bottom.Labels.Font.Size = 9;
            this.tChart.Axes.Bottom.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.Bottom.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.MinorTicks.Length = 2;
            this.tChart.Axes.Bottom.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Ticks.Length = 4;
            this.tChart.Axes.Bottom.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.TicksInner.Length = 0;
            this.tChart.Axes.Bottom.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Bottom.Title.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Bottom.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Bottom.Title.Font.Size = 11;
            this.tChart.Axes.Bottom.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.Bottom.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Bottom.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.AxisPen.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Axis = this.tChart.Axes.Depth;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Depth.Labels.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Depth.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Depth.Labels.Font.Size = 9;
            this.tChart.Axes.Depth.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.Depth.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.MinorTicks.Length = 2;
            this.tChart.Axes.Depth.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Ticks.Length = 4;
            this.tChart.Axes.Depth.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.TicksInner.Length = 0;
            this.tChart.Axes.Depth.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Depth.Title.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Depth.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Depth.Title.Font.Size = 11;
            this.tChart.Axes.Depth.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.Depth.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Depth.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Depth.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Depth.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.AxisPen.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Axis = this.tChart.Axes.DepthTop;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.DepthTop.Labels.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.DepthTop.Labels.Font.Size = 9;
            this.tChart.Axes.DepthTop.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.DepthTop.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.MinorTicks.Length = 2;
            this.tChart.Axes.DepthTop.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Ticks.Length = 4;
            this.tChart.Axes.DepthTop.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.TicksInner.Length = 0;
            this.tChart.Axes.DepthTop.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.DepthTop.Title.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.DepthTop.Title.Font.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.DepthTop.Title.Font.Size = 11;
            this.tChart.Axes.DepthTop.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.DepthTop.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.DepthTop.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.AxisPen.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Axis = this.tChart.Axes.Left;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Left.Labels.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Left.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Left.Labels.Font.Size = 9;
            this.tChart.Axes.Left.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.Left.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.MinorTicks.Length = 2;
            this.tChart.Axes.Left.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Ticks.Length = 4;
            this.tChart.Axes.Left.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.TicksInner.Length = 0;
            this.tChart.Axes.Left.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Left.Title.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Left.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Left.Title.Font.Size = 11;
            this.tChart.Axes.Left.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.Left.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Left.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Left.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Left.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Left.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.AxisPen.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Axis = this.tChart.Axes.Right;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Right.Labels.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Right.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Right.Labels.Font.Size = 9;
            this.tChart.Axes.Right.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.Right.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.MinorTicks.Length = 2;
            this.tChart.Axes.Right.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Ticks.Length = 4;
            this.tChart.Axes.Right.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.TicksInner.Length = 0;
            this.tChart.Axes.Right.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Right.Title.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Right.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Right.Title.Font.Size = 11;
            this.tChart.Axes.Right.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.Right.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Right.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Right.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Right.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Right.Title.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.AxisPen.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Axis = this.tChart.Axes.Top;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Brush.Color = System.Drawing.Color.White;
            this.tChart.Axes.Top.Labels.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Axes.Top.Labels.Font.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Top.Labels.Font.Size = 9;
            this.tChart.Axes.Top.Labels.Font.SizeFloat = 9F;
            this.tChart.Axes.Top.Labels.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Labels.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Labels.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Labels.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.MinorTicks.Length = 2;
            this.tChart.Axes.Top.MinorTicks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Ticks.Length = 4;
            this.tChart.Axes.Top.Ticks.Visible = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.TicksInner.Length = 0;
            this.tChart.Axes.Top.TicksInner.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Axes.Top.Title.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Axes.Top.Title.Font.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Axes.Top.Title.Font.Size = 11;
            this.tChart.Axes.Top.Title.Font.SizeFloat = 11F;
            this.tChart.Axes.Top.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Axes.Top.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Axes.Top.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Axes.Top.Title.Shadow.Brush.Solid = true;
            this.tChart.Axes.Top.Title.Shadow.Brush.Visible = true;
            this.tChart.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tChart.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Footer.Brush.Solid = true;
            this.tChart.Footer.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Footer.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Footer.Font.Brush.Color = System.Drawing.Color.Red;
            this.tChart.Footer.Font.Brush.Solid = true;
            this.tChart.Footer.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Footer.Font.Shadow.Brush.Solid = true;
            this.tChart.Footer.Font.Shadow.Brush.Visible = true;
            this.tChart.Footer.Font.Size = 8;
            this.tChart.Footer.Font.SizeFloat = 8F;
            this.tChart.Footer.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Footer.ImageBevel.Brush.Solid = true;
            this.tChart.Footer.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Footer.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Footer.Shadow.Brush.Solid = true;
            this.tChart.Footer.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tChart.Header.Brush.Solid = true;
            this.tChart.Header.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Header.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Header.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.Header.Font.Brush.Solid = true;
            this.tChart.Header.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Header.Font.Shadow.Brush.Solid = true;
            this.tChart.Header.Font.Shadow.Brush.Visible = true;
            this.tChart.Header.Font.Size = 12;
            this.tChart.Header.Font.SizeFloat = 12F;
            this.tChart.Header.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Header.ImageBevel.Brush.Solid = true;
            this.tChart.Header.ImageBevel.Brush.Visible = true;
            this.tChart.Header.Lines = new string[] {
        ""};
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Header.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChart.Header.Shadow.Brush.Solid = true;
            this.tChart.Header.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Brush.Color = System.Drawing.Color.White;
            this.tChart.Legend.Brush.Solid = true;
            this.tChart.Legend.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.Legend.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tChart.Legend.Font.Brush.Solid = true;
            this.tChart.Legend.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Font.Shadow.Brush.Solid = true;
            this.tChart.Legend.Font.Shadow.Brush.Visible = true;
            this.tChart.Legend.Font.Size = 9;
            this.tChart.Legend.Font.SizeFloat = 9F;
            this.tChart.Legend.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Legend.ImageBevel.Brush.Solid = true;
            this.tChart.Legend.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tChart.Legend.Shadow.Brush.Solid = true;
            this.tChart.Legend.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Symbol.Legend = this.tChart.Legend;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Symbol.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Symbol.Shadow.Brush.Solid = true;
            this.tChart.Legend.Symbol.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Brush.Color = System.Drawing.Color.White;
            this.tChart.Legend.Title.Brush.Solid = true;
            this.tChart.Legend.Title.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Bold = true;
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Brush.Color = System.Drawing.Color.Black;
            this.tChart.Legend.Title.Font.Brush.Solid = true;
            this.tChart.Legend.Title.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Title.Font.Shadow.Brush.Solid = true;
            this.tChart.Legend.Title.Font.Shadow.Brush.Visible = true;
            this.tChart.Legend.Title.Font.Size = 8;
            this.tChart.Legend.Title.Font.SizeFloat = 8F;
            this.tChart.Legend.Title.Font.Style = Steema.TeeChart.Drawing.FontStyle.Bold;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Legend.Title.ImageBevel.Brush.Solid = true;
            this.tChart.Legend.Title.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Legend.Title.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Legend.Title.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Legend.Title.Shadow.Brush.Solid = true;
            this.tChart.Legend.Title.Shadow.Brush.Visible = true;
            this.tChart.Location = new System.Drawing.Point(0, 0);
            this.tChart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tChart.Name = "tChart";
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.tChart.Panel.Brush.Gradient.UseMiddle = true;
            this.tChart.Panel.Brush.Solid = true;
            this.tChart.Panel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Panel.ImageBevel.Brush.Solid = true;
            this.tChart.Panel.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.Panel.Pen.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Panel.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Panel.Shadow.Brush.Solid = true;
            this.tChart.Panel.Shadow.Brush.Visible = true;
            this.tChart.Size = new System.Drawing.Size(618, 661);
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.SubFooter.Brush.Solid = true;
            this.tChart.SubFooter.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Brush.Color = System.Drawing.Color.Red;
            this.tChart.SubFooter.Font.Brush.Solid = true;
            this.tChart.SubFooter.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubFooter.Font.Shadow.Brush.Solid = true;
            this.tChart.SubFooter.Font.Shadow.Brush.Visible = true;
            this.tChart.SubFooter.Font.Size = 8;
            this.tChart.SubFooter.Font.SizeFloat = 8F;
            this.tChart.SubFooter.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.SubFooter.ImageBevel.Brush.Solid = true;
            this.tChart.SubFooter.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubFooter.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubFooter.Shadow.Brush.Solid = true;
            this.tChart.SubFooter.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.tChart.SubHeader.Brush.Solid = true;
            this.tChart.SubHeader.Brush.Visible = true;
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Bold = false;
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tChart.SubHeader.Font.Brush.Solid = true;
            this.tChart.SubHeader.Font.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Font.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.SubHeader.Font.Shadow.Brush.Solid = true;
            this.tChart.SubHeader.Font.Shadow.Brush.Visible = true;
            this.tChart.SubHeader.Font.Size = 12;
            this.tChart.SubHeader.Font.SizeFloat = 12F;
            this.tChart.SubHeader.Font.Style = Steema.TeeChart.Drawing.FontStyle.Regular;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.SubHeader.ImageBevel.Brush.Solid = true;
            this.tChart.SubHeader.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.SubHeader.Shadow.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.tChart.SubHeader.Shadow.Brush.Solid = true;
            this.tChart.SubHeader.Shadow.Brush.Visible = true;
            this.tChart.TabIndex = 15;
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.Brush.Color = System.Drawing.Color.Silver;
            this.tChart.Walls.Back.Brush.Solid = true;
            this.tChart.Walls.Back.Brush.Visible = false;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Back.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Back.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Back.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Back.Shadow.Brush.Solid = true;
            this.tChart.Walls.Back.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.Brush.Color = System.Drawing.Color.White;
            this.tChart.Walls.Bottom.Brush.Solid = true;
            this.tChart.Walls.Bottom.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Bottom.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Bottom.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Bottom.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Bottom.Shadow.Brush.Solid = true;
            this.tChart.Walls.Bottom.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.Brush.Color = System.Drawing.Color.LightYellow;
            this.tChart.Walls.Left.Brush.Solid = true;
            this.tChart.Walls.Left.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Left.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Left.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Left.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Left.Shadow.Brush.Solid = true;
            this.tChart.Walls.Left.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.Brush.Color = System.Drawing.Color.LightYellow;
            this.tChart.Walls.Right.Brush.Solid = true;
            this.tChart.Walls.Right.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.ImageBevel.Brush.Color = System.Drawing.Color.LightGray;
            this.tChart.Walls.Right.ImageBevel.Brush.Solid = true;
            this.tChart.Walls.Right.ImageBevel.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Walls.Right.Shadow.Brush.Color = System.Drawing.Color.DarkGray;
            this.tChart.Walls.Right.Shadow.Brush.Solid = true;
            this.tChart.Walls.Right.Shadow.Brush.Visible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.tChart.Zoom.Brush.Color = System.Drawing.Color.LightBlue;
            this.tChart.Zoom.Brush.Solid = true;
            this.tChart.Zoom.Brush.Visible = true;
            // 
            // TraitChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TraitChart";
            this.Size = new System.Drawing.Size(821, 661);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Steema.TeeChart.TChart tChart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox traitsBox;
        private System.Windows.Forms.ComboBox plotsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox chartBox;
    }
}
