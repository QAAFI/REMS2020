using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Experiments.Queries.GetExperimentDetail
{
    public class LevelDto
    {
        public int LevelId { get; set; }

        public int? FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual FactorDto Factor { get; set; }
    }
}
