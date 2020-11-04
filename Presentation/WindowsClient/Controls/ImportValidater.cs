using Rems.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class ImportValidater : Validater
    {
        protected override void FillRows()
        {
            //throw new NotImplementedException();
        }

        public bool AllValid()
        {
            foreach (ValidaterRow row in grid.Rows)
                if (!row.IsValid && !row.Ignore)
                    return false;

            return true;
        }

        protected override void ValidateRow(ValidaterRow row)
        {
            if (row.Cells[0].Value is DataColumn col)
            {
                foreach (var item in row.Values)
                    if (ItemIsProperty(col, item))
                    {
                        row.IsValid = true;
                        return;
                    };

                row.IsValid = false;
            }
            else
            {
                throw new Exception("No column data found in row");
            }
        }

        private bool ItemIsProperty(DataColumn col, string item)
        {
            var type = col.Table.ExtendedProperties["Type"] as Type;

            if (type.GetProperty(item) is PropertyInfo)
            {
                col.ColumnName = item;
                return true;
            }

            return false;
        }

        public void OnFoundInvalids(DataColumn[] items)
        {
            foreach (var item in items) 
                AddRow(item, "");

            MessageBox.Show("Unknown items found in data. Please validate before importing.");
        }
    }
}
