using MediatR;

using System;
using Rems.Application.Common.Interfaces;
using Models.PMF;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class PlantQuery : IRequest<Plant>, IParameterised
    {   
        public int ExperimentId { get; set; }

        public void Parameterise(params object[] args)
        {
            if (args.Length != 1) 
                throw new Exception($"Invalid number of parameters. \n Expected: 1 \n Received: {args.Length}");

            if (args[0] is int id)
                ExperimentId = id;
            else
                throw new Exception($"Invalid parameter type. \n Expected: {typeof(int)} \n Received: {args[0].GetType()}");
        }
    }
}
