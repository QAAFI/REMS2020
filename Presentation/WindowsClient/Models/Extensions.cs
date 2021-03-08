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
        public static void AddToChart(this SeriesData series, Chart chart)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;            

            Points points = new Points();
            points.Legend.Text = series.Name;

            Line line = new Line();
            line.Legend.Visible = false;

            if (series.X.GetValue(0) is DateTime)
            {
                points.XValues.DateTime = true;
                line.XValues.DateTime = true;
            }

            line.Add(series.X, series.Y);
            points.Add(series.X, series.Y);

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
