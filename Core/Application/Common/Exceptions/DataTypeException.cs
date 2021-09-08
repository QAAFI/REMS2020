using System;
using System.Data;

namespace Rems.Application.Common
{
    public class DataTypeException : Exception
    {
        public DataTypeException(DataRow row, string column, string type)
            : base($"Invalid data found in the {row.Table.TableName} table:" +
                  $"\n     Row: {row.Table.Rows.IndexOf(row) + 1}" +
                  $"\n  Column: {column}" +
                  $"\n   Value: {row[column]}" +
                  $"\nData value must be of type {type}.")
        { }
    }
}
