using MediatR;
using Models;
using Models.Core;
using Models.Factorial;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Surface;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;

namespace Rems.Application.Common.Extensions
{
    public enum NextNode
    {
        Parent,
        Sibling,
        Child,
        None
    }

    public static class ModelExtensions
    {
        #region General
        public static IModel Add<R>(this IModel model, NextNode next, Query<IModel> query, params object[] args)
            where R : IRequest<IModel>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(args);

            var child = query(request);
            return model.Add(child, next);
        }

        public static IModel Add<M>(this IModel model, NextNode next, string name = null)
            where M : IModel, new()
        {
            var child = new M();
            if (!(name is null)) child.Name = name;
            return model.Add(child, next);
        }

        public static IModel Add(this IModel model, IModel child, NextNode next)
        {
            EventManager.InvokeProgressIncremented();

            model.Children.Add(child);
            child.Parent = model;

            if (next == NextNode.Parent)
                return model.Parent;

            if (next == NextNode.Child)
                return child;

            if (next == NextNode.Sibling)
                return model;

            // if (next == NextNode.None)
            return null;
        }
        #endregion

        #region Simulations       

        public static IModel AddExperiment(this IModel model, NextNode next, int id, string name, Query<IModel> query)
        {
            var experiment = new Experiment() { Name = name };
            experiment.Add<Factors>(NextNode.Child)
                .Add<PermutationQuery>(NextNode.Child, query, id);
            
            experiment.AddTreatment(id, query);
            return model.Add(experiment, next);
        }

        public static void AddTreatment(this IModel model, int id, Query<IModel> query)
        {
            var sim = new Simulation() { Name = "Base" };

            sim.Add<ClockQuery>(NextNode.Sibling, query, id)
                .Add<Summary>(NextNode.Sibling)
                .AddWeather(NextNode.Sibling, id, query)
                .Add<SoilArbitrator>(NextNode.Sibling)
                .Add<ZoneQuery>(NextNode.Child, query, id)
                    .Add<PlantQuery>(NextNode.Sibling, query, id)
                    .AddSoil(NextNode.Sibling, id, query)
                    .AddSurfaceOrganicMatter(NextNode.Sibling)
                    .Add<Operations>(NextNode.Sibling)
                    .Add<Irrigation>(NextNode.Sibling, "Irrigation")
                    .Add<Fertiliser>(NextNode.Sibling, "Fertiliser")
                    .Add<Report>(NextNode.Sibling, "Daily")
                    .Add<Report>(NextNode.None, "Harvest");

            model.Children.Add(sim);

            EventManager.InvokeProgressIncremented();
        }
        #endregion

        #region Models
        public static IModel AddSoil(this IModel model, NextNode next, int id, Query<IModel> query)
        {
            var soil = query(new SoilQuery() { ExperimentId = id });

            soil.Add<PhysicalQuery>(NextNode.Child, query, id)
                    .Add<SoilCropQuery>(NextNode.Parent, query, id)
                .Add<WaterBalanceQuery>(NextNode.Sibling, query, id)
                .Add<SoilNitrogen>(NextNode.Child, "SoilNitrogen")
                    .Add<SoilNitrogenNH4>(NextNode.Sibling, "NH4")
                    .Add<SoilNitrogenNO3>(NextNode.Sibling, "NO3")
                    .Add<SoilNitrogenUrea>(NextNode.Sibling, "Urea")
                    .Add<SoilNitrogenPlantAvailableNH4>(NextNode.Sibling, "PlantAvailableNH4")
                    .Add<SoilNitrogenPlantAvailableNO3>(NextNode.Parent, "PlantAvailableNH4")
                .Add<OrganicQuery>(NextNode.Sibling, query, id)
                .Add<ChemicalQuery>(NextNode.Sibling, query, id)
                .Add<SampleQuery>(NextNode.Sibling, query, id)
                .Add<CERESSoilTemperature>(NextNode.Parent, "SoilTemperature");

            return model.Add(soil, next);
        }

        public static IModel AddSurfaceOrganicMatter(this IModel model, NextNode next)
        {
            var organic = new SurfaceOrganicMatter()
            {
                ResourceName = "SurfaceOrganicMatter",
                InitialResidueName = "wheat_stubble",
                InitialResidueType = "wheat",
                InitialCNR = 80.0
            };

            return model.Add(organic, next);
        }

        public static IModel AddWeather(this IModel model, NextNode next, int id, Query<IModel> query)
        {
            var weather = query(new MetFileDataQuery() { ExperimentId = id });           

            return model.Add(weather, next);
        }

        public static IModel AddOperations(this IModel model, NextNode next, int id, Query<Operation[]> query)
        {
            var operations = new Operations()
            {
                Operation = new List<Operation>()
            };

            var i = query(new IrrigationsQuery() { TreatmentId = id });
            operations.Operation.AddRange(i);

            var f = query(new FertilizationsQuery() { TreatmentId = id });            
            operations.Operation.AddRange(f);

            return model.Add(operations, next);
        }

        #endregion
    }
}
