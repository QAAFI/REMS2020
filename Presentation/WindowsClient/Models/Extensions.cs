using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Forms;

namespace WindowsClient.Models
{
    public static class Extensions
    {
        /// <summary>
        /// Override the formatting of columns in a <see cref="DataGridView"/>
        /// </summary>
        public static void Format(this DataGridView grid, int selectedColumn = -1)
        {
            grid.ClearSelection();

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.DisplayIndex = column.Index;

                DataGridViewCellStyle style;

                // Set doubles to be right aligned, 3 decimal places
                if (column.ValueType == typeof(double))
                    style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N3"
                    };
                // Set integers to be right aligned, 0 decimal places
                else if (column.ValueType == typeof(int) || column.ValueType == typeof(long))
                    style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N0"
                    };
                // Set the default alignment to be left aligned
                else
                    style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleLeft,
                    };

                column.DefaultCellStyle = style;
            }

            // Highlight the selected column
            if (selectedColumn >= 0)
                foreach (DataGridViewRow row in grid.Rows)                
                    row.Cells[selectedColumn].Selected = true;                
        }

        /// <summary>
        /// Await a task with exception handling
        /// </summary>
        public static async Task TryRun(this Task task)
        {
            Application.UseWaitCursor = true;
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                // Don't want an error message box for cancelled operations
            }
            catch (Exception error)
            {
                while (error.InnerException != null) 
                    error = error.InnerException;

                Application.UseWaitCursor = false;
                AlertBox.Show(error.Message, AlertType.Error);
            }

            Application.UseWaitCursor = false;
        }

        public static string WordWrap(this string text, int length)
        {
            if (text is null)
                return "";

            var builder = new StringBuilder();
            int line = 0;
            foreach (string word in text.Split(' '))
            {
                line += word.Length + 1;

                if (word.Length > length)
                {
                    builder.Append(word.Substring(0, length));

                    var sub = word.Substring(length);
                    builder.Append('\n' + sub + ' ');

                    line = sub.Length + 1;
                }
                else if (line > length)
                {
                    line = word.Length + 1;
                    builder.Append('\n' + word + ' ');
                }
                else
                    builder.Append(word + ' ');
            }

            return builder.ToString();
        }
    }
}
