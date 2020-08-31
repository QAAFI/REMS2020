using Rems.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsClient.Forms
{
    public partial class ProgressDialog : Form
    {
        private int item = 0;
        private int items;

        private double step;
        private double max;

        public ProgressDialog(string title, int items)
        {
            InitializeComponent();

            this.Text = title;
            this.items = items;

            bar.Width = 0;

            EventManager.StartProgress += OnStartProgress;
            EventManager.ProgressIncremented += OnProgressIncremented;
            EventManager.StopProgress += OnStopProgress;
        }

        private void OnStopProgress(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            
            Close();            
        }

        private void OnStartProgress(object sender, StartProgressArgs args)
        {
            item++;
            label.Text = $"{item} of {items}: {args.Item}";

            if (args.Maximum == 0)
            {
                bar.Width = barPanel.Width;
                pctLabel.Text = $"100%";

                Refresh();
                return;
            }

            bar.Width = 0;
            step = barPanel.Width / args.Maximum;
            max = args.Maximum;
            pctLabel.Text = $"0%";

            Refresh();
        }

        private void OnProgressIncremented(object sender, EventArgs e)
        {
            bar.Width += (int)step;

            int pct = Math.Min(100, 100 * bar.Width / barPanel.Width);
            pctLabel.Text = $"{pct}%";
            
            Refresh();
        }
    }
}
