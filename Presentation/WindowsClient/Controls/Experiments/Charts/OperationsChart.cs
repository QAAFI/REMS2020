using Rems.Application.Common;
using Rems.Application.CQRS;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    /// <summary>
    /// Manages the presentation of operation data for a treatment
    /// </summary>
    public partial class OperationsChart : UserControl, ITreatmentControl
    {
        /// <inheritdoc/>
        public int Experiment { get; set; }

        /// <inheritdoc/>
        public int Treatment { get; set; }

        /// <summary>
        /// The ID of the currently displayed treatment
        /// </summary>
        public int TreatmentID { get; private set; }

        private Chart chart => tChart.Chart;

        public OperationsChart()
        {
            InitializeComponent();
            Format();
        }

        private void Format()
        {
            // General options
            chart.Header.Text = "Operations";
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
        /// Sets the default style of the chart
        /// </summary>
        public async Task Initialise(int experiment)
        {
            // No async initialisation required
            await Task.Delay(0);
        }

        /// <summary>
        /// Updates the displayed data for a new treatment
        /// </summary>
        /// <param name="id">The treatment ID</param>
        public async Task LoadTreatment(int id)
        {
            // Don't need to update if the ID matches
            if (id == TreatmentID) return;

            TreatmentID = id;

            var set = await QueryManager.Request(new OperationsQuery { TreatmentId = id });

            chart.Series.Clear();
            chart.Axes.Custom.Clear();       

            for (int i = 0; i < set.Tables.Count; i++)
                chart.Series.Add(CreateBarPlot(set.Tables[i], i));

            chart.Draw();
        }

        /// <summary>
        /// Creates a bar plot at the given position
        /// </summary>
        /// <param name="data">The data to plot</param>
        /// <param name="pos">The position of the plot</param>
        private Bar CreateBarPlot(DataTable data, int pos)
        {
            int margin = 5 * pos;
            int start = 30 * pos + margin;
            int end = start + 30;

            var title = new AxisTitle()
            {
                Text = data.TableName + "\n" + data.ExtendedProperties["Subname"].ToString(),
                Angle = 90
            };
            title.Font.Size = 8;

            int increment = 1;
            var ys = data.Rows.Cast<DataRow>().Select(r => r.Field<double>("Value"));

            if (ys.Any())
                increment = Convert.ToInt32(Math.Floor(ys.Max() / 30)) * 10;

            var pen = new ChartPen(chart)
            {
                Visible = true,
                Width = 2,
                Color = Color.Black
            };

            var y = new Axis(chart)
            {
                Title = title,
                StartPosition = start,
                EndPosition = end,
                MinorTickCount = 1,
                Increment = increment,
                AxisPen = pen
            };
            y.MinorGrid.Visible = true;
            y.MinorGrid.Color = Color.LightGray;

            var b = new Axis(chart)
            {
                AxisPen = pen,
                Horizontal = true,
                Visible = true,
                RelativePosition = start
            };

            chart.Axes.Custom.Add(y);
            chart.Axes.Custom.Add(b);

            // Data
            Bar bar = new()
            {
                CustomBarWidth = 4,
                CustomVertAxis = y,
                Title = data.TableName
            };
            bar.Marks.Visible = false;

            foreach (DataRow row in data.Rows)            
                bar.Add(row.Field<DateTime>("Date"), row.Field<double>("Value"));

            // X-Axis
            bar.XValues.DateTime = true;

            // Legend            
            bar.Legend.Visible = false;

            return bar;
        }
    }
}
