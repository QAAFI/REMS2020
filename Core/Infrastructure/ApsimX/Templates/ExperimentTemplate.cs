using System;
using System.Threading.Tasks;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using Models.Factorial;
using MediatR;
using Models;
using Models.Core;
using Models.Soils.Arbitrator;
using Models.Surface;
using Models.Climate;

namespace Rems.Infrastructure.ApsimX
{
    public class ExperimentTemplate : ITemplate<Experiment>
    {
        readonly IFileManager Manager = FileManager.Instance;

        readonly IQueryHandler Handler;

        readonly IProgress<int> Progress;

        readonly Markdown Summary;

        readonly int ID;

        readonly string Name;

        readonly Weather Weather;

        public ExperimentTemplate(int id, string name, Weather weather, IQueryHandler handler, IProgress<int> progress, Markdown summary)
        {
            ID = id;
            Name = name;
            Weather = weather;
            Handler = handler;
            Progress = progress;
            Summary = summary;
        }

        public Experiment Create() => AsyncCreate().Result;

        public async Task<Experiment> AsyncCreate()
        {
            // Creates a model tree
            // The indentation below indicates depth in the tree, grouped models at the same indentation 
            // represent siblings in the tree

            var experiment =
            Create<Experiment>(Name, new IModel[]
            {
                await Request(new FactorsQuery{ ExperimentId = ID, Report = Summary }),
                Create<Simulation>(Name, new IModel[]
                {
                    await Request(new ClockQuery{ ExperimentId = ID, Report = Summary }),
                    Create<Summary>(),
                    Weather,
                    Create<SoilArbitrator>(),
                    await Request(new ZoneQuery{ ExperimentId = ID, Report = Summary }, new IModel[]
                    {
                        await new ReportTemplate(ID, Handler, Manager).AsyncCreate(),
                        await Handler.Query(new ReportQuery { ExperimentId = ID }),
                        await Handler.Query(new ManagersQuery {ExperimentId = ID }),
                        await Request(new PlantQuery { ExperimentId = ID, Report = Summary }),
                        await new SoilTemplate(ID, Handler, Progress, Summary).AsyncCreate(),
                        new SurfaceOrganicMatter()
                        {
                            ResourceName = "SurfaceOrganicMatter",
                            InitialResidueName = "wheat_stubble",
                            InitialResidueType = "wheat",
                            InitialCNR = 80.0
                        },
                        Create<Operations>("Irrigations"),
                        Create<Operations>("Fertilisations"),
                        Create<Irrigation>("Irrigation"),
                        await Request(new FertiliserQuery { ExperimentId = ID }),
                        new MicroClimate
                        {
                            a_interception = 0,
                            b_interception = 1,
                            c_interception = 0,
                            d_interception = 0,
                            SoilHeatFluxFraction = 0.4,
                            MinimumHeightDiffForNewLayer = 0,
                            NightInterceptionFraction = 0.5,
                            ReferenceHeight = 2
                        }
                    })
                })
            });
            Summary.AddLine("\n");

            return experiment as Experiment;
        }

        /// <summary>
        /// Invokes a request for the specified Apsim model
        /// </summary>
        /// <typeparam name="R">A request that can be parameterised with specific values</typeparam>
        /// <param name="id">The experiment data is requested from</param>
        /// <param name="children">Any child models to include</param>
        private async Task<T> Request<T>(IRequest<T> query, params IModel[] children) where T : IModel
        {
            var model = await Handler.Query(query);

            // Attach child models
            if (children != null) foreach (var child in children)
                    model.Children.Add(child);

            Progress.Report(1);

            return model;
        }

        /// <summary>
        /// Creates an Apsim model
        /// </summary>
        /// <typeparam name="M">The model type</typeparam>
        /// <param name="name">The model name</param>
        /// <param name="children">Any child models to include</param>
        /// <returns></returns>
        private IModel Create<M>(string name = null, params IModel[] children)
            where M : IModel, new()
        {
            var model = new M();

            if (!(name is null)) model.Name = name;

            if (children != null) foreach (var child in children)
                    model.Children.Add(child);

            Progress.Report(1);

            return model;
        }
    }
}
