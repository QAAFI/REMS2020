using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;
using Castle.Core.Internal;
using Steema.TeeChart.Styles;
using Rems.Application.Common.Interfaces;
using System.Collections.Immutable;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public abstract partial class Validater : UserControl
    {
        public QueryHandler SendQuery;
        
        protected DataGridView grid => dataGrid;

        public Validater()
        {
            InitializeComponent();

            //dataGrid.CellEndEdit += OnCellEndEdit;
            dataGrid.CellValueChanged += OnCellValueChanged;
        }

        public void Clear()
        {
            grid.Rows.Clear();
            ignoreBox.Checked = false;
        }

        private void OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGrid.Rows[e.RowIndex] as ValidaterRow;

            if (row.Ignore)
                row.Color = Color.LightGray;
            else
                ValidateRow(row);
        }  

        //private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    var row = dataGrid.Rows[e.RowIndex] as ValidaterRow;
        //    ValidateRow(row);            
        //}

        protected void AddRow(object item, string values)
        {
            var row = new ValidaterRow();
            row.CreateCells(dataGrid);
            row.SetValues(item, values);
            ValidateRow(row);

            dataGrid.Rows.Add(row);
        }

        protected abstract void ValidateRow(ValidaterRow row);

        /// <summary>
        /// Searches the validater for the requested item
        /// </summary>
        /// <param name="item"></param>
        /// <returns>An alternative name, or null if none were found</returns>
        public string HandleMissingItem(string item)
        {
            foreach (IItemValidater row in dataGrid.Rows)
                if (row.Name == item) 
                    return row.Item;

            return "";
        }

        private void ignoreBoxChecked(object sender, EventArgs e)
        {
            foreach (ValidaterRow row in grid.Rows)
                row.Cells[2].Value = ignoreBox.Checked;
        }
    }
    
}
