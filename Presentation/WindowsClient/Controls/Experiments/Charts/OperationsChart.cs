using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Steema.TeeChart.Axis;

namespace WindowsClient.Controls
{
    public partial class OperationsChart : UserControl
    {
        public event QueryHandler DataRequested;

        public int TreatmentID { get; private set; }

        private Chart chart => tChart.Chart;

        public OperationsChart()
        {
            InitializeComponent();

            tChart.Text = "Operations";
            chart.Panel.MarginUnits = PanelMarginUnits.Pixels;
            chart.Panel.MarginLeft = 70;
            chart.Panel.MarginRight = 30;
            chart.Panel.MarginBottom = 90;
        }

        public async Task UpdateData(int id)
        {
            // Don't need to update if the ID matches
            if (id == TreatmentID) return;

            TreatmentID = id;

            var iData = await DataRequested.Send(new IrrigationDataQuery { TreatmentId = id });
            var fData = await DataRequested.Send(new FertilizationDataQuery{ TreatmentId = id });
            var tData = await DataRequested.Send(new TillagesDataQuery{ TreatmentId = id });

            chart.Series.Clear();
            chart.Axes.Custom.Clear();

            var pen = new AxisLinePen(chart)
            {
                Visible = true,
                Color = Color.Black,
                Width = 2
            };
            chart.Axes.Right.AxisPen = pen;
            chart.Axes.Right.Visible = true;

            var x = new Axis(chart)
            {
                Title = new AxisTitle() { Text = "Date" },
                AutomaticMaximum = true,
                AutomaticMinimum = true,
                Horizontal = true,
            };
            x.Labels.DateTimeFormat = "MMM-dd";
            x.Labels.Angle = 60;
            x.Ticks.Visible = true;
            x.MinorGrid.Visible = true;
            x.MinorGrid.Color = Color.LightGray;
            x.Grid.Visible = true;

            var y = new Axis(chart)
            {
                AxisPen = pen
            };

            chart.Axes.Custom.Add(x);
            chart.Axes.Custom.Add(y);

            chart.Series.Add(CreateBar(iData, x, 0));
            chart.Series.Add(CreateBar(fData, x, 1));
            chart.Series.Add(CreateBar(tData, x, 2));

            chart.Draw();
        }

        private Bar CreateBar(SeriesData data, Axis x, int pos)
        {
            int margin = 5 * pos;
            int start = 30 * pos + margin;
            int end = start + 30;

            var title = new AxisTitle()
            {
                Text = data.Title + " " + data.YLabel,
                Angle = 90
            };
            title.Font.Size = 8;

            int increment = 1;
            var ys = data.Y.Cast<double>();

            if (ys.Any())
                increment = Convert.ToInt32(Math.Floor(ys.Max() / 30)) * 10;

            var y = new Axis(chart)
            {
                Title = title,
                StartPosition = start,
                EndPosition = end,
                MinorTickCount = 1,
                Increment = increment
            };
            y.MinorGrid.Visible = true;
            y.MinorGrid.Color = Color.LightGray;

            var b = new Axis(chart)
            {
                AxisPen = new AxisLinePen(chart) { Visible = true, Width = 2, Color = Color.Black },
                Horizontal = true,
                Visible = true,
                RelativePosition = start
            };

            chart.Axes.Custom.Add(y);
            chart.Axes.Custom.Add(b);

            // Data
            Bar bar = new Bar()
            {
                CustomBarWidth = 4,
                CustomHorizAxis = x,
                CustomVertAxis = y,
                Title = data.Title
            };
            bar.Marks.Visible = false;

            bar.Add(data.X, data.Y);

            // X-Axis
            bar.XValues.DateTime = true;

            // Legend            
            bar.Legend.Visible = false;

            return bar;
        }
    }
}
