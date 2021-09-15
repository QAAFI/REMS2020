using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace Rems.Infrastructure
{
    /// <summary>
    /// Manages data taken from an excel spreadsheet
    /// </summary>
    public abstract class ExcelData<TData>
        where TData : IDisposable
    {
        public TData Data { get; init; }

        public bool Ignore { get; set; }

        public abstract string Name { get; set; }

        public abstract DataTable Source { get; }

        #region Disposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Data.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ExcelTable : ExcelData<DataTable>
    {
        /// <inheritdoc/>
        public override string Name
        {
            get => Data?.TableName;
            set => Data.TableName = value;
        }

        /// <inheritdoc/>
        public override DataTable Source => Data;

        public Type Type { get; set; }

        public bool Required { get; init; }

        public ExcelColumn[] GetColumns(Type type)
        {
            var cols = Data?.Columns.OfType<DataColumn>();

            var columns = new List<ExcelColumn>();

            foreach (var prop in type.ExpectedProperties())
            {
                var col = cols?.FirstOrDefault(c => prop.IsExpected(c.ColumnName));
                if (col is not null)
                    col.ColumnName = prop.Name;

                var xl = new ExcelColumn
                {
                    Info = prop,
                    Data = col ?? new DataColumn(prop.Name + " not found"),
                };

                columns.Add(xl);
            }

            return columns.ToArray();
        }
    }

    public class ExcelColumn : ExcelData<DataColumn>
    {
        /// <inheritdoc/>
        public override string Name
        {
            get => Data.ColumnName;
            set => Data.ColumnName = value;
        }

        /// <inheritdoc/>
        public override DataTable Source => Data?.Table;

        public PropertyInfo Info { get; set; }    
    }
}
