using Rems.Infrastructure.Excel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class TableNode : DataNode<ExcelTable, DataTable>
    {
        public GroupNode Required = new("Required")
        {
            Advice = new("These nodes describe the columns that REMS expects to find in the spreadsheet" +
                "and are required for the import."),
            Key = "Properties"
        };

        public GroupNode Traits = new("Traits")
        {
            Advice = new("Any additional columns in the spreadsheet will be imported as trait data."),
            Key = "Traits"
        };

        public override string Key 
        {
            get
            {
                if (Excel.Ignore)
                    return "ExcelOff";
                else if (!Valid)
                    return "Warning";
                else
                    return "Excel";                
            }
        }

        public TableNode(ExcelTable excel) : base(excel)
        {
            Nodes.Add(Required);
            Nodes.Add(Traits);

            var ignore = new ToolStripMenuItem("Ignore", null, (s, e) => ToggleIgnore())
            {
                Enabled = !Excel.Required,
                ToolTipText = Excel.Required
                ? "This table is required and cannot be ignored in the import"
                : "Ignore this table and do not import its data"
            };

            Items.Add(ignore);

            Traits.Items.Add(new ToolStripMenuItem("Add all as Traits", null, AddAllNodes));
            Traits.Items.Add(new ToolStripMenuItem("Toggle ignore", null, ToggleIgnoreAll));
        }

        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public async void AddAllNodes(object sender, EventArgs args)
        {
            var nodes = TreeView.SelectedNode.Nodes.OfType<TraitNode>();
            foreach (var node in nodes)
                if (!node.Valid)
                    await node.Add.Invoke();

            Refresh();
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private void ToggleIgnoreAll(object sender, EventArgs args)
        {
            var nodes = TreeView.SelectedNode.Nodes.OfType<DataNode<ExcelColumn, DataColumn>>();
            foreach (var node in nodes)
                node.ToggleIgnore();

            Refresh();
        }
    }
}
