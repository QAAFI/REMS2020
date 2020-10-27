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
using Rems.Application.Common.Models;
using Castle.Core.Internal;
using Steema.TeeChart.Styles;
using Rems.Application.Common.Interfaces;

namespace WindowsClient.Controls
{
    public partial class Validater : UserControl
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

        public Validater()
        {
            InitializeComponent();

            Enter += OnClick;

            dataGrid.CellEndEdit += OnCellEndEdit;
        }        

        private void OnClick(object sender, EventArgs e)
        {
            if (dataGrid.Rows.Count == 1) FillRows();
        }

        private void FillRows()
        {
            var red = new DataGridViewCellStyle() { BackColor = Color.Red };

            dataGrid.Rows.Clear();
            foreach (var item in Items)
            {
                var row = new ValidaterRow();
                row.CreateCells(dataGrid);
                row.SetValues(item.Key, item.Value);
                SendQuery(new TraitExistsQuery() { Validater = row });

                dataGrid.Rows.Add(row);
            }

            Refresh();
        }

        private void OnCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = dataGrid.Rows[e.RowIndex] as ValidaterRow;

            var exists = new TraitExistsQuery() { Validater = row };
            SendQuery(exists);
        }

        public IItemValidater HandleMissingItem(string item)
        {
            foreach (ValidaterRow row in dataGrid.Rows)            
                if (row.Name == item) return row;            

            throw new Exception("The requested item is not handled by the validater");
        }
    }

    public class ValidaterRow : DataGridViewRow, IItemValidater
    {
        public string Name 
        { 
            get => Cells[0].Value.ToString();            
            set => Cells[0].Value = value;            
        }

        public string Values 
        {
            get => Cells[1].Value.ToString();
            set => Cells[1].Value = value;
        }

        public string Item { get; set; }

        private bool valid;
        public bool IsValid 
        {
            get => valid; 
            set
            {
                valid = value;

                if (valid) DefaultCellStyle.BackColor = Color.White;
                else DefaultCellStyle.BackColor = Color.Red;
                
            }
        }
    }
}
