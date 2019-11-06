using ExcelDataReader;
using Microsoft.Data.Sqlite;
using REMS;
using REMS.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Services
{
    public static class ImportService
    {
        public static void ImportExcelData(this IREMSDatabase db, string file)
        {
            var dbContext = (db as REMSDatabase).context;
            using var excel = ExcelImporter.ReadRawData(file);
            using var connection = new SqliteConnection(dbContext.ConnectionString);
            connection.Open();

            foreach (DataTable table in excel.Tables)
            {
                NewImportTable(dbContext, table);
            }
        }

        private static void NewImportTable(REMSContext dbContext, DataTable table)
        {
            var values = table.Rows.Cast<DataRow>().Select(r => r.ItemArray);
            var names = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

            var entity = dbContext.Entities.Where(e => e.CheckName(table.TableName)).FirstOrDefault();
            if(entity != null)
            {
                var entities = values.Select(v => entity.Create(v, names));

                dbContext.AddRange(entities);
                dbContext.SaveChanges();
            }
        }

        public static class ExcelImporter
        {
            public static DataSet ReadRawData(string filePath)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        return reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                    }
                }
            }

        }
    }
}
