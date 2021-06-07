using System;
using System.Data;
using System.Linq;

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
                if (Excel.State["Ignore"] is true)
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
        }

        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            // No configuration required
        }        

        public override void Validate()
        {
            Validator.Valid = true;

            var nodes = Nodes.OfType<GroupNode>().SelectMany(g => g.Nodes.OfType<ColumnNode>());
            
            foreach (var node in nodes)
            {
                node.Validate();

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
