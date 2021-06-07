using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public class TableNode : DataNode<DataTable, ITableValidator>
    {
        public GroupNode Properties = new GroupNode("Properties")
        {
            Advice = new("These nodes represent information about the experiment"),
            ImageKey = "Properties",
            SelectedImageKey = "Properties"
        };

        public GroupNode Traits = new GroupNode("Traits")
        {
            Advice = new("These nodes represent trait data with measured values"),
            ImageKey = "Traits",
            SelectedImageKey = "Traits"
        };

        public GroupNode Unknowns = new GroupNode("Unknowns")
        {
            Advice = new("These nodes represent unidentified data types"),
            ImageKey = "Question",
            SelectedImageKey = "Question"
        };

        public override string Key 
        {
            get
            {
                if (Excel.State["Ignore"] is true)
                    return "ExcelOff";
                else if (Excel.State["Valid"] is not true)
                    return "Warning";
                else
                    return "Excel";                
            }
        }

        public TableNode(ExcelTable excel, ITableValidator validator) : base(excel, validator)
        {
            Nodes.Add(Properties);
            Nodes.Add(Traits);
            Nodes.Add(Unknowns);

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
