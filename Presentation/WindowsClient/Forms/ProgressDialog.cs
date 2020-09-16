using Rems.Application;
using Rems.Application.Common.Interfaces;
using System;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Rems.Application.EventManager;

namespace WindowsClient.Forms
{
    public partial class ProgressDialog : Form
    {
        public event Action TaskComplete;

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

            tracker.NextProgress += OnNextItem;
            tracker.IncrementProgress += OnProgressChanged;
            tracker.StopProgress += OnRunWorkerCompleted;
            tracker.TaskFailed += OnTaskFailed;

            tracker.Run();
        }

        private void OnTaskFailed(Exception error)
        {
            while (error.InnerException != null) error = error.InnerException;
            MessageBox.Show(error.Message, "Import failed!");

            Close();
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
                bar.Width = Convert.ToInt32(progress);

                int pct = Math.Min(100, 100 * bar.Width / barPanel.Width);
                pctLabel.Text = $"{pct}%";

                Refresh();
            }
        }

        private void OnRunWorkerCompleted(object sender, EventArgs e)
        {
            Thread.Sleep(1500);

            Close();

            MessageBox.Show("No errors encountered.", "Task complete!");

            TaskComplete?.Invoke();
        }

        private void OnNextItem(object sender, NextItemArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new NextItemHandler(OnNextItem));
            }
            else
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
                step = ((double)barPanel.Width) / args.Maximum;
                bar.Width = 0;
                pctLabel.Text = $"0%";

                Refresh();
            }
        }

    }
}
