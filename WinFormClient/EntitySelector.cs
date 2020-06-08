using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class EntitySelector : Form
    {
        public string Selection => combo.SelectedItem.ToString();

        public EntitySelector(string name, string[] options)
        {
            InitializeComponent();

            instructions.Text = "Could not find \"" + name + "\", please select an alternative:";

            combo.Items.AddRange(options);

            FormClosed += OnFormClosed;
            confirmBtn.Click += OnConfirmClick;
            cancelBtn.Click += OnCancelClick;
        }

        private void OnConfirmClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosed -= OnFormClosed;
            confirmBtn.Click -= OnConfirmClick;
        }
    }
}
