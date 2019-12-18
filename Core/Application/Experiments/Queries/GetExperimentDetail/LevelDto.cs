using AutoMapper;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments
{
    public class LevelDto : IMapFrom<Design>
    {
        public int LevelId { get; set; }

        public int? FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual FactorDto Factor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Design, LevelDto>()
                .ForMember(d => d.LevelId, opt => opt.MapFrom(s => s.LevelId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Level.Name))
                .ForMember(d => d.FactorId, opt => opt.MapFrom(s => s.Level.FactorId))
                .ForMember(d => d.Factor, opt => opt.MapFrom(s => s.Level.Factor));
        }

    }
}
