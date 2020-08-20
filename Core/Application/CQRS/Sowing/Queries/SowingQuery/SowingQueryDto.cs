using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;

namespace Rems.Application.Queries
{
    public class SowingQueryDto : IMapFrom<Sowing>
    {
        public DateTime Date { get; set; }

        public string Density { get; set; }

        public string Depth { get; set; }

        public string Cultivar { get; set; }

        public string RowSpacing { get; set; }

        public string RowConfiguration { get; set; }

        public string Ftn { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Sowing, SowingQueryDto>()
                .ForMember(d => d.Date, opts => opts.MapFrom(s => s.Date))
                .ForMember(d => d.Density, opts => opts.MapFrom(s => s.Population))
                .ForMember(d => d.Depth, opts => opts.MapFrom(s => s.Depth))
                .ForMember(d => d.Cultivar, opts => opts.MapFrom(s => s.Cultivar))
                .ForMember(d => d.RowSpacing, opts => opts.MapFrom(s => s.RowSpace))
                .ForMember(d => d.RowConfiguration, opts => opts.MapFrom(s => "solid"));
        }
    }
}
