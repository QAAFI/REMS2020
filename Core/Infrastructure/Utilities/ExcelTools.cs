using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Data;
using System.IO;

namespace Rems.Infrastructure.Utilities
{
    public static class ExcelTools
    {
        public static DataSet ReadAsDataSet(string file)
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

            var set = new DataSet(Path.GetFileNameWithoutExtension(file));

            for (int i = 0; i < book.NumberOfSheets; i++)
            {
                var sheet = book.GetSheetAt(i);
                AddTable(sheet, set);
            }
            return set;
        }

        private static void AddTable(ISheet sheet, DataSet set)
        {
            if (!(sheet.GetRow(0) is IRow header)) return;

            var table = CreateTable(header, sheet.GetRow(1), sheet.SheetName);
            
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (row != null)
                    AddRow(row, table);
            }

            set.Tables.Add(table);
        }

        private static DataTable CreateTable(IRow header, IRow first, string title)
        {
            var table = new DataTable(title);

            foreach (ICell head in header.Cells)
            {
                if (head.CellType != CellType.Blank)
                    table.Columns.Add(head.ToString());
            }

            return table;
        }

        private static void AddRow(IRow row, DataTable table)
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

    }
}
