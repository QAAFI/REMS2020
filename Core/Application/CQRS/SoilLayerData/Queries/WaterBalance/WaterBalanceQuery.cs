using MediatR;

using Rems.Domain.Entities;
using Rems.Application.Common.Mappings;

using System;
using System.Data;
using System.Collections.Generic;
using Models.Soils;
using Rems.Application.Common.Interfaces;
using Models.WaterModel;

namespace Rems.Application.CQRS.Experiments.Queries.Experiments
{
    public class WaterBalanceQuery : IRequest<WaterBalance>, IParameterised
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
