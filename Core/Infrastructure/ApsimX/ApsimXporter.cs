using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using Models;
using Models.Core;
using Models.Core.ApsimFile;

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

        public Markdown Summary { get; } = new("REMS export summary");

        public Simulations Simulations { get; } = new Simulations();

        private int[] ids = Array.Empty<int>();

        private IEnumerable<(int ID, string Name, string Crop)> exps = new List<(int, string, string)>();

        private (string Station, string File, DateTime Start, DateTime End)[] mets = Array.Empty<(string, string, DateTime, DateTime)>();

        private string name;

        /// <summary>
        /// Creates an .apsimx file and populates it with experiment models
        /// </summary>
        public async override Task Run()
        {
            await FindExperiments();
            await ExportObserved();
            await ExportMet();
            AddDataStore();
            AddPanelGraphs();
            AddReplacements();
            await AddExperiments();
            AddSummary();
            ExportSimulation();
        }

        public async Task FindExperiments()
        {
            exps = (await Handler.Query(new ExperimentsQuery()))
                .Where(e => Experiments.Contains(e.Name));

            ids = exps.Select(e => e.ID).ToArray();
        }

        public async Task ExportObserved()
        {
            name = Path.GetFileNameWithoutExtension(FilePath);
            await Handler.Query(new WriteObservedCommand { FileName = name, IDs = ids });
        }

        public async Task ExportMet()
        {
            var query = new WriteMetCommand { ExperimentIds = ids };
            mets = await Handler.Query(query);
        }

        public void AddDataStore()
        {
            var store = new DataStoreTemplate(name).Create();
            Simulations.Children.Add(store);
        }

        public void AddPanelGraphs()
        {
            var file = Manager.GetFileInfo("PredictedObserved.apsimx");
            var panel = JsonTools.LoadJson<GraphPanel>(file);
            Simulations.Children.Add(panel);
        }

        // Check if replacements is necessary
        public void AddReplacements()
        {
            
            if (exps.Any(e => e.Crop == "Sorghum"))
            {
                var info = Manager.GetFileInfo("SorghumReplacements");
                var model = JsonTools.LoadJson<Replacements>(info);
                Simulations.Children.Add(model);
            }
        }

        // Convert each experiment into an APSIM model
        public async Task AddExperiments()
        {            
            foreach (var (ID, Name, Crop) in exps)
            {
                OnNextItem("Simulation");
                Summary.AddSubHeading(Name + ':', 2);
                var request = new WeatherQuery { ExperimentId = ID, Mets = mets, Report = Summary };
                var weather = await Handler.Query(request);
                var template = new ExperimentTemplate(ID, Name, weather, Handler, Progress, Summary);
                var model = await template.AsyncCreate();
                Simulations.Children.Add(model);
            }
        }

        public void AddSummary()
        {
            var memo = new Memo { Name = "ExportSummary", Text = Summary.Text };
            Simulations.Children.Add(memo);
        }

        public void ExportSimulation()
        {
            SanitiseNames(Simulations);
            File.WriteAllText(FilePath, FileFormat.WriteToString(Simulations));
        }

        private void SanitiseNames(IModel model)
        {
            model.Name = model.Name.Replace('.', '_');

            foreach (IModel child in model.Children)
                SanitiseNames(child);
        }        
    }
}
