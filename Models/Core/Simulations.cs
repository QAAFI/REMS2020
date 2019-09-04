using Models.Storage;

namespace Models.Core
{
    /// <summary>
    /// Container for a series of simulations
    /// </summary>
    public class Simulations : Node
    {
        public int ExplorerWidth { get; set; } = 300;

        public int Version { get; set; } = 54;

        public Simulations()
        {
            Name = "Simulations";
        }
    }      
}
