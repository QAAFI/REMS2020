using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Utils = Rems.Application.Common.Utilities;

namespace Rems.Infrastructure.Excel
{
    public class ExcelReader
    {
        /// <summary>
        /// The excel data
        /// </summary>
        public DataSet Data { get; set; }

        public void LoadFromFile(string file)
        {
            var format = Path.GetExtension(file);

            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook book;

            if (format == ".xls")
                book = new HSSFWorkbook(stream);
            else if (format == ".xlsx")
                book = new XSSFWorkbook(stream);
            else
                throw new Exception("Unknown file format: " + format);

            Data = new DataSet(Path.GetFileNameWithoutExtension(file));

            for (int i = 0; i < book.NumberOfSheets; i++)
            {
                var sheet = book.GetSheetAt(i);
                AddTable(sheet);
            }
        }

        private void AddTable(ISheet sheet)
        {
            if (sheet.GetRow(0) is not IRow header) return;

            var table = CreateTable(header, sheet.SheetName);
            
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (row != null)
                    AddRow(row, table);
            }

            Data.Tables.Add(table);
        }

        private DataTable CreateTable(IRow header, string title)
        {
            var table = new DataTable(title);

            foreach (ICell head in header.Cells)
            {
                if (head.CellType != CellType.Blank)
                    table.Columns.Add(head.ToString());
            }

            return table;
        }

        private void AddRow(IRow row, DataTable table)
        {
            var data = table.NewRow();
            bool any = false;
             
            foreach (ICell c in row.Cells)
            {
                if (c.CellType == CellType.Blank)
                    continue;                

                if (c.ColumnIndex >= data.ItemArray.Length)
                    throw new IndexOutOfRangeException($"The sheet {row.Sheet.SheetName} contains extra data in cell {c.Address}." +
                        $" Please ensure data is only listed under titled columns.");

                switch (c.CellType)
                {
                    case CellType.String:
                        if (double.TryParse(c.StringCellValue, out double value))
                            data[c.ColumnIndex] = value;
                        else if (DateTime.TryParse(c.StringCellValue, out DateTime date))
                            data[c.ColumnIndex] = date;
                        else if (c.StringCellValue != "")
                            data[c.ColumnIndex] = c.StringCellValue;
                        else
                            continue; // Ignore empty cells by continuing here
                        break;

                    case CellType.Numeric:
                        if (c.ToString().Contains("-"))
                            data[c.ColumnIndex] = c.ToString();
                        else
                            data[c.ColumnIndex] = c.NumericCellValue;
                        break;

                    default:
                        break;
                }
                any = true;
            }

            if (any) table.Rows.Add(data);
        }

        public Dictionary<ExcelTable, ExcelColumn[]> ConvertData(string format)
        {
            var types = Utils.GetFormatTypes(format);

            if (Data.Tables.OfType<DataTable>().Any(t => t.TableName == "Notes"))
                Data.Tables.Remove("Notes");

            Data.FindExperiments();
            if (format == "Data")
                Data.Tables.Remove("Experiments");

            var data = Data.Tables.OfType<DataTable>();
            var tables = new Dictionary<ExcelTable, ExcelColumn[]>();

            foreach (var type in types)
            {
                var table = data.FirstOrDefault(t => type.IsExpected(t.TableName));

                if (table is not null)
                {
                    table.ExtendedProperties["Type"] = type;
                    table.RemoveDuplicateRows();
                    table.RemoveEmptyColumns();

                    if (type.GetProperty("Experiment") is PropertyInfo info)
                        table.ConvertExperiments(info);
                }

                var excel = new ExcelTable { Data = table, Type = type, Required = type.IsRequired() };
                tables.Add(excel, excel.GetColumns(type));
            }

            return tables;
        }
    }
}
