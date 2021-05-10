using MediatR;
using Rems.Application.Common;
using Rems.Application.CQRS;
using Steema.TeeChart;
using Steema.TeeChart.Styles;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;
using static Steema.TeeChart.Axis;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of operation data for a treatment
    /// </summary>
    public partial class OperationsChart : UserControl
    {
        /// <summary>
        /// The ID of the currently displayed treatment
        /// </summary>
        public int TreatmentID { get; private set; }

        private Chart chart => tChart.Chart;

        public OperationsChart()
        {
            InitializeComponent();
            InitialiseChart();            
        }

        /// <summary>
        /// Sets the default style of the chart
        /// </summary>
        private void InitialiseChart()
        {
            // General options
            tChart.Text = "Operations";
            chart.Panel.MarginUnits = PanelMarginUnits.Pixels;
            chart.Panel.MarginLeft = 70;
            chart.Panel.MarginRight = 15;
            chart.Panel.MarginBottom = 10;

            // X-Axis options
            chart.Axes.Bottom.Title = new AxisTitle() { Text = "Date" };
            chart.Axes.Bottom.AutomaticMaximum = true;
            chart.Axes.Bottom.AutomaticMinimum = true;
            chart.Axes.Bottom.Horizontal = true;
            chart.Axes.Bottom.Labels.DateTimeFormat = "MMM-dd";
            chart.Axes.Bottom.Labels.Angle = 60;
            chart.Axes.Bottom.Ticks.Visible = true;
            chart.Axes.Bottom.MinorGrid.Visible = true;
            chart.Axes.Bottom.MinorGrid.Color = Color.LightGray;
            chart.Axes.Bottom.Grid.Visible = true;
        }

        /// <summary>
        /// Updates the displayed data for a new treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task UpdateData(int id)
        {
            // Don't need to update if the ID matches
            if (id == TreatmentID) return;

            TreatmentID = id;

            var iData = await QueryManager.Request(new IrrigationDataQuery { TreatmentId = id });
            var fData = await QueryManager.Request(new FertilizationDataQuery{ TreatmentId = id });
            var tData = await QueryManager.Request(new TillagesDataQuery{ TreatmentId = id });

            chart.Series.Clear();
            chart.Axes.Custom.Clear();            

            chart.Series.Add(CreateBarPlot(iData, 0));
            chart.Series.Add(CreateBarPlot(fData, 1));
            chart.Series.Add(CreateBarPlot(tData, 2));

            chart.Draw();
        }

        /// <summary>
        /// Creates a bar plot at the given position
        /// </summary>
        /// <param name="data">The data to plot</param>
        /// <param name="pos">The position of the plot</param>
        private Bar CreateBarPlot(SeriesData data, int pos)
        {
            int margin = 5 * pos;
            int start = 30 * pos + margin;
            int end = start + 30;

            var title = new AxisTitle()
            {
                Text = data.Name + "\n" + data.YName,
                Angle = 90
            };
            title.Font.Size = 8;

            int increment = 1;
            var ys = data.Y.Cast<double>();

            if (ys.Any())
                increment = Convert.ToInt32(Math.Floor(ys.Max() / 30)) * 10;

            //var pen = new AxisLinePen(chart) 
            //{ 
            //    Visible = true, 
            //    Width = 2, 
            //    Color = Color.Black
            //};

            var y = new Axis(chart)
            {
                Title = title,
                StartPosition = start,
                EndPosition = end,
                MinorTickCount = 1,
                Increment = increment,
                //AxisPen = pen
            };
            y.MinorGrid.Visible = true;
            y.MinorGrid.Color = Color.LightGray;

            var b = new Axis(chart)
            {
                //AxisPen = pen,
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
                CustomVertAxis = y,
                Title = data.Name
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
