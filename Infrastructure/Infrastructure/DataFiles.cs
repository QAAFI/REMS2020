using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rems.Infrastructure
{
    public static class DataFiles
    {
        public static string ReadRawText(string file)
        {
            var filePath = Path.Combine("DataFiles", "apsimx", file);
            StringBuilder builder = new StringBuilder();

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream) builder.AppendLine(reader.ReadLine());
            }

            builder.Replace("\r", "");
            return builder.ToString();
        }
    }
}
