using Rems.Application.Common;
using Rems.Application.CQRS;
using Steema.TeeChart.Styles;
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
        public static ColourLookup Colours = new();

        public static async Task IterateTraits<TX, TY>(this TraitDataQuery<TX, TY> query, IEnumerable<string> traits, Action<SeriesData<TX, TY>> action)
        {
            foreach (string trait in traits)
            {
                query.TraitName = trait;
                var input = await QueryManager.Request(query);
                action(input);
            }
        }

        /// <summary>
        /// Adds data to a chart
        /// </summary>
        /// <param name="data">The data</param>
        /// <param name="chart">The chart</param>
        /// <param name="vertical">If the data is ordered vertically, not horizontally</param>
        public static C CreateSeries<C, TX, TY>(this SeriesData<TX, TY> data, bool vertical = false)
            where C : Series, new()
        {
            if (data.X.Length == 0) 
                return new();

            var series = new C();
            series.Legend.Text = data.Series == 0 ? data.Name : data.Name + " " + data.Series;
            series.Color = Colours.Lookup(data.Name).colour;            
            
            if (vertical)
            {
                series.XValues.Order = ValueListOrder.None;
                series.YValues.Order = ValueListOrder.Ascending;
            }

            if (typeof(TX) == typeof(DateTime))
                series.XValues.DateTime = true;

            for (int i = 0; i < data.X.Length; i++)
            {
                var x = data.X[i];
                var y = data.Y[i];

                if (x is DateTime x1 && y is double y1)                
                    series.Add(x1, y1);
                else if (x is double x2 && y is int y2)
                    series.Add(x2, y2);
                else
                    throw new Exception("Unrecognised series data type");
            }

            return series;
        }        

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
