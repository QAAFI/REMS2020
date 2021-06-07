﻿using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    /// <summary>
    /// Represents excel data in a <see cref="TreeView"/>
    /// </summary>
    public abstract class DataNode<TData> : ImportNode
        where TData : IDisposable
    {
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
        protected ToolStripItemCollection items => ContextMenuStrip.Items;

        public DataNode(IExcelData<TData> excel, INodeValidator validater) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Data;
            
            validater.StateChanged += UpdateState;
            validater.SetAdvice += (s, e) => Advice = e.Item;
            Validater = validater;
            
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Opening += OnMenuOpening;

            items.Add(new ToolStripMenuItem("Rename", null, Rename));
            items.Add(new ToolStripMenuItem("Ignore", null, async (s, e) => await ToggleIgnore(s, e)));
        }

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
        public void UpdateState(object sender, Args<string, object> args)
        {
            string state = args.Item1;
            object value = args.Item2;
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

        #region Menu functions       

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public async Task ToggleIgnore(object sender, EventArgs args)
        {
            var state = new Args<string, object> { Item1 = "Ignore", Item2 = !(bool)Excel.State["Ignore"] };
            UpdateState(this, state);

            if (!(sender is ToolStripMenuItem item))
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
            if (Excel.State["Ignore"] is true)
                return;

            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            await QueryManager.Request(new AddTraitCommand() { Name = name, Type = type.Name });

            UpdateState(this, new Args<string, object> { Item1 = "Valid", Item2 = true });
        }

        #endregion

        #region Validation 
        /// <summary>
        /// Recursively confirm a node and its children as valid
        /// </summary>
        public void ForceValidate()
        {
            UpdateState(this, new Args<string, object> { Item1 = "Valid", Item2 = true });

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

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Excel.Dispose();
                    Validater.Dispose();
                    ContextMenuStrip.Opening -= OnMenuOpening;
                }
                base.Dispose();
            }
        }
        #endregion
    }
}
