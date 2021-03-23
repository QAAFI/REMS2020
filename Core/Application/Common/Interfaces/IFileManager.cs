using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IFileManager
    {
        string ExportFolder { get; set; }

        string ImportFolder { get; set; }

        string GetFile(string filename, string extension);
    }
}
