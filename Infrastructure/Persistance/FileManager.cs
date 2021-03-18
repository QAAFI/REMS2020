using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Rems.Application.Common.Interfaces;

namespace Rems.Persistence
{
    public class FileManager : IFileManager
    {
        public FileManager()
        { }

        public string GetFile(string filename, string extension)
        {
            using (var stream = new FileStream(filename + "." + extension, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
