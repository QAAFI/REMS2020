using Rems.Application.Common.Interfaces;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Rems.Infrastructure.ApsimX.Writers
{
    public class ObservedWriter : IRemsTemplate
    {
        private readonly IFileManager manager = FileManager.Instance;

        private readonly DataTable observed;

        public ObservedWriter(DataTable table)
        {
            observed = table;
        }

        public void Export()
        {
            var info = Directory.CreateDirectory(manager.ExportPath + "\\obs");

            using var stream = new FileStream(info.FullName + $"\\{observed.TableName}.csv", FileMode.Create);
            using var writer = new StreamWriter(stream);

            var builder = new StringBuilder();

            var columns = observed.Columns.Cast<DataColumn>();
            builder.AppendLine(string.Join(' ', columns.Select(c => c.ColumnName)));

            // Format and add the data
            foreach (DataRow row in observed.Rows)
                builder.AppendLine(string.Join(' ', row.ItemArray));

            writer.Write(builder.ToString());
            writer.Close();
        }
    }
}
