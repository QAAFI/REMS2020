using System;

namespace Rems.Application.Common
{
    public class DataTypeException : Exception
    {
        public DataTypeException(string column, string table, string type)
            : base($"Column {column} in {table} contains invalid data." +
                  $"\nData must be of type: {type}")
        { }
    }
}
