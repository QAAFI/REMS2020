using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    /// <summary>
    /// Represents excel data in a <see cref="TreeView"/>
    /// </summary>
    public abstract class DataNode<TData> : TreeNode, IDisposable
        where TData : IDisposable
    {
        /// <summary>
        /// Occurs when some change is applied to the node
        /// </summary>
        public event Action Updated;

        protected void InvokeUpdated() => Updated?.Invoke();

        /// <summary>
        /// Occurs when the node requests data
        /// </summary>
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        /// <summary>
        /// The advice which is displayed alongside the node
        /// </summary>
        public Advice Advice { get; set; } = new Advice();

        /// <summary>
        /// Used to validate the data prior to import
        /// </summary>
        public INodeValidator Validater { get; set; }

        /// <summary>
        /// Manages an instance of excel data
        /// </summary>
        public IExcelData<TData> Excel { get; }

        /// <summary>
        /// The contents of the popup context menu when the node is right-clicked
        /// </summary>
        protected Menu.MenuItemCollection items => ContextMenu.MenuItems;

        public DataNode(IExcelData<TData> excel, INodeValidator validater) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Data;
            excel.StateChanged += UpdateState;

            validater.StateChanged += UpdateState;
            validater.SetAdvice += a => Advice = a;
            Validater = validater;
            
            ContextMenu = new ContextMenu();

            items.Add(new MenuItem("Rename", Rename));
            items.Add(new MenuItem("Ignore", async (s, e) => await ToggleIgnore(s, e)));
        }

        #region State functions

        /// <summary>
        /// Updates one of the nodes possible states
        /// </summary>
        /// <remarks>
        /// Available states
        /// <list type="bullet">
        ///     <item>
        ///         <term>Valid</term>
        ///         <description>Is the data ready for import?</description>
        ///         <para><description>Value type: <see cref="bool"/></description></para>
        ///     </item>
        ///     <item>
        ///         <term>Ignore</term>
        ///         <description>Is the importer ignoring the data?</description>
        ///         <para><description>Value type: <see cref="bool"/></description></para>
        ///     </item>
        ///     <item>
        ///         <term>Override</term>
        ///         <description>If given a value, forces the node into that state</description>
        ///         <para><description>Value type: <see cref="string"/></description></para>
        ///     </item>
        /// </list>
        /// </remarks>
        /// <param name="state">The name of the state to change</param>
        /// <param name="value">The value to change the state to</param>
        public void UpdateState(string state, object value)
        {
            Text = Excel.Name;

            // Prevent recursively updating states
            if (Excel.State[state] == value) return;

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
            if (Parent is DataNode<DataTable> parent) 
                parent.Validater.Validate();
        }

        #endregion region

        #region Menu functions        
        /// <summary>
        /// Begins editing the node label
        /// </summary>
        private void Rename(object sender, EventArgs args) => BeginEdit();       
        
        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public async Task ToggleIgnore(object sender, EventArgs args)
        {            
            UpdateState("Ignore", !(bool)Excel.State["Ignore"]);

            if (!(sender is MenuItem item))
                return;

            item.Checked = (bool)Excel.State["Ignore"];

            if (item.Checked)
            {
                Advice.Clear();
                Advice.Include("Ignored items will not be imported.\n", Color.Black);
            }
            else
                await Validate();
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
        public async Task AddTrait(object sender, EventArgs args)
        {
            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            await InvokeQuery(new AddTraitCommand() { Name = name, Type = type.Name });

            UpdateState("Valid", true);
        }
        #endregion

        #region Validation 
        /// <summary>
        /// Recursively confirm a node and its children as valid
        /// </summary>
        public void ForceValidate()
        {
            UpdateState("Valid", true);

            foreach (DataNode<DataColumn> node in Nodes)
                node.ForceValidate();
        }

        /// <summary>
        /// Recursively test a node and its children for validity
        /// </summary>
        public async Task Validate()
        {
            foreach (DataNode<DataColumn> node in Nodes)
                await node.Validate();

            await Validater.Validate();
        }
        #endregion

        #region Disposable
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Excel.Dispose();
                    Validater.Dispose();
                }

                Updated = null;
                Query = null;
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ColumnNode : DataNode<DataColumn>
    {
        public ColumnNode(ExcelColumn excel, INodeValidator validator) : base(excel, validator)
        {
            ContextMenu.Popup += (s, e) => excel.ConfigureMenu(items.Cast<MenuItem>().ToArray());
            
            items.Add(new MenuItem("Add as trait", async (s, e) => await AddTrait(s, e)));
            items.Add(new MenuItem("Set property"));
            items.Add("-");
            items.Add(new MenuItem("Move up", MoveUp));
            items.Add(new MenuItem("Move down", MoveDown));
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

    public class TableNode : DataNode<DataTable>
    {
        public TableNode(ExcelTable excel, ITableValidator validator) : base(excel, validator)
        {
            items.Add(new MenuItem("Add invalids as traits", AddTraits));
            items.Add(new MenuItem("Ignore all invalids", IgnoreAll));
        }

        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public async void AddTraits(object sender, EventArgs args)
        {
            foreach (DataNode<DataColumn> n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    await n.AddTrait(sender, args);
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private async void IgnoreAll(object sender, EventArgs args)
        {
            foreach (DataNode<DataColumn> n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    await n.ToggleIgnore(null, args);
        }
    }
}
