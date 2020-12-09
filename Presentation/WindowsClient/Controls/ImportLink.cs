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

    public delegate void LinkAction(ImportLink link);

    public partial class ImportLink : UserControl
    {
        public event EventHandler Clicked;        
        public event LinkAction ImportComplete;
        public event LinkAction StageChanged;

        public string File { get; set; }

        private Stage stage;
        public Stage Stage
        {
            get => stage;
            set => SetStage(value);
        }

        public string Label
        {
            get => label.Text;
            set => UpdateLabel(value);
        }

        public Image Image
        {
            get => image.BackgroundImage;
            set => image.BackgroundImage = value;
        }

        public ImageList Images { get; }

        public TabPage Tab { get; } = new TabPage();

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

        private void OnImportFinished() => ImportComplete?.Invoke(this);

        private void OnClick(object sender, EventArgs e) => Clicked?.Invoke(this, EventArgs.Empty);

        private void LabelMouseEnter(object sender, EventArgs e) => label.ForeColor = SystemColors.MenuHighlight;

        private void LabelMouseLeave(object sender, EventArgs e) => label.ForeColor = SystemColors.HotTrack;

        private void SetStage(Stage _stage)
        {
            stage = _stage;

            Image = Images.Images[_stage.ToString()];

            StageChanged?.Invoke(this);
        }

        private void SetFile(string text) => File = Path.GetFileName(text);

        private void UpdateLabel(string text)
        {
            label.Text = text;
            Tab.Text = "Import " + text;
        }

    }
}
