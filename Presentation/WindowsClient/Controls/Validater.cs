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

        public Dictionary<string, string> Items = new Dictionary<string, string>()
        {
            // Physical
            { "BD", "" },
            { "AirDry", "" },
            { "LL15", "" },
            { "DUL", "" },
            { "SAT", "" },
            { "KS", "" },
                
            // SoilCrop
            { "LL", "" },
            { "KL", "" },
            { "XF", "" },

            // WaterBalance
            { "SWCON", "" },
            { "KLAT", "" },

            // Organic
            { "Carbon", "" },
            { "SoilCNRatio", "" },
            { "FBiom", "" },
            { "FInert", "" },
            { "FOM", "" },

            // Chemical
            { "NO3N", "" },
            { "NH4N", "" },
            { "PH", "" },

            // Sample
            { "SW", "" },
        };

        protected DataGridView grid => dataGrid;

        public Validater()
        {
            InitializeComponent();

            Enter += OnClick;

            dataGrid.CellEndEdit += OnCellEndEdit;
            dataGrid.CellMouseDown += OnCellMouseDown;
        }

        private void OnCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 2) return;

            var row = dataGrid.Rows[e.RowIndex] as ValidaterRow;

            if (row.Ignore) row.Color = Color.Yellow;

            dataGrid.ClearSelection();
            dataGrid.Refresh();
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count == 1) FillRows();
        }        

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGrid.Rows[e.RowIndex] as ValidaterRow;
            ValidateRow(row);            
        }

        protected abstract void FillRows();

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
    }
    
}
