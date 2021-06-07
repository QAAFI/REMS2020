using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class ColumnNode : DataNode<DataColumn, INodeValidator>
    {
        private ToolStripMenuItem addtrait;
        private ToolStripMenuItem properties = new ToolStripMenuItem("Set property");
        private ToolStripMenuItem moveUp;
        private ToolStripMenuItem moveDown;

        public override string Key
        {
            get
            {
                string key = Validator.Valid ? "Valid" : "Invalid";
                key += Ignore ? "Off" : "On";

                return key;
            }
        }

        public ColumnNode(ExcelColumn excel, INodeValidator validator) : base(excel, validator)
        {
            addtrait = new ToolStripMenuItem("Add as trait", null, AddTraitClicked);
            moveUp = new ToolStripMenuItem("Move up", null, MoveUp);
            moveDown = new ToolStripMenuItem("Move down", null, MoveDown);

            Items.Add(addtrait);
            Items.Add(properties);
            Items.Add("-");
            Items.Add(moveUp);
            Items.Add(moveDown);
        }

        #region Menu functions
        /// <inheritdoc/>
        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            if (Excel.State["Info"] != null)
            {
                Items[2].Enabled = false;
                return;
            }

            properties.DropDownItems.Clear();

            var props = Excel.Data.GetUnmappedProperties();

            foreach (var prop in props)
                properties.DropDownItems.Add(prop.Name, null, (s, e) => SetProperty(prop));
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
        public async void AddTraitClicked(object sender, EventArgs args)
        {
            await AddTrait();
            InvokeUpdated();
        }

        public async Task AddTrait()
        {
            if (Ignore)
                return;

            var query = new AddTraitCommand
            {
                Name = Excel.Data.ColumnName,
                Type = (Excel.Source.ExtendedProperties["Type"] as Type).Name
            };

            await QueryManager.Request(query);

            await (Excel as ExcelColumn).CheckIfTrait();
        }

        private void SetProperty(PropertyInfo info)
        {
            Text = info.Name;
            Excel.State["Info"] = info;

            InvokeUpdated();
        }

        /// <summary>
        /// Switches this node with the sibling immediately above it in the tree
        /// </summary>
        public void MoveUp(object sender, EventArgs args)
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

            ((ColumnNode)p.Nodes[i]).Validate();
            Validate();

            InvokeUpdated();
        }

        /// <summary>
        /// Switches this node with the sibling immediately below it in the tree
        /// </summary>
        public void MoveDown(object sender, EventArgs args)
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

            ((ColumnNode)p.Nodes[i]).Validate();
            Validate();

            InvokeUpdated();
        }
        #endregion        

        public override void Validate()
        {
            Validator.Validate();

            ImageKey = SelectedImageKey = Key;
        }
    }
}
