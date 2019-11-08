using Models.Core;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Classes
{
    public class ApsimX : IApsimX
    {
        public Simulations Simulations { get; set; }
    }
}
