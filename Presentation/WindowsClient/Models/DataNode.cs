using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Rems.Application.Common.Extensions;

namespace WindowsClient.Models
{
    public class DataNode : TreeNode
    {
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
            ContextMenu = new TableNodeMenu();

            State["Valid"] = true;
            State["Ignore"] = false;
        }

        public void SetState()
        {
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

        private void CheckState(DataNode node)
        {
            if (node?.Tag is DataTable table)
            {
                var cols = table.Columns.Cast<DataColumn>();
                if (cols.Any(c => c.ExtendedProperties["Valid"] is false && c.ExtendedProperties["Ignore"] is false))
                    table.ExtendedProperties["Override"] = "Warning";
                else
                    table.ExtendedProperties["Override"] = "";

                node.SetState();
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

            ignore = new MenuItem("Ignore", ToggleIgnore);
            trait = new MenuItem("Add trait", AddTrait);
            items = new MenuItem("Set property");
            
            MenuItems.Add(ignore);
            MenuItems.Add(trait);
            MenuItems.Add(items);

            Popup += ItemsPopup;
        }        

        private void ToggleIgnore(object sender, EventArgs args)
        {
            ignore.Checked = !ignore.Checked;
            column.ExtendedProperties["Ignore"] = ignore.Checked;

            node.SetState();
        }

        private void AddTrait(object sender, EventArgs args)
        {

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
            node.State["Valid"] = true;
            node.Text = item.Text;
            node.SetState();
        }
    }

    public class TableNodeMenu : ContextMenu
    {
        public TableNodeMenu() : base()
        {
            var ignore = new MenuItem("Ignore");

            MenuItems.Add(ignore);
        }
    }

}
