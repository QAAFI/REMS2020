using AutoMapper;

using Rems.Application.Common.Mappings;
using Rems.Domain.Entities;

using System.Collections.Generic;
using System.Linq;

namespace Rems.Application.Queries
{
    public class ExperimentDetailVm : IMapFrom<Experiment>
    {
        public string Name { get; set; }

        public int MetStationId { get; set; }

        public IEnumerable<int> Treatments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Experiment, ExperimentDetailVm>()
                .ForMember(s => s.Name, opts => opts.MapFrom(d => d.Name))
                .ForMember(s => s.MetStationId, opts => opts.MapFrom(d => d.MetStationId))
                .ForMember(s => s.Treatments, opts => opts.MapFrom(d => d.Treatments.Select(t => t.TreatmentId)));
        }
    }
}
