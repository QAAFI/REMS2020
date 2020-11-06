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
    public partial class LabeledBox : UserControl
    {
        public string Label 
        { 
            get 
            { 
                return label.Text; 
            } 
            set
            {
                label.Text = value;
            }
        }

        public string Content
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public LabeledBox()
        {
            InitializeComponent();

            //label.SizeChanged += LabelSizeChanged;
        }

        //private void LabelSizeChanged(object sender, EventArgs e)
        //{
        //    this.Width = textBox.Width + label.Width + 11;

        //    textBox.Left = label.Right + 3;
        //}
    }
}
