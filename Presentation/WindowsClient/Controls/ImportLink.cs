using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsClient.Controls
{
    /// <summary>
    /// The current stage of the import
    /// </summary>
    public enum Stage
    {
        Missing,
        Validation,
        Imported
    }

    /// <summary>
    /// A text link that manages the creation of an importer tab
    /// </summary>
    public partial class ImportLink : UserControl
    {
        /// <summary>
        /// Occurs when the link is clicked
        /// </summary>
        public event EventHandler Clicked;
        
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
        public Stage Stage
        {
            get => stage;
            set => SetStage(value);
        }
        private Stage stage;

        /// <summary>
        /// The link text
        /// </summary>
        public string Label
        {
            get => label.Text;
            set => label.Text = value;
        }

        /// <summary>
        /// The link icon
        /// </summary>
        public Image Image
        {
            get => image.BackgroundImage;
            set => image.BackgroundImage = value;
        }

        /// <summary>
        /// The images used by the link
        /// </summary>
        public ImageList Images { get; }

        public ImportLink()
        {
            InitializeComponent();

            Images = new ImageList();

            Images.Images.Add("Imported", Properties.Resources.ValidOn);
            Images.Images.Add("Missing", Properties.Resources.InvalidOn);
            Images.Images.Add("Validation", Properties.Resources.WarningOn);

            SetStage(Stage.Missing);                       

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
            if (active) Clicked?.Invoke(this, e);
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

            if (active)
                label.ForeColor = SystemColors.HotTrack;
            else
                label.ForeColor = SystemColors.GrayText;
        }

        /// <summary>
        /// Sets the current stage of the link
        /// </summary>
        private void SetStage(Stage _stage)
        {
            stage = _stage;
            Image = Images.Images[_stage.ToString()];
        }
    }
}
