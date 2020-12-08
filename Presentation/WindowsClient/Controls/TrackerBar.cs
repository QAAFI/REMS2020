using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common.Interfaces;
using System.Threading;
using Rems.Application.Common;

namespace WindowsClient.Controls
{
    public partial class TrackerBar : UserControl
    {
        public event Action TaskBegun;

        private int item = 0;
        private int items;

        private double step;
        private double progress;

        public TrackerBar()
        {
            InitializeComponent();

            bar.Width = 0;
        }

        public void SetSteps(IProgressTracker tracker)
        {
            items = tracker.Items;
            bar.Width = 0;
            step = ((double)barPanel.Width) / tracker.Steps;
        }

        public void OnTaskFailed(Exception error)
        {
            while (error.InnerException != null) error = error.InnerException;
            MessageBox.Show(error.Message, "Import failed!");
        }

        public void OnProgressChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(OnProgressChanged));
                return;
            }

            progress += step;
            bar.Width = Convert.ToInt32(progress);

            int pct = Math.Min(100, 100 * bar.Width / barPanel.Width);
            pctLabel.Text = $"{pct}%";

            Refresh();
        }

        public void OnNextItem(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new StringSender(OnNextItem));
                return;
            }

            item++;
            label.Text = $"{item} of {items}: {text}";

            Refresh();
        }

        private void RunClicked(object sender, EventArgs e) => TaskBegun?.Invoke();

    }
}
