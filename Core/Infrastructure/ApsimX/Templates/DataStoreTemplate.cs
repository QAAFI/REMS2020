using System;
using System.Threading.Tasks;
using Rems.Application.Common.Interfaces;
using Models.Core;
using Models.Storage;
using Models.PostSimulationTools;

namespace Rems.Infrastructure.ApsimX
{
    public class DataStoreTemplate : ITemplate
    {
        readonly IFileManager manager = FileManager.Instance;

        readonly string Name;

        public DataStoreTemplate(string name)
        {
            Name = name;
        }

        public IModel Create()
        {
            var store = new DataStore();
            var input = new Input
            {
                Name = "Observed",
                FileNames = new string[] { $"{manager.ExportPath}\\obs\\{Name}_Observed.csv" }
            };
            store.Children.Add(input);

            var po = new PredictedObserved
            {
                ObservedTableName = "Observed",
                PredictedTableName = "DailyReport",
                FieldNameUsedForMatch = "Date",
                AllColumns = true
            };

            store.Children.Add(po);

            return store;
        }

        public async Task<IModel> AsyncCreate() => await Task.Run(Create);
    }
}
