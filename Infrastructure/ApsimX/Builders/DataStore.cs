using Models.PostSimulationTools;
using Models.Storage;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static DataStore BuildDataStore()
        {
            var store = new DataStore();

            var excelObserved = new ExcelInput()
            {
                Name = "Observed",
                FileName = "Observed.xlsx",
                SheetNames = new string[] { "Observed" }
            };

            var excelDailyObserved = new ExcelInput()
            {
                Name = "DailyObserved",
                FileName = "DailyObserved.xlsx",
                SheetNames = new string[] { "DailyObserved" }
            };

            var observed = new PredictedObserved()
            {
                PredictedTableName = "HarvestReport",
                ObservedTableName = "Observed",
                FieldNameUsedForMatch = "SimulationID",
                Name = "PredictedObserved"
            };

            var dailyObserved = new PredictedObserved()
            {
                PredictedTableName = "DailyReport",
                ObservedTableName = "DailyObserved",
                FieldNameUsedForMatch = "SimulationID",
                Name = "DailyPredictedObserved"
            };

            store.Children.Add(excelObserved);
            store.Children.Add(excelDailyObserved);
            store.Children.Add(observed);
            store.Children.Add(dailyObserved);

            return store;
        }
    }
}
