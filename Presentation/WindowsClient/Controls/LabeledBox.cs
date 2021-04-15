using System;
using System.Windows.Forms;

namespace WindowsClient.Controls
{
    /// <summary>
    /// A textbox with a label attached to it
    /// </summary>
    public partial class LabeledBox : UserControl
    {
        /// <summary>
        /// The label text of the box
        /// </summary>
        public string Label 
        { 
            get => label.Text;
            set => label.Text = value;            
        }

        /// <summary>
        /// The text of the box itself
        /// </summary>
        public string Content
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        /// <summary>
        /// If the box is read only
        /// </summary>
        public bool ReadOnly
        {
            get => textBox.ReadOnly;
            set => textBox.ReadOnly = value;
        }

        public LabeledBox()
        {
            InitializeComponent();
        }
    }
}
