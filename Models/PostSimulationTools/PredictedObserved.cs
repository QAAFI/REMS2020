using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PostSimulationTools
{
    public class PredictedObserved : ApsimNode
    {
        public string PredictedTableName { get; set; } = default;

        public string ObservedTableName { get; set; } = default;

        public string FieldNameUsedForMatch { get; set; } = default;

        public string FieldName2UsedForMatch { get; set; } = default;

        public string FieldName3UsedForMatch { get; set; } = default;


        public PredictedObserved()
        { }
    }
}
