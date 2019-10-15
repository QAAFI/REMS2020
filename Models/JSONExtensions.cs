using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models
{
    public class ShortDateTimeConverter : IsoDateTimeConverter
    {
        public ShortDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
