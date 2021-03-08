using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages data taken from an excel spreadsheet
    /// </summary>
    public interface IExcelData
    {
        /// <summary>
        /// Occurs when the data is changed
        /// </summary>
        event Action<string, object> StateChanged;

        /// <summary>
        /// The raw data
        /// </summary>
        object Data { get; }

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
        /// Connects the given menu items to the data, allowing modification
        /// </summary>
        void ConfigureMenu(params MenuItem[] items);

        /// <summary>
        /// If the data is a column, swap its position to the given index
        /// </summary>
        void Swap(int index);
    }

    public class ExcelTable : IExcelData
    {
        readonly DataTable table;

        /// <inheritdoc/>
        public object Data => table;

        /// <inheritdoc/>
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public string Name
        {
            get => table.TableName;
            set => table.TableName = value;
        }

        /// <inheritdoc/>
        public DataTable Source => table;

        /// <inheritdoc/>
        public PropertyCollection State => table.ExtendedProperties;

        public ExcelTable(DataTable table)
        {
            this.table = table;           

            State["Ignore"] = false;
            State["Valid"] = true;            
        }

        /// <inheritdoc/>
        public void ConfigureMenu(params MenuItem[] items)
        {
            // Not used
        }

        /// <inheritdoc/>
        public void Swap(int i)
        {
            throw new NotImplementedException();
        }
    }

    public class ExcelColumn : IExcelData
    {
        readonly DataColumn column;

        /// <inheritdoc/>
        public object Data => column;

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public string Name
        {
            get => column.ColumnName;
            set => column.ColumnName = value;
        }

        /// <inheritdoc/>
        public DataTable Source => column.Table;

        /// <inheritdoc/>
        public PropertyCollection State => column.ExtendedProperties;

        public ExcelColumn(DataColumn column)
        {
            this.column = column;

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

            var props = column.GetUnmappedProperties();

            foreach (var prop in props)
                items[3].MenuItems.Add(prop.Name, SetProperty);
        }

        /// <inheritdoc/>
        private void SetProperty(object sender, EventArgs args)
        {
            var item = sender as MenuItem;
            Name = item.Text;
            State["Info"] = column.FindProperty();
            StateChanged?.Invoke("Valid", true);
        }

        /// <inheritdoc/>
        public void Swap(int index)
        {
            column.SetOrdinal(index);
        }
    }
}
