using System;
using System.Data;
using System.Linq;
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

        public GroupNode Ignored = new GroupNode("Ignored")
        {
            Advice = new("These nodes are ignored during import"),
            ImageKey = "Ignore",
            SelectedImageKey = "Ignore"
        };

        public override string Key 
        {
            get
            {
                if (Ignore)
                    return "ExcelOff";
                else if (!Validator.Valid)
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
            Nodes.Add(Ignored);

            Unknowns.Items.Add(new ToolStripMenuItem("Add all as Traits", null, AddTraits));
            Unknowns.Items.Add(new ToolStripMenuItem("Ignore all", null, ToggleIgnoreAll));

            Ignored.Items.Add(new ToolStripMenuItem("Import all", null, ToggleIgnoreAll));
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
            var nodes = TreeView.SelectedNode.Nodes.OfType<ColumnNode>();
            foreach (var node in nodes)
                if (!node.Validator.Valid)
                    await node.AddTrait();

            Validate();
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private void ToggleIgnoreAll(object sender, EventArgs args)
        {
            var nodes = TreeView.SelectedNode.Nodes.OfType<ColumnNode>();
            foreach (var node in nodes)
                node.ToggleIgnore();

            Validate();
        }

        private void UpdateColumn(ColumnNode node)
        {
            node.Parent.Nodes.Remove(node);

            if (node.Ignore)
                Ignored.Nodes.Add(node);
            else if (node.Excel.State["IsTrait"] is true)
                Traits.Nodes.Add(node);
            else if (node.Excel.State["Info"] is not null)
                Properties.Nodes.Add(node);
            else
                Unknowns.Nodes.Add(node);
        }

        public override void Validate()
        {
            Validator.Valid = true;

            var nodes = Nodes.OfType<GroupNode>().SelectMany(g => g.Nodes.OfType<ColumnNode>()).ToArray();
            
            foreach (var node in nodes)
            {
                node.Validate();
                UpdateColumn(node);

                if (!node.Ignore)
                    Validator.Valid &= node.Validator.Valid;
            }

            Validator.Validate();

            CheckNode(Properties);
            CheckNode(Traits);
            CheckNode(Unknowns);
            CheckNode(Ignored);

            ImageKey = SelectedImageKey = Key;
        }

        private void CheckNode(GroupNode node)
        {
            if (node.Nodes.Count == 0)
                Nodes.Remove(node);
            else if (!Nodes.Contains(node))
                Nodes.Add(node);
        }
    }
}
