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

            message.Font = FontManager.GetFont("Cascadia Mono Light", Font.Size);

            btnOk.Click += OnOkClick;
            btnCancel.Click += OnCancelClick;
        }

        public static DialogResult Show(string message, AlertType alert, string title = "REMS2020", bool cancel = false)
        {
            Image image = alert switch
            {
                AlertType.Success => Properties.Resources.ValidOn,
                AlertType.Error => Properties.Resources.InvalidOn,
                _ => Properties.Resources.Question,
            };

            var box = new AlertBox
            {
                Text = title,
                Message = message,
                Image = image,
                StartPosition = FormStartPosition.CenterParent                
            };
            box.btnCancel.Visible = cancel;

            return box.ShowDialog();
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
