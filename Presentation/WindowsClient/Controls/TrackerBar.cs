using System;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Infrastructure;
using WindowsClient.Forms;

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

        public TrackerBar()
        {
            InitializeComponent();

            bar.Value = 0;
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
                bar.Value = 0;
                label.Text = "";
                task = 0;
            }
        }

        /// <summary>
        /// Initialise the step size from the tracker
        /// </summary>
        /// <param name="runner"></param>
        public void AttachRunner(ITaskRunner runner)
        {
            runner.NextItem += OnNextTask;
            runner.Progress = new Progress<int>(amount => bar.Increment(amount));

            tasks = runner.Items;
            bar.Value = 0;
            bar.Maximum = runner.Steps;
        }

        /// <summary>
        /// Move to the next task
        /// </summary>
        public void OnNextTask(object sender, string item)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<string>(OnNextTask), sender, item);
            else
            {
                task++;
                label.Text = $"{item} {task}/{tasks}";

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
