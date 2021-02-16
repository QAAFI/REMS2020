using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Models
{
    public interface IExcelData
    {
        event Action<string, object> StateChanged;

        object Tag { get; }

        string Name { get; set; }

        DataTable Source { get; }

        PropertyCollection State { get; }

        List<MenuItem> Items { get; }

        void SetMenu(params MenuItem[] items);

        void Swap(int i);
    }

    public class ExcelTable : IExcelData
    {
        readonly DataTable table;

        public object Tag => table;

        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public event Action<string, object> StateChanged;

        public string Name
        {
            get => table.TableName;
            set => table.TableName = value;
        }

        public DataTable Source => table;

        public PropertyCollection State => table.ExtendedProperties;

        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public ExcelTable(DataTable table)
        {
            this.table = table;           

            State["Ignore"] = false;
            State["Valid"] = true;            
        }

        

        public void SetMenu(params MenuItem[] items)
        {
            // Not used
        }

        public void Swap(int i)
        {
            throw new NotImplementedException();
        }
    }

    public class ExcelColumn : IExcelData
    {
        readonly DataColumn column;

        public object Tag => column;

        public event Action<string, object> StateChanged;

        public string Name
        {
            get => column.ColumnName;
            set => column.ColumnName = value;
        }

        public DataTable Source => column.Table;

        public PropertyCollection State => column.ExtendedProperties;

        public List<MenuItem> Items { get; } = new List<MenuItem>();

        public ExcelColumn(DataColumn column)
        {
            this.column = column;

            var info = column.FindProperty();
            State["Info"] = info;
            State["Ignore"] = false;                       
        }

        public void SetMenu(params MenuItem[] items)
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

        private void SetProperty(object sender, EventArgs args)
        {
            var item = sender as MenuItem;
            Name = item.Text;
            State["Info"] = column.FindProperty();
            StateChanged?.Invoke("Valid", true);
        }

        public void Swap(int index)
        {
            column.SetOrdinal(index);
        }
    }
}
