using Rems.Application.CQRS;
using Steema.TeeChart.Styles;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class MetChart : TraitChart
    {
        public MetChart() : base()
        {
            TraitType = "Climate";
        }

        public override async Task LoadPlots()
        {
            chart.Series.Clear();

            foreach (var trait in traits)
            {
                if (!charts.Tables.Contains($"{Treatment}_{trait}"))
                {
                    var query = new MetDataQuery { TreatmentId = Treatment, TraitName = trait };
                    charts.Tables.Add(await QueryManager.Request(query));
                }

                var table = charts.Tables[$"{Treatment}_{trait}"];

                var rows = table.Rows.Cast<DataRow>();

                if (!rows.Any())
                    continue;

                var pairs = rows.Select(r => (Convert.ToDateTime(r["Date"]), (double)r["Mean"]));

                var b = new Bar();
                var l = new Line(chart);

                b.XValues.DateTime = true;
                l.XValues.DateTime = true;
                b.Marks.Visible = false;
                b.CustomBarWidth = 3;

                b.Color = l.Color = Extensions.Colours.Lookup(trait).colour;

                foreach (var pair in pairs)
                {
                    b.Add(pair.Item1, pair.Item2);
                    l.Add(pair.Item1, pair.Item2);
                }

                l.Legend.Text = trait;
                b.Legend.Visible = false;

                //chart.Series.Add(b);
                chart.Series.Add(l);
            }

            chart.Axes.Bottom.Title.Text = "Date";
            chart.Axes.Left.Title.Text = "Value";
            chart.Legend.Title.Text = descriptions[0]?.WordWrap(18) ?? "";

            chart.Legend.Width = 120;

            chart.Header.Text = "Average annual weather data";
        }
    }
}
