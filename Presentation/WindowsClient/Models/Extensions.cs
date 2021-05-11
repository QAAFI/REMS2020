using Rems.Application.Common;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using System;
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
        public static void AddToChart<TX, TY>(this SeriesData<TX, TY> series, Chart chart)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;            

            Points points = new Points();
            points.Legend.Text = series.Name;

            Line line = new Line();
            line.Legend.Visible = false;

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
        public static void Format(this DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.DisplayIndex = column.Index;

                // Set doubles to be right aligned, 3 decimal places
                if (column.ValueType == typeof(double))
                    column.DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N3"
                    };
                // Set integers to be right aligned, 0 decimal places
                else if (column.ValueType == typeof(int) || column.ValueType == typeof(long))
                    column.DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "N0"
                    };
                // Set the default alignment to be left aligned
                else
                    column.DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleLeft,
                    };
            }
        }
    }
}
