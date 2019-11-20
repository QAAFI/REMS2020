using AutoMapper;
using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments.Queries.GetExperimentDetail
{
    public class ExperimentDetailVm : IMapFrom<Experiment>
    {
        public string ExperimentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }
        public int Year { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Crop { get; set; }
        public string Field { get; set; }
        public string MetStation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Experiment, ExperimentDetailVm>()
                .ForMember(d => d.ExperimentId, opt => opt.MapFrom(s => s.ExperimentId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.Location, opt => opt.MapFrom(s => s.Field != null ? s.Field.Name : ""))
                .ForMember(d => d.Year, opt => opt.MapFrom(s => s.BeginDate != null ? s.BeginDate.Value.Year : 0))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.BeginDate))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.EndDate))
                .ForMember(d => d.Crop, opt => opt.MapFrom(s => s.Crop != null ? s.Crop.Name : ""))
                .ForMember(d => d.Field, opt => opt.MapFrom(s => s.Field != null ? s.Field.Name : ""))
                .ForMember(d => d.MetStation, opt => opt.MapFrom(s => s.MetStation != null ? s.MetStation.Name : ""));

        }


    }
}
