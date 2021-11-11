using Rems.Application.Common.Interfaces;
using Rems.Application.CQRS;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Rems.Infrastructure.ApsimX.Writers
{
    public class MetTemplate : IRemsTemplate
    {
        private readonly IQueryHandler Handler;

        private readonly IFileManager manager = FileManager.Instance;

        private readonly int[] IDs;

        public MetTemplate(IQueryHandler handler, int[] ids)
        {
            Handler = handler;
            IDs = ids;
        }

        public void Export()
        {
            var stations = Handler.Query(new MetFileDataQuery { ExperimentIds = IDs }).Result;

            foreach (DataTable table in stations.Tables)
                ExportMetFile(table);
        }

        private void ExportMetFile(DataTable table)
        {
            var info = Directory.CreateDirectory(manager.ExportPath + "\\met");

            var start = new DateTime(table.Rows[0].Field<int>("Year"), 1, 1).AddDays(table.Rows[0].Field<int>("Day"));
            var name = table.TableName.Replace('/', '-').Replace(' ', '_') + "_" + start.ToString("MMM-yyyy") + ".met";

            using var stream = new FileStream(info.FullName + '\\' + name, FileMode.Create);
            using var writer = new StreamWriter(stream);

            // Attach header lines to the file
            var builder = new StringBuilder();
            builder.AppendLine("[weather.met.weather]");
            builder.AppendLine($"!station name = {table.TableName}");
            builder.AppendLine($"latitude = {table.ExtendedProperties["Latitude"]} (DECIMAL DEGREES)");
            builder.AppendLine($"longitude = {table.ExtendedProperties["Longitude"]} (DECIMAL DEGREES)");
            builder.AppendLine($"tav = {table.ExtendedProperties["TemperatureAverage"]} (oC) \t ! annual average ambient temperature");
            builder.AppendLine($"amp = {table.ExtendedProperties["Amplitude"]} (oC) \t ! annual amplitude in mean monthly temperature");
            builder.AppendLine();

            var columns = table.Columns.Cast<DataColumn>();
            builder.AppendLine(string.Join(' ', columns.Select(c => c.ColumnName)));
            builder.AppendLine(string.Join(' ', columns.Select(c => "()")));

            // Format and add the data
            foreach (DataRow row in table.Rows)            
                builder.AppendLine(string.Join(' ', row.ItemArray));

            writer.Write(builder.ToString());
            writer.Close();
        }
    }
}
