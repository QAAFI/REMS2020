using Rems.Application.Common.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class ValidaterRow : DataGridViewRow, IItemValidater
    {
        public ValidaterRow(DataGridView grid, params object[] values) : base()
        {
            CreateCells(grid);
            SetValues(values);

            var combo = (DataGridViewComboBoxCell)Cells[2];
            combo.Style.BackColor = Color.White;
            combo.Items.AddRange("Ignore", "Add trait", "Set property");
        }

        public string Name
        {
            get => Cells[0].Value.ToString();
            set => Cells[0].Value = value;
        }

        public string[] Values
        {
            get => Cells[2].Value is object o ? o.ToString().Split(',').Select(s => s.Trim()).ToArray() : new string[0];
            set => Cells[2].Value = string.Join(", ", value);
        }

        public string Item { get; set; } = "";

        public bool IsValid { get; set; }

        public bool Ignore
        {
            get => Cells[3].Value is true;
        }

        public Color Color
        {
            get => DefaultCellStyle.BackColor;
            set => DefaultCellStyle.BackColor = value;
        }
    }

    public class ItemValidater : IItemValidater
    {
        public string Name { get; set; }
        public string[] Values { get; set; }
        public string Item { get; set; }
        public bool IsValid { get; set; } = false;

        public bool Ignore { get; set; } = false;
    }
}
