using Models.Core;
using Models.Core.ApsimFile;
using Models.Core.Run;

using System.IO;
using System.Threading.Tasks;

namespace Rems.Infrastructure.ApsimX
{
    public interface IApsimX
    {
        ApsimBuilder Builder { get; set; }

        Simulations Simulations { get; set; }
    }

    public static class ApsimXExtensions
    {
        public static void RunApsimFile(this IApsimX apsimx)
        {
            ApsimX apsim = apsimx as ApsimX;
            var runner = new Runner(apsim.Simulations);
            runner.Run();
        }

        public static async Task CreateApsimModel(this IApsimX apsimx, string path)
        {
            apsimx.Simulations.Children.Add(apsimx.Builder.BuildDataStore());
            apsimx.Simulations.Children.Add(apsimx.Builder.BuildReplacements());
            apsimx.Simulations.Children.Add(await apsimx.Builder.BuildValidations(path));
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
