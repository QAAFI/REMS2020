using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsClient.Controls
{
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
        /// Occurs when the importer has finished
        /// </summary>
        public event Action<ImportLink> ImportComplete;

        /// <summary>
        /// Occurs when the importer has changed stage
        /// </summary>
        public event Action<ImportLink> StageChanged;

        /// <summary>
        /// The currently selected file to import
        /// </summary>
        public string File { get; set; }

        private Stage stage;
        /// <summary>
        /// The current stage of the import process
        /// </summary>
        public Stage Stage
        {
            get => stage;
            set => SetStage(value);
        }

        /// <summary>
        /// The link text
        /// </summary>
        public string Label
        {
            get => label.Text;
            set => UpdateLabel(value);
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

        /// <summary>
        /// The tab page created by the link
        /// </summary>
        public TabPage Tab { get; } = new TabPage();

        /// <summary>
        /// The importer used by the tab
        /// </summary>
        public Importer Importer { get; } = new Importer();

        public ImportLink()
        {
            InitializeComponent();

            Images = new ImageList();

            Images.Images.Add("Imported", Properties.Resources.ValidOn);
            Images.Images.Add("Missing", Properties.Resources.InvalidOn);
            Images.Images.Add("Validation", Properties.Resources.WarningOn);

            SetStage(Stage.Missing);

            Tab.Controls.Add(Importer);
            Importer.StageChanged += SetStage;
            Importer.FileChanged += SetFile;
            Importer.FileImported += OnImportFinished;

            label.Click += OnClick;
            label.MouseEnter += LabelMouseEnter;
            label.MouseLeave += LabelMouseLeave;
        }

        /// <summary>
        /// Invokes the ImportComplete event
        /// </summary>
        private void OnImportFinished() => ImportComplete?.Invoke(this);

        /// <summary>
        /// Invokes the Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e) => Clicked?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Changes the label colour on mouse enter
        /// </summary>
        private void LabelMouseEnter(object sender, EventArgs e) => label.ForeColor = SystemColors.MenuHighlight;

        /// <summary>
        /// Changes the label colour on mouse leave
        /// </summary>
        private void LabelMouseLeave(object sender, EventArgs e) => label.ForeColor = SystemColors.HotTrack;

        /// <summary>
        /// Sets the current stage of the link
        /// </summary>
        private void SetStage(Stage _stage)
        {
            stage = _stage;

            Image = Images.Images[_stage.ToString()];

            StageChanged?.Invoke(this);
        }

        /// <summary>
        /// Sets the file to import
        /// </summary>
        private void SetFile(string text) => File = Path.GetFileName(text);

        /// <summary>
        /// Changes the label text
        /// </summary>
        private void UpdateLabel(string text)
        {
            label.Text = text;
            Tab.Text = "Import " + text;
        }

    }
}
