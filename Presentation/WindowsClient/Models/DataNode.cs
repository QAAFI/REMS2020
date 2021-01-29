using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.Common;
using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class DataNode : TreeNode
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public PropertyCollection State { get; }

        public DataTable Source { get; }

        public IEnumerable<RichText> Advice { get; set; }

        private List<RichText> valid;
        private List<RichText> invalid;

        private DataNode(string name) : base(name)
        {
            
        }

        public DataNode(DataColumn col) : this(col.ColumnName)
        {
            Tag = col;
            State = col.ExtendedProperties;
            Source = col.Table;

            State["Ignore"] = false;

            ContextMenu = new ColumnNodeMenu(this, col);            

            valid = new List<RichText>
            { 
                new RichText
                { Text = "This column is valid and can be imported", Color = Color.Black }
            };

            invalid = new List<RichText>
            {
                new RichText
                { Text = "The type of column could not be determined. ", Color = Color.Black },
                new RichText
                { Text = "Right click to view options. \n\n", Color = Color.Black },
                new RichText
                { Text = "Ignore\n", Color = Color.Blue },
                new RichText
                { Text = "    - The column is not imported\n\n", Color = Color.Black },                
                new RichText
                { Text = "Add trait\n", Color = Color.Blue },
                new RichText
                { Text = "    - Add a trait named for the column\n", Color = Color.Black },
                new RichText
                { Text = "    - Only valid traits are imported\n\n", Color = Color.Black },
                new RichText
                { Text = "Set property\n", Color = Color.Blue },
                new RichText
                { Text = "    - Match the column to a REMS property\n", Color = Color.Black }
            };    
        }

        public DataNode(DataTable table) : this(table.TableName)
        {
            Tag = table;
            State = table.ExtendedProperties;
            Source = table;
            ContextMenu = new TableNodeMenu(this, table);

            State["Ignore"] = false;
            State["Valid"] = true;

            valid = new List<RichText>
            {
                new RichText
                { Text = "This table is valid. Check the other tables prior to import.", Color = Color.Black }
            };

            invalid = new List<RichText>
            {
                new RichText
                { Text = "This table contains columns that REMS does not recognise. " +
                "Please fix the columns before importing", Color = Color.Black }
            };
        }

        public void UpdateState(string state, object value)
        {
            State[state] = value;

            string key = "";

            if (State["Override"] is string s && s != "")
            {
                key = s;
            }
            else if (State["Valid"] is true)
            {
                key += "Valid";
                Advice = valid;
            }
            else
            {
                key += "Invalid";
                Advice = invalid;
            }            

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
            UpdateState("Ignore", !(bool)State["Ignore"]);
            
            if (sender is MenuItem item)
                item.Checked = (bool)State["Ignore"];
        }

        public async void AddTrait(object sender, EventArgs args)
        {
            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            var result = await InvokeQuery(new AddTraitCommand() { Name = name, Type = type.Name });

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

        /// <summary>
        /// Recursively validate a node and all its children
        /// </summary>
        public void ValidateAll()
        {
            UpdateState("Valid", true);

            foreach (DataNode node in Nodes)
                node.ValidateAll();
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
            if (node.State["Info"] != null)
            {
                trait.Enabled = false;
                return;
            }

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
        MenuItem addTraits;
        MenuItem ignoreAll;

        public TableNodeMenu(DataNode node, DataTable table) : base()
        {
            this.node = node;
            this.table = table;

            ignore = new MenuItem("Ignore", node.ToggleIgnore);
            addTraits = new MenuItem("Add invalids as traits", AddTraits);
            ignoreAll = new MenuItem("Ignore all invalids", IgnoreAll);

            MenuItems.Add(ignore);
            MenuItems.Add(addTraits);
            MenuItems.Add(ignoreAll);
        }

        public void AddTraits(object sender, EventArgs args)
        {
            foreach (DataNode n in node.Nodes)
                if (n.State["Valid"] is false)
                    n.AddTrait(sender, args);
        }

        private void IgnoreAll(object sender, EventArgs args)
        {
            foreach (DataNode n in node.Nodes)
                if (n.State["Valid"] is false)
                    n.ToggleIgnore(null, args);
        }
    }

}
