using Models.Core.ApsimFile;
using Models.Core.Run;

using System.IO;

namespace Rems.Infrastructure.ApsimX
{
    public interface IApsimX
    {
    }

    public static class ApsimXExtensions
    {
        public static void RunApsimFile(this IApsimX apsimx)
        {
            ApsimX apsim = apsimx as ApsimX;
            var runner = new Runner(apsim.Simulations);
            runner.Run();
        }

        public static void SaveApsimFile(this IApsimX apsimx, string filename)
        {
            ApsimX apsim = apsimx as ApsimX;
            apsim.Simulations.FileName = filename;
            //Calling apsim.Simulations.Write causes the storage to run which looks for sqlite.dll
            File.WriteAllText(filename, FileFormat.WriteToString(apsim.Simulations));
        }
    }
}
