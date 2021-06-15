using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    /// <summary>
    /// Represents excel data in a <see cref="TreeView"/>
    /// </summary>
    public abstract class DataNode<TData, TValidator> : ImportNode<TValidator>
        where TData : IDisposable
        where TValidator : INodeValidator
    {
        /// <summary>
        /// Manages an instance of excel data
        /// </summary>
        public IExcelData<TData> Excel { get; }

        public bool Ignore { get; set; } = false;

        public DataNode(IExcelData<TData> excel, TValidator validator) : base(excel.Name)
        {
            Excel = excel;
            Tag = Excel.Data;
            
            Validator = validator;

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
                item.Checked = Ignore;

            InvokeUpdated();
        }

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public void ToggleIgnore()
        {
            Excel.State["Ignore"] = Ignore = !Ignore;

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
                    Validator.Dispose();
                    ContextMenuStrip.Opening -= OnMenuOpening;
                }
                base.Dispose();
            }
        }
        #endregion
    }
}
