using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class DataNode : TreeNode
    {
        public QueryHandler Query;

        public PropertyCollection State { get; }

        public DataNode(DataColumn col) : base(col.ColumnName)
        {
            Tag = col;
            State = col.ExtendedProperties;
           
            ContextMenu = new ColumnNodeMenu(this, col);

            State["Ignore"] = false;
        }

        public DataNode(DataTable table) : base(table.TableName)
        {
            Tag = table;
            State = table.ExtendedProperties;
            ContextMenu = new TableNodeMenu(this, table);

            State["Valid"] = true;
            State["Ignore"] = false;
        }

        public void UpdateState(string state, object value)
        {
            State[state] = value;

            string key = "";

            if (State["Valid"] is true)
                key += "Valid";
            else
                key += "Invalid";

            if (State["Override"] is string s && s != "")
                key = s;

            if (State["Ignore"] is true)
                key += "Off";
            else
                key += "On";

            ImageKey = key;
            SelectedImageKey = key;

            // Update the node parent
            CheckState(Parent as DataNode);
        }

        public void ToggleIgnore(object sender, EventArgs args)
        {
            var item = sender as MenuItem;
            item.Checked = !item.Checked;
            UpdateState("Ignore", item.Checked);
        }

        public async void AddTrait(object sender, EventArgs args)
        {
            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            var result = await Query(new AddTraitCommand() { Name = name, Type = type.Name });

            if ((bool)result)
                UpdateState("Valid", true);
            else
                MessageBox.Show("The trait could not be added");
        }

        private void CheckState(DataNode node)
        {
            if (node?.Tag is DataTable table)
            {
                var cols = table.Columns.Cast<DataColumn>();
                if (cols.Any(c => c.ExtendedProperties["Valid"] is false && c.ExtendedProperties["Ignore"] is false))
                    node.UpdateState("Override", "Warning");
                else
                    node.UpdateState("Override", "");
            }
        }
    }

    public class ColumnNodeMenu : ContextMenu
    {
        DataNode node;
        DataColumn column;

        MenuItem ignore;
        MenuItem trait;
        MenuItem items;

        public ColumnNodeMenu(DataNode node, DataColumn column) : base()
        {
            this.column = column;
            this.node = node;

            ignore = new MenuItem("Ignore", node.ToggleIgnore);
            trait = new MenuItem("Add trait", node.AddTrait);
            items = new MenuItem("Set property");
            
            MenuItems.Add(ignore);
            MenuItems.Add(trait);
            MenuItems.Add(items);

            Popup += ItemsPopup;
        }        

        private void ItemsPopup(object sender, EventArgs e)
        {
            items.MenuItems.Clear();

            var props = column.GetUnmappedProperties();

            foreach (var prop in props)
                items.MenuItems.Add(prop.Name, SetProperty);
        }


        private void SetProperty(object sender, EventArgs args)
        {
            var item = sender as MenuItem;
            column.ColumnName = item.Text;
            node.State["Info"] = column.FindProperty();
            node.Text = item.Text;
            node.UpdateState("Valid", true);
        }
    }

    public class TableNodeMenu : ContextMenu
    {
        DataNode node;
        DataTable table;

        MenuItem ignore;

        public TableNodeMenu(DataNode node, DataTable table) : base()
        {
            this.node = node;
            this.table = table;

            ignore = new MenuItem("Ignore", node.ToggleIgnore);

            MenuItems.Add(ignore);
        }
    }

}
