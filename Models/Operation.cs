using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models
{
    public class Operation : ApsimNode
    {
        [JsonIgnore]
        public new string Name;

        [JsonIgnore]
        public new List<ApsimNode> Children { get; set; } = new List<ApsimNode>();

        [JsonIgnore]
        public new bool IncludeInDocumentation { get; set; } = true;

        [JsonIgnore]
        public new bool ReadOnly { get; set; } = false;

        [JsonConverter(typeof(ShortDateTimeConverter))]
        public DateTime Date { get; set; } = default;

        public string Action { get; set; } = default;
        
        public Operation()
        { }
    }
}
