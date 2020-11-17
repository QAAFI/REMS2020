using Rems.Application.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class ImportValidater : Validater
    {    
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
                        row.Color = Color.White;
                        return;
                    };

                row.IsValid = false;
                row.Color = Color.Crimson;
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

        public void OnFoundInvalids(DataColumn[] columns)
        {
            foreach (var col in columns)            
                AddRow(col, col.Table.TableName);            

            MessageBox.Show("Unknown items found in data. Please validate before importing.");
        }
    }
}
