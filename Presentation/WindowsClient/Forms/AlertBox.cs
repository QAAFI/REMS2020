using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsClient.Forms
{
    public enum AlertType
    {
        Success,
        Ok,
        Error
    }

    public partial class AlertBox : Form
    {
        public Image Image
        {
            get => icon.BackgroundImage;
            set => icon.BackgroundImage = value;
        }

        public string Message
        {
            get => message.Text;
            set => message.Text = value;
        }

        private AlertBox()
        {
            InitializeComponent();

            btnOk.Click += OnOkClick;
        }        

        public static DialogResult Show(string message, AlertType alert, string title = "REMS2020")
        {
            Image image;
            switch (alert)
            {
                case AlertType.Success:
                    image = Properties.Resources.ValidOn;
                    break;

                case AlertType.Error:
                    image = Properties.Resources.InvalidOn;
                    break;

                case AlertType.Ok:
                default:
                    image = Properties.Resources.Question;
                    break;
            }

            var box = new AlertBox
            {
                Text = title,
                Message = message,
                Image = image,
                StartPosition = FormStartPosition.CenterParent
            };
            return box.ShowDialog();
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
