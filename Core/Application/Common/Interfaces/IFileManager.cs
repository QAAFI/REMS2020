using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Application.Common.Interfaces
{
    public interface IFileManager
    {
        string GetFile(string filename, string extension);
    }
}
