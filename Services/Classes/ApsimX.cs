using Models.Core;
using Services.Interfaces;

namespace Services.Classes
{
    public class ApsimX : IApsimX
    {
        public Simulations Simulations { get; set; }
    }
}
