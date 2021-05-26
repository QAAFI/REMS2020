using System;
using System.IO;
using System.Linq;

using Rems.Application.Common.Interfaces;

namespace Rems.Persistence
{
    public sealed class FileManager : IFileManager
    {
        #region Singleton implementation
        private static readonly FileManager instance = new FileManager();

        public static FileManager Instance => instance;

        static FileManager()
        { }

        private FileManager()
        { }
        #endregion

        /// <inheritdoc/>
        public string DbConnection { get; set; }

        /// <inheritdoc/>
        public string ExportFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <inheritdoc/>
        public string ImportFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory() + "/Files");

        public string GetContent(string filename)
        {
            var file = GetFileInfo(filename);

            if (!file.Exists) throw new FileNotFoundException(null, filename);
            
            using var stream = file.OpenRead();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public FileInfo GetFileInfo(string filename)
        {
            var files = directory.GetFiles($"*{filename}*", SearchOption.AllDirectories);
                        
            if (files.Length > 1)
                throw new Exception($"Multiple files found that match '{filename}'");

            return files.FirstOrDefault();
        }
    }
}
