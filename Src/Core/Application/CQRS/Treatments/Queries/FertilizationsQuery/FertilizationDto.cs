using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;

namespace Rems.Application.Treatments.Queries
{
    public class FertilizationDto : IMapFrom<Fertilization>
    {
        public string Amount { get; set; }

        public string Type { get; set; }

        public int Depth { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Fertilization, FertilizationDto>()
                .ForMember(d => d.Amount, opts => opts.MapFrom(s => s.Amount))
                .ForMember(d => d.Type, opts => opts.MapFrom(s => s.Fertilizer.Name))
                .ForMember(d => d.Depth, opts => opts.MapFrom(s => s.Depth))
                .ForMember(d => d.Date, opts => opts.MapFrom(s => s.Date));
        }
    }
}
