﻿using AutoMapper;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments.Queries.GetExperimentList
{
    public class ExperimentLookupDto : IMapFrom<Experiment>
    {
        public string ExperimentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public int Year { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Experiment, ExperimentLookupDto>()
                .ForMember(d => d.ExperimentId, opt => opt.MapFrom(s => s.ExperimentId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Field != null ? s.Field.Name : ""))
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.BeginDate != null ? s.BeginDate.Value.Year : 0));

        }

    }
}