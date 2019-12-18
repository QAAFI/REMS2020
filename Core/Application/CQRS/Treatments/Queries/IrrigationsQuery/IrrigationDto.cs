using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;

namespace Rems.Application.Treatments.Queries
{ 
    public class IrrigationDto : IMapFrom<Irrigation>
    {
        public string Amount { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Fertilization, IrrigationDto>()
                .ForMember(d => d.Amount, opts => opts.MapFrom(s => s.Amount))
                .ForMember(d => d.Date, opts => opts.MapFrom(s => s.Date));
        }
    }
}
