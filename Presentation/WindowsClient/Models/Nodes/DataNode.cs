using System;
using System.Drawing;
using System.Windows.Forms;
using Rems.Infrastructure;

namespace WindowsClient.Models
{
    /// <summary>
    /// Represents excel data in a <see cref="TreeView"/>
    /// </summary>
    public abstract class DataNode<TExcel, TData> : ImportNode
        where TExcel : ExcelData<TData>
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
        }

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public void ToggleIgnore()
        {
            Excel.Ignore = !Excel.Ignore;

            Advice.Clear();
            Advice.Include("Ignored items will not be imported.\n", Color.Black);

            Root.Refresh();
        }

        #region Disposable

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Excel.Dispose();                    
                }
                base.Dispose();
            }
        }
        #endregion
    }
}
