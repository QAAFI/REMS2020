using Rems.Infrastructure.Excel;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class InvalidNode : DataNode<ExcelTable, DataTable>
    {
        public override bool Valid => false;

        public override string Key 
        { 
            get => "Invalid" + (Excel.Ignore ? "Off" : "On"); 
            init => base.Key = value;
        }

        public InvalidNode(ExcelTable excel) : base(excel)
        {
            Name = excel.Type.Name;
            Text = Name;

            var ignore = new ToolStripMenuItem("Ignore", null, (s, e) => ToggleIgnore());
            Items.Add(ignore);

            Refresh();
        }
    }
}
