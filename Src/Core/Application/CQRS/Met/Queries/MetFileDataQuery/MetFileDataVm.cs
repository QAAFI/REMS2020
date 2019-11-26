using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;

namespace Rems.Application.Met.Queries
{
    public class MetFileDataVm : IMapFrom<MetData[]>
    {
        public string Year { get; set; }

        public string Day { get; set; }

        public double TMax { get; set; }

        public double TMin { get; set; }

        public double Radn { get; set; }

        public double Rain { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MetData[], MetFileDataVm>()
                .BeforeMap((s, d) => 
                {
                    if (s.Length != 4) 
                        throw new Exception("MetFileData requires 4 ordered instances of MetData to map.");
                })
                .ForMember(d => d.Year, opts => opts.MapFrom(s => s[0].Date.Year))
                .ForMember(d => d.Day, opts => opts.MapFrom(s => s[0].Date.DayOfYear))
                .ForMember(d => d.TMax, opts => opts.MapFrom(s => s[0].Value))
                .ForMember(d => d.TMin, opts => opts.MapFrom(s => s[1].Value))
                .ForMember(d => d.Radn, opts => opts.MapFrom(s => s[2].Value))
                .ForMember(d => d.Rain, opts => opts.MapFrom(s => s[3].Value));
        }
    }
}
