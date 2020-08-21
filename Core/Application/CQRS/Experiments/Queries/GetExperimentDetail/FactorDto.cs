using AutoMapper;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments
{
    public class FactorDto : IMapFrom<Factor>
    {
        public int FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Factor, FactorDto>();
                //.ForMember(d => d.FactorId, opt => opt.MapFrom(s => s.FactorId ))
                //.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }

    }
}
