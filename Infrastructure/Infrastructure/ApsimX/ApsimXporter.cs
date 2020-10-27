using Models.Core;
using Models.Core.ApsimFile;

using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;

using System;
using System.IO;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public class ApsimXporter : ProgressTracker
    {
        public string FileName { get; set; }

        public override int Items { get; protected set; } = 0;
        public override int Steps { get; protected set; } = 0;

        public ApsimXporter(QueryHandler query, CommandHandler command) 
            : base(query, command)
        {            
            Items = OnSendQuery(new ExperimentCount());
            Steps = Items * 28;
        }

        public async override Task Run()
        {
            var path = Path.Combine("DataFiles", "apsimx", "Sorghum.apsimx");
            var simulations = JsonTools.LoadJson<Simulations>(path);

            var folder = new Folder() { Name = "Experiments" };
            var experiments = OnSendQuery(new ExperimentsQuery());
            
            foreach (var experiment in experiments)            
                await AddExperiment(folder, experiment.Key, experiment.Value);            

            simulations.Children.Add(folder);

            File.WriteAllText(FileName, FileFormat.WriteToString(simulations));

            OnTaskFinished();
        }

        private Task AddExperiment(Model model, int id, string name)
        {
            OnNextItem(name);
            return Task.Run(() => model.AddExperiment(NextNode.None, id, name));
        }
    }    
}
