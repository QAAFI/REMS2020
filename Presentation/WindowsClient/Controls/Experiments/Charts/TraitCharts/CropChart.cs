using Rems.Application.Common;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class CropChart : TraitChart
    {
        public CropChart() : base()
        {
            TraitType = "Crop";
        }

        public override async Task LoadPlots()
        {
            string xtitle = "";
            string ytitle = "";

            List<SeriesData<DateTime, double>> datas = new();
            Action<SeriesData<DateTime, double>> action = data =>
            {
                datas.Add(data);
                xtitle = data.XName;
                ytitle = data.YName;
            };

            if (selected.ToString() == "All")
                foreach (var plot in await QueryManager.Request(new PlotsQuery { TreatmentId = Treatment }))
                    await new PlotDataByTraitQuery
                    {
                        TraitName = "",
                        PlotId = plot.Key
                    }.IterateTraits(traits, action);

            else if (selected.ToString() == "Mean")
                await new MeanCropTraitDataQuery
                {
                    TraitName = "",
                    TreatmentId = Treatment
                }.IterateTraits(traits, action);

            else if (selected is PlotDTO plot)
                await new PlotDataByTraitQuery
                {
                    TraitName = "",
                    PlotId = plot.ID
                }.IterateTraits(traits, action);

            chart.Series.Clear();
            datas.ForEach(d => d.AddToSeries(chart.Series));

            chart.Axes.Bottom.Title.Text = xtitle;
            chart.Axes.Left.Title.Text = ytitle;
            chart.Legend.Title.Text = descriptions[0]?.WordWrap(18) ?? "";

            chart.Legend.Width = 120;

            chart.Header.Text = await QueryManager.Request(new TreatmentDesignQuery { TreatmentId = Treatment });

        }
    }
}
