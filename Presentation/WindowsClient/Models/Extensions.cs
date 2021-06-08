using Rems.Application.Common;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public static class Extensions
    {
        /// <summary>
        /// Adds data to a chart
        /// </summary>
        /// <param name="series">The data</param>
        /// <param name="chart">The chart</param>
        /// <param name="vertical">If the data is ordered vertically, not horizontally</param>
        public static void AddToChart<TX, TY>(this SeriesData<TX, TY> series, Chart chart, bool vertical = false)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;            

            Points points = new();
            points.Legend.Text = series.Name;

            Line line = new();
            line.Legend.Visible = false;

            if (vertical)
            {
                points.XValues.Order = ValueListOrder.None;
                points.YValues.Order = ValueListOrder.Ascending;
                line.XValues.Order = ValueListOrder.None;
                line.YValues.Order = ValueListOrder.Ascending;
            }

            if (typeof(TX) == typeof(DateTime))
            {
                points.XValues.DateTime = true;
                line.XValues.DateTime = true;
            }

            for (int i = 0; i < series.X.Length; i++)
            {
                var x = series.X[i];
                var y = series.Y[i];

                if (x is DateTime x1 && y is double y1)
                {
                    line.Add(x1, y1);
                    points.Add(x1, y1);
                }
                else if (x is double x2 && y is int y2)
                {
                    line.Add(x2, y2);
                    points.Add(x2, y2);
                }
                else
                    throw new Exception("Unrecognised series data type");
            }            

            chart.Series.Add(line);
            chart.Series.Add(points);

            line.Color = points.Color;
        }
        
        /// <summary>
        /// Override the formatting of columns in a <see cref="DataGridView"/>
        /// </summary>
        public static void Format(this DataGridView grid, int selectedColumn = -1)
        {
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

                if (column.Index == selectedColumn)
                    style.BackColor = SystemColors.Highlight;

                column.DefaultCellStyle = style;
            }
        }
    }
}
