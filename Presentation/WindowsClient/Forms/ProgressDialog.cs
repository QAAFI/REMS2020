using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using System;

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            step = ((double)barPanel.Width) / tracker.Steps;

            Show();

            tracker.NextItem += OnNextItem;
            tracker.IncrementProgress += OnProgressChanged;
            tracker.TaskFinished += OnTaskFinished;
            tracker.TaskFailed += OnTaskFailed;

            tracker.Run();
        }

        private void OnTaskFailed(Exception error)
        {
            while (error.InnerException != null) error = error.InnerException;
            MessageBox.Show(error.Message, "Import failed!");

            Close();
        }

        private void OnProgressChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnProgressChanged));
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

        private void OnTaskFinished()
        {
            Thread.Sleep(1500);

            Close();

            MessageBox.Show("No errors encountered.", "Task complete!");

            TaskComplete?.Invoke();
        }

        private void OnNextItem(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new NextItemHandler(OnNextItem));
            }
            else
            {
                item++;
                label.Text = $"{item} of {items}: {text}";

                Refresh();
            }
        }

    }
}
