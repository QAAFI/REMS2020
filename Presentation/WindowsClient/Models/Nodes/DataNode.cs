using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;

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
            
            validator.SetAdvice += (s, e) => Advice = e.Item;
            Validator = validator;

            items.Add(new ToolStripMenuItem("Rename", null, Rename));
            items.Add(new ToolStripMenuItem("Ignore", null, ToggleIgnore));
        }

        #region Menu functions       

        /// <summary>
        /// Begins editing the node label
        /// </summary>
        protected void Rename(object sender, EventArgs args)
        {
            BeginEdit();
            InvokeUpdated();
        }

        /// <summary>
        /// Toggles the ignored state of the current node
        /// </summary>
        public void ToggleIgnore(object sender, EventArgs args)
        {
            Excel.Source.ExtendedProperties["Ignore"] = Ignore = !Ignore;

            if (sender is not ToolStripMenuItem item)
                return;

            if (item.Checked = Ignore)
            {
                Advice.Clear();
                Advice.Include("Ignored items will not be imported.\n", Color.Black);
            }

            InvokeUpdated();
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
        public async Task AddTrait(object sender, EventArgs args)
        {
            if (Ignore)
                return;

            if (Tag is DataTable)
                throw new Exception("A table cannot be added as a trait");

            var name = (Tag as DataColumn).ColumnName;
            var type = (Tag as DataColumn).Table.ExtendedProperties["Type"] as Type;
            await QueryManager.Request(new AddTraitCommand() { Name = name, Type = type.Name });

            Validator.Validate();
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
