using System;
using System.Collections.Generic;

namespace Database
{
    public partial class IrrigationInfo
    {
        public int IrrigationInfoId { get; set; }
        public int? IrrigationId { get; set; }
        public string Variable { get; set; }
        public string Value { get; set; }

        public virtual Irrigation Irrigation { get; set; }
    }
}
