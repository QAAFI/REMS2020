using EFCore.BulkExtensions;
using ExcelDataReader;
using Microsoft.Data.Sqlite;
using REMS;
using REMS.Context;
using REMS.Context.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
                    var bulkConfig = new BulkConfig(){};
 
                    if (table.TableName == "PlotData")
                    {
                        ImportPlotData(table, dbContext);
                    }
                    else 
                    {
                        var entities = ReadEntitiesFromTable(table, dbContext);
                        if (table.TableName == "SoilLayerDatas")
                        {
                            bulkConfig.PropertiesToExclude = new List<string> { nameof(SoilLayerData.SoilLayerDataId) };
                            dbContext.BulkInsert(entities.Cast<SoilLayerData>().ToList());
                        }
                        else if (table.TableName == "MetDatas")
                        {
                            bulkConfig.PropertiesToExclude = new List<string> { nameof(MetData.MetStationId), nameof(MetData.TraitId), nameof(MetData.Date) };
                            dbContext.BulkInsert(entities.Cast<MetData>().ToList());
                        }
                        else
                        {
                            NewImportTable(dbContext, table, entities);
                        }
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

        private static IEnumerable<IEntity> ReadEntitiesFromTable(DataTable table, REMSContext dbContext)
        {
            var values = table.Rows.Cast<DataRow>().Select(r => r.ItemArray);
            var names = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

            var entity = dbContext.Entities.Where(e => e.CheckName(table.TableName)).FirstOrDefault();
            if (entity != null)
            {
                return values.Select(v => entity.Create(v, names));
            }
            return null;
        }

        private static void ImportPlotData(DataTable table, REMSContext dbContext)
        {
            try
            {
                //each row can have multiple samples - any data after the first 4 cols is an individual sample
                //exp id, plot id, date sample
                var plotdataList = new List<PlotData>();
                var TraitList = new List<Trait>();
                var firstRow = table.Rows[0];
                
                for(int c = 4; c < table.Columns.Count; ++c)
                {
                    var trait = dbContext.Traits.Where(t => t.Name == table.Columns[c].ColumnName).FirstOrDefault();
                    if(trait == null)
                    {
                        var tmptrait = trait;
                    }
                    TraitList.Add(trait);
                }

                foreach (DataRow row in table.Rows)
                {
                    var plotDataDate = row.Field<DateTime>("date");
                    var sample = row.Field<string>("Sample");
                    var plot = Convert.ToInt32(row.Field<double>("Plot"));

                    for (int i = 4; i < row.ItemArray.Length; ++i)
                    {
                        var val = row.ItemArray[i].ToString();
                        if (val != "")
                        {
                            var plotdata = new PlotData()
                            {
                                PlotId = plot,
                                PlotDataDate = plotDataDate,
                                Sample = sample,
                                TraitId = TraitList[i-4].TraitId,
                                Value = Convert.ToDouble(val),
                                UnitId = TraitList[i-4].UnitId
                            };
                            plotdataList.Add(plotdata);
                        }
                    }
                }
                var bulkConfig = new BulkConfig()
                {
                    PropertiesToExclude = new List<string> { nameof(PlotData.PlotDataId) }
                };
                dbContext.BulkInsert(plotdataList, bulkConfig);
            }
            catch (Exception ex)
            {
                var itemCount = dbContext.PlotDatas.Count();
                throw;
            }
        }
    

        private static void NewImportTable(REMSContext dbContext, DataTable table, IEnumerable<IEntity> entities)
        {
            try
            {
                dbContext.AddRange(entities);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var tmp = table.TableName;
                throw;
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
