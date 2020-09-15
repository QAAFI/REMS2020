using Rems.Application;
using Rems.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Forms
{
    public partial class ProgressDialog : Form
    {
        private delegate void SafeIncrement();

        private int item = 0;
        private readonly int items;

        private double step;
        private double progress;

        public ProgressDialog(IProgressTracker tracker, string title)
        {
            InitializeComponent();

            items = tracker.Items;
            Text = title;
            bar.Width = 0;            

            Show();

            tracker.NextProgress += OnNextProgress;
            tracker.IncrementProgress += OnProgressChanged;
            tracker.StopProgress += OnRunWorkerCompleted;

            tracker.Run();
        }        

        private void OnProgressChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(OnProgressChanged));
            }
            else
            {
                progress += step;
                bar.Width = (int)progress;

                int pct = Math.Min(100, 100 * bar.Width / barPanel.Width);
                pctLabel.Text = $"{pct}%";

                Refresh();
            }
        }

        private void OnRunWorkerCompleted(object sender, EventArgs e)
        {
            Thread.Sleep(1500);

            Close();

            MessageBox.Show("Import complete!");
        }

        private void OnNextProgress(object sender, StartProgressArgs args)
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

            progress = 0;
            step = (double)barPanel.Width / (double)args.Maximum;
            bar.Width = 0;
            pctLabel.Text = $"0%";

            Refresh();
        }

    }
}
