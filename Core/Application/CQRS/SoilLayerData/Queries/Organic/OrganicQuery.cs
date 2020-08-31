using System;

using MediatR;
using Models.Soils;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class OrganicQuery : IRequest<Organic>, IParameterised
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
