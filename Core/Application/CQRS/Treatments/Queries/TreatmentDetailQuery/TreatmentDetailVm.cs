using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System;
using System.Linq;

namespace Rems.Application.Treatments.Queries
{ 
    public class TreatmentDetailVm : IMapFrom<Treatment>
    {
        public string CropName { get; set; }

        public string ExperimentName { get; set; }

        public int Id { get; set; }

        public int SoilId { get; set; }

        public int SowingId { get; set; }

        public double FieldSlope { get; set; }

        public double FieldArea { get; set; }

        public double FieldLat { get; set; }

        public double FieldLon { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string MetFileName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Treatment, TreatmentDetailVm>()
                .ForMember(d => d.CropName, opts => opts.MapFrom(s => s.Experiment.Crop.Name))
                .ForMember(d => d.ExperimentName, opts => opts.MapFrom(s => s.Experiment.Name))
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.TreatmentId))
                .ForMember(d => d.SoilId, opts => opts.MapFrom(s => s.Experiment.Field.SoilId))
                .ForMember(d => d.FieldSlope, opts => opts.MapFrom(s => s.Experiment.Field.Slope))
                .ForMember(d => d.FieldArea, opts => opts.MapFrom(s => 1.0))
                .ForMember(d => d.FieldLat, opts => opts.MapFrom(s => s.Experiment.Field.Latitude))
                .ForMember(d => d.FieldLon, opts => opts.MapFrom(s => s.Experiment.Field.Longitude))
                .ForMember(d => d.Start, opts => opts.MapFrom(s => s.Experiment.BeginDate))
                .ForMember(d => d.End, opts => opts.MapFrom(s => s.Experiment.EndDate))
                .ForMember(d => d.MetFileName, opts => opts.MapFrom(s => s.Experiment.MetStation.Name + ".met"));
        }
    }
}
