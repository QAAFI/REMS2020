using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsClient.Models
{
    public abstract class ImportNode : TreeNode, IDisposable
    {
        /// <summary>
        /// The contents of the popup context menu when the node is right-clicked
        /// </summary>
        public ToolStripItemCollection Items => ContextMenuStrip.Items;

        /// <summary>
        /// The advice which is displayed alongside the node
        /// </summary>
        public Advice Advice { get; set; } = new Advice();

        public virtual bool Valid => Nodes.OfType<ImportNode>().All(n => n.Valid); 

        public virtual string Key { get; init; }

        public ImportNode(string name) : base(name)
        {
            ContextMenuStrip = new ContextMenuStrip();                        
        }

        public ImportNode Root => (Parent as ImportNode)?.Root ?? this;       

        /// <summary>
        /// Test a node for validity
        /// </summary>
        public void Refresh()
        {
            ImageKey = SelectedImageKey = Key;

            foreach (var node in Nodes.OfType<ImportNode>())
                node.Refresh();           
        }

        #region Disposable
        protected bool disposedValue;

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {                  
                    }
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
