using ExcelDataReader;
using Microsoft.Data.Sqlite;
using REMS;
using REMS.Context;
using REMS.Context.Entities;
using System;
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
                try
                {
                    if (table.TableName == "PlotData")
                    {
                        ImportPlotData(table, dbContext);
                    }
                    else if (table.TableName == "ExperimentInfos")
                    {
                        var exp = dbContext.Experiments;
                        var infos = dbContext.ExperimentInfos;

                        NewImportTable(dbContext, table);
                    }
                    else
                    {
                        NewImportTable(dbContext, table);
                    }
                }
                catch(Exception ex)
                {
                    var name = table.TableName;
                    var tmp = ex.Message;
                    var tmp2 = ex.InnerException.Message;
                }
            }
        }

        private static void ImportPlotData(DataTable table, REMSContext dbContext)
        {
            //each row can have multiple samples - any data after the first 4 cols is an individual sample
            //exp id, plot id, date sample
            foreach (DataRow row in table.Rows)
            {
                for(int i = 4; i < row.ItemArray.Length; ++i)
                {
                    var val = row.ItemArray[i].ToString();
                    if (val != "")
                    { 
                        var trait = dbContext.Traits.Where(t => t.Name == table.Columns[i].ColumnName).FirstOrDefault();
                        if (trait != null) 
                        {
                            var plt = Convert.ToInt32(row.Field<double>("Plot"));
                            var existingPlot = dbContext.Plots.Where(p => p.PlotId == plt);
                            if(existingPlot == null)
                            {
                                int tmp = plt;
                            }
                            var dt = row.Field<DateTime>("date");
                            var tid = trait.TraitId;
                            var sample = row.Field<string>("Sample");
                            var val2 = val;

                            var plotdata = new PlotData()
                            {
                                PlotId = Convert.ToInt32(row.Field<double>("Plot")),
                                PlotDataDate = row.Field<DateTime>("date"),
                                Sample = row.Field<string>("Sample"),
                                TraitId = trait.TraitId,
                                Value = Convert.ToDouble(val),
                                UnitId = trait.UnitId
                            };
                            dbContext.PlotDatas.Add(plotdata);
                        }
                    }
                }
            }
            dbContext.SaveChanges();
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
