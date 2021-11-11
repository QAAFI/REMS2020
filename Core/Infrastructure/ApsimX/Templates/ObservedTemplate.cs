using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Rems.Infrastructure.ApsimX
{
    public class ObservedTemplate : IRemsTemplate
    {
        private readonly IQueryHandler Handler;

        private readonly IFileManager manager = FileManager.Instance;

        private readonly int[] IDs;

        private readonly string filename;

        public ObservedTemplate(IQueryHandler handler, int[] ids, string name)
        {
            Handler = handler;
            IDs = ids;
            filename = name;
        }

        public void Export()
        {
            var observed = Handler.Query(new ObservedDataQuery { IDs = IDs }).Result;

            var info = Directory.CreateDirectory(manager.ExportPath + "\\obs");

            using var stream = new FileStream(info.FullName + $"\\{filename}_Observed.csv", FileMode.Create);
            using var writer = new StreamWriter(stream);

            var builder = new StringBuilder();

            var columns = observed.Columns.Cast<DataColumn>();
            builder.AppendLine(string.Join(',', columns.Select(c => c.ColumnName)));

            // Format and add the data
            foreach (DataRow row in observed.Rows)
                builder.AppendLine(string.Join(',', Format(row.ItemArray)));

            writer.Write(builder.ToString());
            writer.Close();
        }

        private IEnumerable<string> Format(object[] items)
        {
            foreach (var item in items)
            {
                if (item is DateTime date)
                    yield return date.ToString("dd/MM/yyyy");

                else if (item is double value)
                    yield return value.ToString("F2");

                else
                    yield return item.ToString();
            }
        }
    }
}
