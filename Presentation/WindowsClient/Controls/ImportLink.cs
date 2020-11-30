using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Controls
{
    public enum Stage
    {
        Missing,
        Validation,
        Imported
    }

    public partial class ImportLink : UserControl
    {
        public event EventHandler Clicked;

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
            set => label.Text = value;
        }

        public Image Image
        {
            get => image.BackgroundImage;
            set => image.BackgroundImage = value;
        }

        public ImageList Images { get; }

        public Importer Importer { get; } = new Importer();

        public ImportLink()
        {
            InitializeComponent();

            Images = new ImageList();

            Images.Images.Add("Imported", Properties.Resources.ValidOn);
            Images.Images.Add("Missing", Properties.Resources.InvalidOn);
            Images.Images.Add("Validation", Properties.Resources.WarningOn);

            SetStage(Stage.Missing);
        }

        private void SetStage(Stage _stage)
        {
            stage = _stage;

            Image = Images.Images[_stage.ToString()];
        }

        private void OnClick(object sender, EventArgs e) => Clicked?.Invoke(this, EventArgs.Empty);
    }
}
