using MediatR;
using Models;
using Models.Climate;
using Models.Core;
using Models.Factorial;
using Models.PMF;
using Models.Soils;
using Models.Soils.Arbitrator;
using Models.Surface;
using Models.WaterModel;
using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.IO;

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
        public static IModel Add<M, R>(this IModel model, NextNode next, params object[] args)
            where M : IModel
            where R : IRequest<M>, IParameterised, new()
        {
            var request = new R();
            request.Parameterise(args);

            var child = EventManager.OnSendQuery(request);
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

        public static IModel AddExperiment(this IModel model, NextNode next, int id, string name)
        {
            var experiment = new Experiment() { Name = name };
            experiment.Add<Factors>(NextNode.Child)
                .Add<Permutation>(NextNode.Child)
                    .AddFactors(id);
            
            experiment.AddTreatment(id);
            return model.Add(experiment, next);
        }

        public static void AddFactors(this IModel model, int id)
        {
            var factors = EventManager.OnSendQuery(new FactorQuery() { ExperimentId = id });

            foreach (var factor in factors)
                model.Children.Add(factor);            
        }

        public static void AddTreatment(this IModel model, int id)
        {
            var sim = new Simulation() { Name = "Base" };

            sim.Add<Clock, ClockQuery>(NextNode.Sibling, id)
                .Add<Summary>(NextNode.Sibling)
                .AddWeather(NextNode.Sibling, id)
                .Add<SoilArbitrator>(NextNode.Sibling)
                .Add<Zone, ZoneQuery>(NextNode.Child, id)
                    .Add<Plant, PlantQuery>(NextNode.Sibling, id)
                    .AddSoil(NextNode.Sibling, id)
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
        public static IModel AddSoil(this IModel model, NextNode next, int id)
        {
            var soil = EventManager.OnSendQuery(new SoilQuery() { ExperimentId = id });

            soil.Add<Physical, PhysicalQuery>(NextNode.Child, id)
                    .Add<SoilCrop, SoilCropQuery>(NextNode.Parent, id)
                .Add<WaterBalance, WaterBalanceQuery>(NextNode.Sibling, id)
                .Add<SoilNitrogen>(NextNode.Child, "SoilNitrogen")
                    .Add<SoilNitrogenNH4>(NextNode.Sibling, "NH4")
                    .Add<SoilNitrogenNO3>(NextNode.Sibling, "NO3")
                    .Add<SoilNitrogenUrea>(NextNode.Sibling, "Urea")
                    .Add<SoilNitrogenPlantAvailableNH4>(NextNode.Sibling, "PlantAvailableNH4")
                    .Add<SoilNitrogenPlantAvailableNO3>(NextNode.Parent, "PlantAvailableNH4")
                .Add<Organic, OrganicQuery>(NextNode.Sibling, id)
                .Add<Chemical, ChemicalQuery>(NextNode.Sibling, id)
                .Add<Sample, SampleQuery>(NextNode.Sibling, id)
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

        public static IModel AddWeather(this IModel model, NextNode next, int id)
        {
            var met = EventManager.OnSendQuery(new MetStationQuery() { ExperimentId = id });

            string file = met + ".met";

            if (!File.Exists(file))
            {
                using (var stream = new FileStream(file, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                {
                    var builder = EventManager.OnSendQuery(new MetFileDataQuery() { ExperimentId = id });
                    writer.Write(builder.ToString());
                    writer.Close();
                }
            }

            var weather = new Weather()
            {
                FileName = file
            };

            return model.Add(weather, next);
        }

        public static IModel AddOperations(this IModel model, NextNode next, int id)
        {
            var operations = new Operations()
            {
                Operation = new List<Operation>()
            };

            var i = EventManager.OnSendQuery(new IrrigationsQuery() { TreatmentId = id });
            operations.Operation.AddRange(i);

            var f = EventManager.OnSendQuery(new FertilizationsQuery() { TreatmentId = id });            
            operations.Operation.AddRange(f);

            return model.Add(operations, next);
        }

        #endregion
    }
}
