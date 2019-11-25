using System.IO;

using Models.Core;
using Models.PMF;
using Models.Report;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public Replacements BuildReplacements()
        {
            var replacements = new Replacements() { Name = "Replacements" };

            var sorghumModel = Path.Combine("DataFiles", "apsimx", "Sorghum.json");
            var sorghum = JsonTools.LoadJson<Plant>(sorghumModel);
            replacements.Children.Add(sorghum);

            var daily = new Report()
            {
                Name = "DailyReport",
                VariableNames = DataFiles.ReadRawText("Daily.txt").Split("\n"),
                EventNames = new string[] { "[Clock].DoReport" }
            };

            var harvest = new Report()
            {
                Name = "HarvestReport",
                VariableNames = DataFiles.ReadRawText("Harvest.txt").Split("\n"),
                EventNames = new string[] { "[Sorghum].Harvesting" }
            };

            replacements.Children.Add(daily);
            replacements.Children.Add(harvest);

            return replacements;
        }
    }
}
