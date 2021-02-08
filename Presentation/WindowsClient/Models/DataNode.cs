using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.CQRS;
using static System.Windows.Forms.Menu;

namespace WindowsClient.Models
{
    public class DataNode : TreeNode
    {
        public event Action Updated;

        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public INodeValidater Validater { get; set; }

        public IExcelData Excel { get; }

        MenuItemCollection items => ContextMenu.MenuItems;

        public DataNode(IExcelData excel, INodeValidater validater) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Tag;
            excel.StateChanged += UpdateState;

            validater.StateChanged += UpdateState;
            Validater = validater;

            ContextMenu = new ContextMenu(excel.Items.ToArray());

            items.Add(new MenuItem("Rename", Rename));
            items.Add(new MenuItem("Ignore", ToggleIgnore));

            if (excel is ExcelColumn)
            {
                ContextMenu.Popup += OnPopup;
                
                items.Add(new MenuItem("Add as trait", AddTrait));
                items.Add(new MenuItem("Set property"));
                items.Add("-");
                items.Add(new MenuItem("Move up", MoveUp));
                items.Add(new MenuItem("Move down", MoveDown));
            }
            else if (excel is ExcelTable)
            {
                items.Add(new MenuItem("Add invalids as traits", AddTraits));
                items.Add(new MenuItem("Ignore all invalids", IgnoreAll));
            }
        }

        #region State functions

        private void OnStateChanged(string state, object value)
        {
            Text = Excel.Name;
            UpdateState(state, value);
        }

        public void UpdateState(string state, object value)
        {
            Excel.State[state] = value;

            string key = "";

            if (Excel.State["Override"] is string s && s != "")
                key = s;
            else if (Excel.State["Valid"] is true)
                key += "Valid";
            else
                key += "Invalid";

            if (Excel.State["Ignore"] is true)
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
                    node.UpdateState("Override", "Warning");
                else
                    node.UpdateState("Override", "");
            }
        }

        #endregion region

        #region Menu functions

        private void OnPopup(object sender, EventArgs e)
        {
            Excel.StateChanged += OnStateChanged;
            Excel.SetMenu(items.Cast<MenuItem>().ToArray());
        }

        private void Rename(object sender, EventArgs args) => BeginEdit();
        
        public void AddTraits(object sender, EventArgs args)
        {
            foreach (DataNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.AddTrait(sender, args);
        }

        private void IgnoreAll(object sender, EventArgs args)
        {
            foreach (DataNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.ToggleIgnore(null, args);
        }

        public void ToggleIgnore(object sender, EventArgs args)
        {            
            UpdateState("Ignore", !(bool)Excel.State["Ignore"]);
            
            if (sender is MenuItem item)
                item.Checked = (bool)Excel.State["Ignore"];
        }

        public async void AddTrait(object sender, EventArgs args)
        {
            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            var result = await InvokeQuery(new AddTraitCommand() { Name = name, Type = type.Name });

            if (result)
                UpdateState("Valid", true);
            else
                MessageBox.Show("The trait could not be added");
        }

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

            Updated?.Invoke();
        }

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

            Updated?.Invoke();
        }

        #endregion

        #region Validation 

        /// <summary>
        /// Recursively confirm a node and its children as valid
        /// </summary>
        public void ForceValidate()
        {
            UpdateState("Valid", true);

            foreach (DataNode node in Nodes)
                node.ForceValidate();
        }

        /// <summary>
        /// Recursively test a node and its children for validity
        /// </summary>
        public void Validate()
        {
            foreach (DataNode node in Nodes)
                node.Validate();

            Validater.Validate();
        }

        #endregion
    }

}
