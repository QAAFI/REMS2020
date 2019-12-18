using AutoMapper;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments
{
    public class TreatmentDto : IMapFrom<Treatment>
    {
        public int TreatmentId { get; set; }
        public string Name { get; set; }

        public IEnumerable<LevelDto> Levels { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Treatment, TreatmentDto>()
                .ForMember(d => d.TreatmentId, opt => opt.MapFrom(s => s.TreatmentId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Levels, opt => opt.MapFrom(s => s.Designs));
        }
    }
}
