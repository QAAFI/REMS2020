using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;

namespace WindowsClient.Models
{
    /// <summary>
    /// Represents excel data in a <see cref="TreeView"/>
    /// </summary>
    public abstract class DataNode<TExcel, TData> : ImportNode
        where TExcel : IExcelData<TData>
        where TData : IDisposable
    {
        /// <summary>
        /// Manages an instance of excel data
        /// </summary>
        public TExcel Excel { get; set; }

        public DataNode(TExcel excel) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Data;

            Items.Add(new ToolStripMenuItem("Rename", null, Rename));
            Items.Add(new ToolStripMenuItem("Ignored", null, IgnoreClicked));
        }

        #region Menu functions       

        /// <summary>
        /// Begins editing the node label
        /// </summary>
        protected async void Rename(object sender, EventArgs args)
        {
            BeginEdit();

            await Task.Run(() => { while (IsEditing) { } });

            InvokeUpdated();
        }

        public void IgnoreClicked(object sender, EventArgs args)
        {
            ToggleIgnore();

            if (sender is ToolStripMenuItem item)            
                item.Checked = Excel.Ignore;

            InvokeUpdated();
        }

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public void ToggleIgnore()
        {
            Excel.Ignore = !Excel.Ignore;

            Advice.Clear();
            Advice.Include("Ignored items will not be imported.\n", Color.Black);            
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
                    ContextMenuStrip.Opening -= OnMenuOpening;
                }
                base.Dispose();
            }
        }
        #endregion
    }
}
