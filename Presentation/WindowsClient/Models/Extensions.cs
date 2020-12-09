using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public static class Extensions
    {
        public static void AddText(this RichTextBox box, RichText rich)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = rich.Color;
            box.AppendText(rich.Text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AddText(this RichTextBox box, IEnumerable<RichText> riches)
        {
            foreach (var rich in riches)
                box.AddText(rich);
        }
    }
}
