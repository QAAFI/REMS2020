using System;
using System.IO;
using System.Linq;

using Rems.Application.Common.Interfaces;

namespace Rems.Infrastructure
{
    public sealed class FileManager : IFileManager
    {
        #region Singleton implementation
        private static readonly FileManager instance = new();

        public static FileManager Instance => instance;

        static FileManager()
        { }

        private FileManager()
        { }
        #endregion

        /// <inheritdoc/>
        public string DbConnection { get; set; }

        /// <inheritdoc/>
        public string ExportPath { get; set; }

        /// <inheritdoc/>
        public string ImportPath { get; set; }

        private DirectoryInfo directory = new(Directory.GetCurrentDirectory() + "/ExportFiles");

        public static bool Connected => File.Exists(instance.DbConnection);

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
