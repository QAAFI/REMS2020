using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments.Queries.GetExperimentDetail
{
    public class TreatmentDto
    {
        public int TreatmentId { get; set; }
        public string Name { get; set; }

        public IEnumerable<LevelDto> Levels { get; set; }
    }
}
