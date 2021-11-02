using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using Models;
using Models.Core;
using Models.Core.ApsimFile;
using Models.PostSimulationTools;
using Models.Storage;

using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;

namespace Rems.Infrastructure.ApsimX
{
    /// <summary>
    /// Manages the construction and output of an .apsimx file
    /// </summary>
    public class ApsimXporter : TaskRunner
    {
        /// <summary>
        /// The name of the .apsimx to output to
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The names of the experiments in the database to export
        /// </summary>
        public IEnumerable<string> Experiments { get; set; }

        public IFileManager Manager { get; set; }

        /// <inheritdoc/>
        public override int Items => Experiments.Count();

        /// <inheritdoc/>
        public override int Steps => Items * numModelsToExport;

        private readonly int numModelsToExport = 13;

        public Markdown Summary { get; } = new();

        /// <summary>
        /// Creates an .apsimx file and populates it with experiment models
        /// </summary>
        public async override Task Run()
        {
            // Reset the markdown report
            Summary.Clear();
            Summary.AddSubHeading("REMS export summary", 1);

            var simulations = new Simulations();            

            // Find the experiments            
            var exps = (await Handler.Query(new ExperimentsQuery()))
                .Where(e => Experiments.Contains(e.Name));

            var ids = exps.Select(e => e.ID).ToArray();

            // Output the observed data
            string name = Path.GetFileNameWithoutExtension(FilePath);
            await Handler.Query(new WriteObservedCommand { FileName = name, IDs = ids });

            // Add the data store
            var store = new DataStore();
            var input = new Input 
            { 
                Name = "Observed", 
                FileNames = new string[] { $"{Manager.ExportPath}\\obs\\{name}_Observed.csv"}
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

            simulations.Children.Add(store);

            // Add the panel graphs
            var file = Manager.GetFileInfo("PredictedObserved.apsimx");
            var panel = JsonTools.LoadJson<GraphPanel>(file);
            simulations.Children.Add(panel);

            // Output the met data
            var query = new WriteMetCommand { ExperimentIds = ids };
            var mets = await Handler.Query(query);

            // Check if replacements is necessary
            if (exps.Any(e => e.Crop == "Sorghum"))
            {
                var info = Manager.GetFileInfo("SorghumReplacements");
                var model = JsonTools.LoadJson<Replacements>(info);
                simulations.Children.Add(model);
            }

            // Convert each experiment into an APSIM model
            foreach (var (ID, Name, Crop) in exps)
            {
                OnNextItem("Simulation");
                Summary.AddSubHeading(Name + ':', 2);
                var request = new WeatherQuery { ExperimentId = ID, Mets = mets, Report = Summary };
                var weather = await Handler.Query(request);
                var template = new ExperimentTemplate(ID, Name, weather, Handler, Progress, Summary);
                var model = await template.AsyncCreate();
                simulations.Children.Add(model);
            }

            var memo = new Memo { Name = "ExportSummary", Text = Summary.Text };
            simulations.Children.Add(memo);
            SanitiseNames(simulations);

            // Save the file
            File.WriteAllText(FilePath, FileFormat.WriteToString(simulations));
        }

        private void SanitiseNames(IModel model)
        {
            model.Name = model.Name.Replace('.', '_');

            foreach (IModel child in model.Children)
                SanitiseNames(child);
        }        
    }
}
