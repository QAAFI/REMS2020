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

        private DirectoryInfo directory;

        public FileManager()
        {
            directory = new DirectoryInfo(Directory.GetCurrentDirectory() + "/Files");

            var test = GetContent("Sorghum");
        }

        public string GetContent(string filename)
        { 
            var files = directory.GetFiles($"*{filename}*", SearchOption.AllDirectories);
            
            if (files.Length == 0)
                throw new FileNotFoundException("", filename);

            if (files.Length > 1)
                throw new Exception($"Multiple files found that match '{filename}'");

            using (var stream = files[0].OpenRead())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
