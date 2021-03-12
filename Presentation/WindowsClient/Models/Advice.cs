using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    /// <summary>
    /// Stores text and color
    /// </summary>
    public struct RichText
    {
        /// <summary>
        /// The text content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The text color
        /// </summary>
        public Color Color { get; set; }
    }

    /// <summary>
    /// Manages a collection of colored text
    /// </summary>
    public class Advice
    {
        /// <summary>
        /// If advice has no text
        /// </summary>
        public bool Empty => !Message.Any();

        private List<RichText> Message = new List<RichText>();

        /// <summary>
        /// Adds a new message to the advice
        /// </summary>
        /// <param name="text">The content of the message</param>
        /// <param name="color">The display color of the message</param>
        public void Include(string text, Color color) => Message.Add(new RichText { Text = text, Color = color });

        /// <summary>
        /// Displays the advice in the given <see cref="RichTextBox"/>
        /// </summary>
        /// <param name="box">The control to display the advice in</param>
        public void AddToTextBox(RichTextBox box)
        {
            box.Clear();

            foreach (var item in Message)
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = item.Color;
                box.AppendText(item.Text);
                box.SelectionColor = box.ForeColor;
            }
        }

        /// <summary>
        /// Clears the advice
        /// </summary>
        public void Clear() => Message.Clear();
    }
}
