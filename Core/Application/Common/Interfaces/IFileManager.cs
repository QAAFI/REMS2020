using System;
using System.IO;

namespace Rems.Application.Common.Interfaces
{
    public interface IFileManager
    {
        string DbConnection { get; set; }

        string ExportPath { get; set; }

        string ImportPath { get; set; }

        string GetContent(string filename);

        FileInfo GetFileInfo(string filename);
    }
}
