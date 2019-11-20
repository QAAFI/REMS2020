using Models.Soils;

namespace Rems.Infrastructure
{
    public partial class ApsimBuilder
    {
        public static Sample BuildSample(string name)
        {
            return new Sample()
            {
                Name = name
            };
        }
    }
}
