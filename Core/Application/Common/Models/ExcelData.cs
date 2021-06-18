using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace Rems.Application.Common
{
    /// <summary>
    /// Manages data taken from an excel spreadsheet
    /// </summary>
    public interface IExcelData<TData> : IDisposable
        where TData : IDisposable
    {
        /// <summary>
        /// The raw data
        /// </summary>
        TData Data { get; }

        bool Valid { get; set; }

        bool Ignore { get; set; }

        /// <summary>
        /// The name of the data
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The table which the data is sourced from
        /// </summary>
        DataTable Source { get; }

        ///// <summary>
        ///// If the data is a column, swap its position to the given index
        ///// </summary>
        //void Swap(int index);
    }

    public abstract class BaseExcelData<TData> : IExcelData<TData>
        where TData : IDisposable
    {
        public TData Data { get; init; }
        
        public bool Valid { get; set; }

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

    public class ExcelTable : BaseExcelData<DataTable>
    {
        /// <inheritdoc/>
        public override string Name
        {
            get => Data.TableName;
            set => Data.TableName = value;
        }

        /// <inheritdoc/>
        public override DataTable Source => Data;

        public Type Type { get; set; }

        ///// <inheritdoc/>
        //public override void Swap(int i)
        //{
        //    throw new NotImplementedException("Cannot swap the ordinal of a table");
        //}
    }

    public class ExcelColumn : BaseExcelData<DataColumn>
    {
        /// <inheritdoc/>
        public override string Name
        {
            get => Data.ColumnName;
            set => Data.ColumnName = value;
        }

        /// <inheritdoc/>
        public override DataTable Source => Data.Table;

        public PropertyInfo Info { get; set; }

        ///// <inheritdoc/>
        //public override void Swap(int index)
        //{
        //    Data.SetOrdinal(index);
        //}        
    }
}
