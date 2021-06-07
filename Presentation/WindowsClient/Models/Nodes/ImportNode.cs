using System;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public abstract class ImportNode<TValidator> : TreeNode, IDisposable
        where TValidator : INodeValidator
    {
        /// <summary>
        /// Occurs when some change is applied to the node
        /// </summary>
        public event EventHandler Updated;

        protected void InvokeUpdated() => Updated?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// The contents of the popup context menu when the node is right-clicked
        /// </summary>
        protected ToolStripItemCollection items => ContextMenuStrip.Items;

        /// <summary>
        /// Used to validate the data prior to import
        /// </summary>
        public TValidator Validator { get; set; }

        /// <summary>
        /// The advice which is displayed alongside the node
        /// </summary>
        public Advice Advice { get; set; } = new Advice();

        public abstract string Key { get; }

        public ImportNode(string name) : base(name)
        {
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Opening += OnMenuOpening;

            items.Add(new ToolStripMenuItem("Rename", null, Rename));
        }

        /// <summary>
        /// Handles any dynamic changes to the menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected abstract void OnMenuOpening(object sender, EventArgs args);

        /// <summary>
        /// Begins editing the node label
        /// </summary>
        protected void Rename(object sender, EventArgs args) => BeginEdit();

        /// <summary>
        /// Test a node for validity
        /// </summary>
        public abstract void Validate();

    #region Disposable
    protected bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {                  
                }
                Updated = null;
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
}
