using System;
using System.Data;
using System.Windows.Forms;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages data taken from an excel spreadsheet
    /// </summary>
    public interface IExcelData<TData> : IDisposable
        where TData : IDisposable
    {
        /// <summary>
        /// Occurs when the data is changed
        /// </summary>
        event Action<string, object> StateChanged;

        /// <summary>
        /// The raw data
        /// </summary>
        TData Data { get; }

        /// <summary>
        /// The name of the data
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The table which the data is sourced from
        /// </summary>
        DataTable Source { get; }

        /// <summary>
        /// The state of the data
        /// </summary>
        PropertyCollection State { get; }

        /// <summary>
        /// If the data is a column, swap its position to the given index
        /// </summary>
        void Swap(int index);
    }

    public abstract class BaseExcelData<TData> : IExcelData<TData>
        where TData : IDisposable
    {
        public TData Data { get; protected set; }
        
        public abstract string Name { get; set; }
        public abstract DataTable Source { get; }
        public abstract PropertyCollection State { get; }

        public event Action<string, object> StateChanged;

        protected void InvokeStateChanged(string state, object value)
            => StateChanged?.Invoke(state, value);

        public abstract void Swap(int index);

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

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                StateChanged = null;
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

        /// <inheritdoc/>
        public override PropertyCollection State => Data.ExtendedProperties;

        public ExcelTable(DataTable table)
        {
            Data = table;
            State["Ignore"] = false;
            State["Valid"] = true;            
        }

        /// <inheritdoc/>
        public override void Swap(int i)
        {
            throw new NotImplementedException("Cannot swap the ordinal of a table");
        }
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

        /// <inheritdoc/>
        public override PropertyCollection State => Data.ExtendedProperties;

        public ExcelColumn(DataColumn column)
        {
            Data = column;

            var info = column.FindProperty();
            State["Info"] = info;
            State["Ignore"] = false;                       
        }

        /// <inheritdoc/>
        public void ConfigureMenu(params MenuItem[] items)
        {
            if (State["Info"] != null)
            {
                items[2].Enabled = false;
                return;
            }

            items[3].MenuItems.Clear();

            var props = Data.GetUnmappedProperties();

            foreach (var prop in props)
                items[3].MenuItems.Add(prop.Name, SetProperty);
        }

        /// <inheritdoc/>
        private void SetProperty(object sender, EventArgs args)
        {
            var item = sender as MenuItem;
            Name = item.Text;
            State["Info"] = Data.FindProperty();
            InvokeStateChanged("Valid", true);
        }

        /// <inheritdoc/>
        public override void Swap(int index)
        {
            Data.SetOrdinal(index);
        }
    }
}
