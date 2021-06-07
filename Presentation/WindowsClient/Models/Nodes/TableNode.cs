using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class TableNode : DataNode<DataTable, ITableValidator>
    {
        public TableNode(ExcelTable excel, ITableValidator validator) : base(excel, validator)
        {
            items.Add(new ToolStripMenuItem("Add invalids as traits", null, AddTraits));
            items.Add(new ToolStripMenuItem("Ignore all invalids", null, IgnoreAll));
        }

        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            // No configuration required
        }

        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public async void AddTraits(object sender, EventArgs args)
        {
            foreach (ColumnNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    await n.AddTrait(sender, args);
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private async void IgnoreAll(object sender, EventArgs args)
        {
            foreach (ColumnNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.ToggleIgnore(null, args);
        }

        public override void Validate()
        {
            foreach (GroupNode node in Nodes)
                node.Validate();

            Validator.Validate();
        }
    }
}
