using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using Rems.Application.Common.Interfaces;
using Rems.Domain.Entities;

namespace Rems.Application.Treatments.Queries
{
    public class IrrigationsQuery : IRequest<IEnumerable<IrrigationDto>>
    {
        public int TreatmentId { get; set; }        
    }
}
