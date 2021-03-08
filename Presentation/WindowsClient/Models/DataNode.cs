﻿using System;
using System.Collections.Generic;
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
    public class DataNode : TreeNode
    {
        /// <summary>
        /// Occurs when some change is applied to the node
        /// </summary>
        public event Action Updated;

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
        public INodeValidater Validater { get; set; }

        /// <summary>
        /// Manages an instance of excel data
        /// </summary>
        public IExcelData Excel { get; }

        /// <summary>
        /// The contents of the popup context menu when the node is right-clicked
        /// </summary>
        Menu.MenuItemCollection items => ContextMenu.MenuItems;

        public DataNode(IExcelData excel, INodeValidater validater) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Data;
            excel.StateChanged += UpdateState;

            validater.StateChanged += UpdateState;
            validater.SetAdvice += a => Advice = a;
            Validater = validater;

            ContextMenu = new ContextMenu();

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
            if (Parent is DataNode parent) 
                parent.Validater.Validate();
        }

        #endregion region

        #region Menu functions

        /// <summary>
        /// Occurs when the popup activates
        /// </summary>
        private void OnPopup(object sender, EventArgs args) => Excel.ConfigureMenu(items.Cast<MenuItem>().ToArray());        

        /// <summary>
        /// Begins editing the node label
        /// </summary>
        private void Rename(object sender, EventArgs args) => BeginEdit();
        
        /// <summary>
        /// Adds a trait to the database for every invalid child node
        /// </summary>
        public void AddTraits(object sender, EventArgs args)
        {
            foreach (DataNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.AddTrait(sender, args);
        }

        /// <summary>
        /// Sets the ignored state of all child nodes
        /// </summary>
        private void IgnoreAll(object sender, EventArgs args)
        {
            foreach (DataNode n in Nodes)
                if (n.Excel.State["Valid"] is false)
                    n.ToggleIgnore(null, args);
        }

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public void ToggleIgnore(object sender, EventArgs args)
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
                Validate();
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
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

            ((DataNode)p.Nodes[i]).Validate();
            Validate();

            Updated?.Invoke();
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

            //if (p is DataNode parent)
            //    parent.Validate();

            ((DataNode)p.Nodes[i]).Validate();
            Validate();

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
