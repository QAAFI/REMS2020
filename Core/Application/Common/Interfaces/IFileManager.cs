using System;
using System.IO;

namespace Rems.Application.Common.Interfaces
{
    public interface IFileManager
    {
        string DbConnection { get; set; }

        string ExportFolder { get; set; }

        string ImportFolder { get; set; }

        string GetContent(string filename);

        FileInfo GetFileInfo(string filename);
    }
}
