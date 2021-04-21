using System;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace WindowsClient.Controls
{
    /// <summary>
    /// A progress bar that updates from a progress tracker 
    /// </summary>
    public partial class TrackerBar : UserControl
    {
        /// <summary>
        /// Occurs when the progress tracker begins its task
        /// </summary>
        public event EventHandler TaskBegun;

        /// <summary>
        /// The text on the button
        /// </summary>
        public string ButtonText
        {
            get => runBtn.Text;
            set => runBtn.Text = value;
        }

        /// <summary>
        /// If the current task is displayed
        /// </summary>
        public bool DisplayTask
        {
            get => label.Visible;
            set => label.Visible = value;
        }

        /// <summary>
        /// The total tasks being tracked
        /// </summary>
        private int tasks;

        /// <summary>
        /// The currently running task
        /// </summary>
        private int task = 0;        

        /// <summary>
        /// The step size to increment the bar by
        /// </summary>
        private double step;

        /// <summary>
        /// The total progress of the bar
        /// </summary>
        private double progress;

        public TrackerBar()
        {
            InitializeComponent();

            bar.Width = 0;
        }

        /// <summary>
        /// Clears the current progress
        /// </summary>
        public void Reset()
        {
            if (InvokeRequired)
                Invoke(new Action(Reset));
            else
            {
                bar.Width = 0;
                label.Text = "";
                pctLabel.Text = "0%";
                task = 0;
                progress = 0;
            }
        }

        /// <summary>
        /// Initialise the step size from the tracker
        /// </summary>
        /// <param name="tracker"></param>
        public void SetSteps(IProgressTracker tracker)
        {
            tasks = tracker.Items;
            bar.Width = 0;
            step = ((double)barPanel.Width) / tracker.Steps;
        }

        /// <summary>
        /// When the tracker fails its current task
        /// </summary>
        public void OnTaskFailed(object sender, Args<Exception> args)
        {
            var error = args.Item;
            while (error.InnerException != null) error = error.InnerException;
            MessageBox.Show(error.Message, "Import failed!");
            Reset();
        }

        /// <summary>
        /// When the current task makes progress
        /// </summary>
        public void OnProgressChanged(object sender, EventArgs args)
        {
            if (InvokeRequired)
                Invoke(new EventHandler(OnProgressChanged), sender, args);
            else
            {
                progress += step;
                bar.Width = Convert.ToInt32(progress);

                int pct = Math.Min(100, 100 * bar.Width / barPanel.Width);
                pctLabel.Text = $"{pct}%";

                Refresh();
            }
        }

        /// <summary>
        /// Move to the next task
        /// </summary>
        public void OnNextTask(object sender, Args<string> args)
        {
            string text = args.Item;

            if (InvokeRequired)
                Invoke(new EventHandler<Args<string>>(OnNextTask), sender, args);
            else
            {
                task++;
                label.Text = $"{task} of {tasks}: {text}";

                Refresh();
            }
        }

        /// <summary>
        /// Invokes the TaskBegun event when the run button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunClicked(object sender, EventArgs e) 
            => TaskBegun?.Invoke(this, EventArgs.Empty);
    }
}
