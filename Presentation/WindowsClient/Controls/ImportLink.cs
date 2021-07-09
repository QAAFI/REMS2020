using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsClient.Controls
{
    /// <summary>
    /// A text link that manages the creation of an importer tab
    /// </summary>
    public partial class ImportLink : UserControl
    {
        /// <summary>
        /// Occurs when the link is clicked
        /// </summary>
        public event EventHandler Clicked;

        public bool WasClicked { get; set; } = false;

        /// <summary>
        /// If the link can be clicked
        /// </summary>
        public bool Active
        {
            get => active;
            set => ToggleEnabled(value);
        }
        private bool active = false;
        
        /// <summary>
        /// The current stage of the import process
        /// </summary>
        public bool HasData 
        {
            get => hasData; 
            set
            {
                hasData = value;
                image.BackgroundImage = value ? Properties.Resources.ValidOn : Properties.Resources.InvalidOn;
            }
        }
        private bool hasData = false;

        /// <summary>
        /// The link text
        /// </summary>
        public string Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        public ImportLink()
        {
            InitializeComponent();

            image.BackgroundImage = Properties.Resources.InvalidOn;

            label.Click += OnClick;
            label.MouseEnter += LabelMouseEnter;
            label.MouseLeave += LabelMouseLeave;
        }

        /// <summary>
        /// Invokes the Clicked event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            if (active)
            {
                WasClicked = true;
                Clicked?.Invoke(this, e);
            }
        }        

        /// <summary>
        /// Changes the label colour on mouse enter
        /// </summary>
        private void LabelMouseEnter(object sender, EventArgs e)
        {
            if (active)
            {
                label.Cursor = Cursors.Hand;
                label.ForeColor = SystemColors.MenuHighlight;
            }
            else
                label.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Changes the label colour on mouse leave
        /// </summary>
        private void LabelMouseLeave(object sender, EventArgs e)
        {
            if (active)
                label.ForeColor = SystemColors.HotTrack;
        }

        /// <summary>
        /// Toggles the active status of the link
        /// </summary>
        private void ToggleEnabled(bool value)
        {
            active = value;
            label.ForeColor = active ? SystemColors.HotTrack : SystemColors.GrayText;
        }
    }
}
