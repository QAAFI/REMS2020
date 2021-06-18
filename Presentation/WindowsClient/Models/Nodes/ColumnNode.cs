using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class ColumnNode : DataNode<ExcelColumn, DataColumn>
    {
        private ToolStripMenuItem addtrait;
        private ToolStripMenuItem properties = new ToolStripMenuItem("Set property");

        public override string Key
        {
            get
            {
                string key = Excel.Valid ? "Valid" : "Invalid";
                key += Excel.Ignore ? "Off" : "On";

                return key;
            }
        }

        public ColumnNode(ExcelColumn excel) : base(excel)
        {
            addtrait = new ToolStripMenuItem("Add as trait", null, AddTraitClicked);

            Items.Add(addtrait);
            Items.Add(properties);
        }

        #region Menu functions
        /// <inheritdoc/>
        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            if (Excel.Info != null)
            {
                Items[2].Enabled = false;
                return;
            }

            properties.DropDownItems.Clear();

            if (Root is not TableNode table)
                return;

            foreach (ColumnNode node in table.Traits.Nodes)
                properties.DropDownItems.Add(node.Text, null, (s, e) => SetColumn(node));                
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
            if (Excel.Ignore)
                return;

            var query = new AddTraitCommand
            {
                Name = Excel.Data.ColumnName,
                Type = (Excel.Source.ExtendedProperties["Type"] as Type).Name
            };

            await QueryManager.Request(query);

            await Excel.CheckIfTrait();
        }

        private void SetColumn(ColumnNode node)
        {
            Excel = node.Excel;
            Text = node.Text;
            node.Parent.Nodes.Remove(node);
            Excel.Valid = true;
            Refresh();
        }
        #endregion        

        public override void Refresh()
        {
            ImageKey = SelectedImageKey = Key;
        }
    }
}
