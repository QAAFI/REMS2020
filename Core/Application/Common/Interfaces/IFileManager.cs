using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IFileManager
    {
        string DbConnection { get; set; }

        string ExportFolder { get; set; }

        string ImportFolder { get; set; }

        string GetContent(string filename);
    }
}
