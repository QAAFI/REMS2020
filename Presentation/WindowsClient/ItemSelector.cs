using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rems.Application;

namespace WindowsClient
{
    public partial class ItemSelector : Form
    {
        private ItemNotFoundArgs args;

        public ItemSelector(ItemNotFoundArgs args)
        {
            this.args = args;

            InitializeComponent();

            instructions.Text = "Could not find \"" + args.Name + "\", please select an alternative:";

            combo.Items.AddRange(args.Options);

            FormClosed += OnFormClosed;
            confirmBtn.Click += OnConfirmClick;
            cancelBtn.Click += OnCancelClick;
        }

        private void OnConfirmClick(object sender, EventArgs e)
        {
            args.Selection = combo.SelectedItem.ToString();
            Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            if (e is ItemNotFoundArgs args) args.Cancelled = true;
            Close();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosed -= OnFormClosed;
            confirmBtn.Click -= OnConfirmClick;
        }
    }
}
