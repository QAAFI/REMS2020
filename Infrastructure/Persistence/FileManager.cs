using System;
using System.IO;

using Rems.Application.Common.Interfaces;

namespace Rems.Persistence
{
    public class FileManager : IFileManager
    {
        /// <inheritdoc/>
        public string DbConnection { get; set; }

        /// <inheritdoc/>
        public string ExportFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <inheritdoc/>
        public string ImportFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        

        /// <inheritdoc/>
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
