using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

namespace Rems.Application.Met.Queries
{ 
    public class MetStationDto : IMapFrom<MetStation>
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        public double TemperatureAverage { get; set; }

        public double Amp { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MetStation, MetStationDto>()
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.Latitude, opts => opts.MapFrom(s => s.Latitude))
                .ForMember(d => d.Longitude, opts => opts.MapFrom(s => s.Longitude))
                .ForMember(d => d.TemperatureAverage, opts => opts.MapFrom(s => s.TemperatureAverage))
                .ForMember(d => d.Amp, opts => opts.MapFrom(s => s.Amplitude));
        }
    }
}
