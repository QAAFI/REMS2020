using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages a collection of colored text
    /// </summary>
    public class Advice
    {
        /// <summary>
        /// If advice has no text
        /// </summary>
        public bool Empty => !Message.Any();

        private readonly List<(string, Color)> Message = new();

        public Advice()
        { }

        public Advice(string message) : base()
        {
            Include(message);
        }

        /// <summary>
        /// Adds a new message to the advice
        /// </summary>
        /// <param name="text">The content of the message</param>
        /// <param name="color">The display color of the message</param>
        public void Include(string text, Color? color = null)
            => Message.Add((text, color.GetValueOrDefault(Color.Black)));

        /// <summary>
        /// Displays the advice in the given <see cref="RichTextBox"/>
        /// </summary>
        /// <param name="box">The control to display the advice in</param>
        public void AddToTextBox(RichTextBox box)
        {
            box.Clear();

            foreach (var (text, color) in Message)
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
        }

        /// <summary>
        /// Clears the advice
        /// </summary>
        public void Clear() => Message.Clear();
    }
}
