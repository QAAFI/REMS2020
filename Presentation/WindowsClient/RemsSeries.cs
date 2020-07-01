using Steema.TeeChart.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient
{
    public class RemsSeries
    {
        public Points Points { get; }

        public Line Line { get; }

        public RemsSeries()
        {
            Points = new Points();
            Points.Color = Color.SkyBlue;
            Points.Legend.Visible = false;
            Points.Pointer.Style = PointerStyles.Circle;

            Line = new Line();
            Line.Color = Color.SkyBlue;
            Line.Legend.Visible = false;
            Line.LinePen.Width = 2;
        }

        public void UpdateValues(IEnumerable<Tuple<object, object>> points)
        {
            Points.Clear();
            Line.Clear();

            foreach (var point in points)
            {
                CastAdd(Points, point.Item1, point.Item2);
                CastAdd(Line, point.Item1, point.Item2);
            }
        }

        private void CastAdd(CustomPoint point, object x, object y)
        {
            if (x is DateTime a && y is DateTime b) point.Add(a, b);
            else if (x is double c && y is DateTime d) point.Add(c, d);
            else if (x is DateTime e && y is double f) point.Add(e, f);
            else point.Add(Convert.ToDouble(x), Convert.ToDouble(y));
        }
    }
}
