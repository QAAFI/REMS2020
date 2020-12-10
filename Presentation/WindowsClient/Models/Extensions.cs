using Rems.Application.Common;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
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

        public static void AddToChart(this SeriesData series, Chart chart)
        {
            if (series is null) return;
            if (series.X.Length == 0) return;            

            Points points = new Points();
            points.Legend.Text = series.Title;

            Line line = new Line();
            line.Legend.Visible = false;

            points.XValues.DateTime = true;
            line.XValues.DateTime = true;

            line.Add(series.X, series.Y);
            points.Add(series.X, series.Y);

            chart.Series.Add(line);
            chart.Series.Add(points);

            line.Color = points.Color;
        }
    }
}
