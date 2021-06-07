using System;
using System.Data;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;

namespace WindowsClient.Models
{
    public class ColumnNode : DataNode<DataColumn>
    {
        private ToolStripMenuItem addtrait;
        private ToolStripMenuItem properties = new ToolStripMenuItem("Set property");
        private ToolStripMenuItem moveUp;
        private ToolStripMenuItem moveDown;

        public ColumnNode(ExcelColumn excel, INodeValidator validator) : base(excel, validator)
        {
            addtrait = new ToolStripMenuItem("Add as trait", null, async (s, e) => await AddTrait(s, e));
            moveUp = new ToolStripMenuItem("Move up", null, MoveUp);
            moveDown = new ToolStripMenuItem("Move down", null, MoveDown);

            items.Add(addtrait);
            items.Add(properties);
            items.Add("-");
            items.Add(moveUp);
            items.Add(moveDown);
        }

        /// <inheritdoc/>
        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            if (Excel.State["Info"] != null)
            {
                items[2].Enabled = false;
                return;
            }

            properties.DropDownItems.Clear();

            var props = Excel.Data.GetUnmappedProperties();

            foreach (var prop in props)
                properties.DropDownItems.Add(prop.Name, null, SetProperty);
        }

        private void SetProperty(object sender, EventArgs args)
        {
            var item = sender as ToolStripMenuItem;
            Name = item.Text;
            Excel.State["Info"] = Excel.Data.FindProperty();
            UpdateState(this, new Args<string, object> { Item1 = "Valid", Item2 = true });
        }

        /// <summary>
        /// Switches this node with the sibling immediately above it in the tree
        /// </summary>
        public async void MoveUp(object sender, EventArgs args)
        {
            // Store references so they are not lost on removal
            int i = Index;
            var p = Parent;

            if (i > 0)
            {
                p.Nodes.Remove(this);
                p.Nodes.Insert(i - 1, this);
                TreeView.SelectedNode = this;
                Excel.Swap(i - 1);
            }

            await ((DataNode<DataColumn>)p.Nodes[i]).Validate();
            await Validate();

            InvokeUpdated();
        }

        /// <summary>
        /// Switches this node with the sibling immediately below it in the tree
        /// </summary>
        public async void MoveDown(object sender, EventArgs args)
        {
            // Store references so they are not lost on removal
            int i = Index;
            var p = Parent;

            if (i + 1 < p.Nodes.Count)
            {
                p.Nodes.Remove(this);
                p.Nodes.Insert(i + 1, this);
                TreeView.SelectedNode = this;
                Excel.Swap(i + 1);
            }

            await ((DataNode<DataColumn>)p.Nodes[i]).Validate();
            await Validate();

            InvokeUpdated();
        }
    }
}
