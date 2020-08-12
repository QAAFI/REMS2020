using System;
using System.Collections.Generic;
using System.Text;

using Models;
using Models.Core;

using Rems.Application.Queries;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static Folder BuildManagers(SowingQueryDto sowing)
        {
            var folder = new Folder() { Name = "Manager folder" };

            var skiprow = new Manager()
            {
                Name = "Sow SkipRow on a fixed date",
                Code = DataFiles.ReadRawText("SkipRow.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Date", sowing.Date.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>("Density", sowing.Density),
                    new KeyValuePair<string, string>("Depth", sowing.Depth),
                    new KeyValuePair<string, string>("Cultivar", sowing.Cultivar),
                    new KeyValuePair<string, string>("RowSpacing", sowing.RowSpacing),
                    new KeyValuePair<string, string>("RowConfiguration", sowing.RowConfiguration),
                    new KeyValuePair<string, string>("Ftn", sowing.Ftn)
                },
                Enabled = true
            };
            folder.Children.Add(skiprow);

            var harvest = new Manager()
            {
                Name = "Harvesting rule",
                Code = DataFiles.ReadRawText("Harvest.cs.txt"),
                Parameters = new List<KeyValuePair<string, string>>()
            };
            folder.Children.Add(harvest);

            return folder;
        }

    }
}
