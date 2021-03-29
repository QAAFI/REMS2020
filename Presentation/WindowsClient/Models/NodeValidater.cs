using MediatR;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    /// <summary>
    /// Manages the validation of a node containing excel data
    /// </summary>
    public interface INodeValidater
    {
        /// <summary>
        /// Occurs when the data is modified
        /// </summary>
        event Action<string, object> StateChanged;

        /// <summary>
        /// Occurs when the advice provided to the user is changed
        /// </summary>
        event Action<Advice> SetAdvice;

        /// <summary>
        /// Checks if the node is ready to be imported and updates the state accordingly
        /// </summary>
        Task Validate();
    }

    /// <summary>
    /// Extends <see cref="INodeValidater"/> to handle the creation of column nodes dependent on the table
    /// </summary>
    public interface ITableValidater : INodeValidater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        DataNode CreateColumnNode(int i, Func<object, Task<object>> query);
    }

    public abstract class BaseValidater<TDisposable> : INodeValidater, IDisposable
        where TDisposable : IDisposable
    {
        private bool disposedValue;

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        public TDisposable Component { get; set; }

        protected void InvokeStateChanged(string state, object value)
            => StateChanged?.Invoke(state, value);

        protected void InvokeSetAdvice(Advice advice)
            => SetAdvice?.Invoke(advice);

        public abstract Task Validate();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Component.Dispose();
                }

                StateChanged = null;
                SetAdvice = null;

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// A default implementation of <see cref="INodeValidater"/> that always validates
    /// </summary>
    public class NullValidater<TComponent> : BaseValidater<TComponent>
        where TComponent : MarshalByValueComponent
    {
        /// <inheritdoc/>
        public async override Task Validate()
        {
            InvokeStateChanged("Valid", true);
        }
    }

    #region Table validation

    /// <summary>
    /// A simple implementation of <see cref="ITableValidater"/>, for generic excel table data
    /// </summary>
    public class TableValidater : BaseValidater<DataTable>, ITableValidater
    {
        public TableValidater(DataTable table)
        {
            Component = table;
        }

        /// <inheritdoc/>
        public async override Task Validate()
        {
            var valid = Component.Columns
                .Cast<DataColumn>()
                .Select(c => (bool)c.ExtendedProperties["Valid"] || (bool)c.ExtendedProperties["Ignore"])
                .Aggregate((v1, v2) => v1 &= v2);
            
            // A table is valid if all of its columns are valid or ignored
            if (valid)
            {
                InvokeStateChanged("Valid", true);
                InvokeStateChanged("Override", "");

                var advice = new Advice();
                advice.Include("This table is valid. Check the other tables prior to import.", Color.Black);

                InvokeSetAdvice(advice);
            }
            else
            {
                InvokeStateChanged("Valid", false);
                InvokeStateChanged("Override", "Warning");

                var advice = new Advice();
                advice.Include("This table contains columns that REMS does not recognise." +
                    " Please fix the columns before importing", Color.Black);
                InvokeSetAdvice(advice);
            }
        }

        /// <inheritdoc/>
        public DataNode CreateColumnNode(int i, Func<object, Task<object>> query)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);
            var validater = new ColumnValidater(col);

            validater.Query += (o) => query?.Invoke(o);            
            validater.SetAdvice += a => InvokeSetAdvice(a);

            return new DataNode(excel, validater);
        }
    }

    /// <summary>
    /// Implements <see cref="ITableValidater"/> with handling for specialised tables
    /// </summary>
    public class CustomTableValidater : BaseValidater<DataTable>, ITableValidater
    {
        public readonly string[] columns;

        public CustomTableValidater(DataTable table, string[] columns)
        {
            Component = table;
            this.columns = columns;
        }

        /// <inheritdoc/>
        public async override Task Validate()
        {
            bool valid = true;

            foreach (DataColumn col in Component.Columns)
                valid &= (bool)col.ExtendedProperties["Valid"];

            if (valid)
            {
                InvokeStateChanged("Valid", true);
                InvokeStateChanged("Override", "");

                var advice = new Advice();
                advice.Include("Ready for import.", Color.Black);
                InvokeSetAdvice(advice);
            }
            else
            {
                var advice = new Advice();
                advice.Include("Mismatch in expected node order. \n\n" +
                    $"{"EXPECTED:", -20}{"DETECTED:", -20}\n",  Color.Black);

                for (int i = 0; i < columns.Length; i++)
                {
                    Color color;

                    string name = Component.Columns[i].ColumnName;
                    string need = columns[i];

                    if (name == need)
                        color = SystemColors.HotTrack;
                    else
                        color = Color.MediumVioletRed;

                    advice.Include($"{columns[i],-20}{name,-20}\n", color);
                }

                advice.Include("\nRight-click nodes to see options.", Color.Black);

                InvokeSetAdvice(advice);
                InvokeStateChanged("Valid", false);
                InvokeStateChanged("Override", "Warning");
            }
                
        }

        /// <inheritdoc/>
        public DataNode CreateColumnNode(int i, Func<object, Task<object>> query)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);

            INodeValidater validater = (i < columns.Length) ? 
                new OrdinalValidater(col, i, columns[i]) as INodeValidater : 
                new NullValidater<MarshalByValueComponent>();
            
            validater.SetAdvice += a => InvokeSetAdvice(a);

            return new DataNode(excel, validater);
        }
    }

    #endregion

    #region Column validation
    /// <summary>
    /// A simple implementation of <see cref="INodeValidater"/> for generic columns of excel data
    /// </summary>
    public class ColumnValidater : BaseValidater<DataColumn>
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public ColumnValidater(DataColumn column)
        {
            Component = column;
        }

        /// <inheritdoc/>
        public async override Task Validate()
        {
            // If the colum node is not valid for import, update the state to warn the user
            if (Component.ExtendedProperties["Info"] is null && !await IsTrait())
            {
                InvokeStateChanged("Valid", false);

                var advice = new Advice();
                advice.Include("The type of column could not be determined. ", Color.Black);
                advice.Include("Right click to view options. \n\n", Color.Black);
                advice.Include("Ignore\n", Color.Blue);
                advice.Include("    - The column is not imported\n\n", Color.Black);
                advice.Include("Add trait\n", Color.Blue);
                advice.Include("    - Add a trait named for the column\n", Color.Black );
                advice.Include("    - Only valid traits are imported\n\n", Color.Black );
                advice.Include("Set property\n", Color.Blue);
                advice.Include("    - Match the column to a REMS property\n", Color.Black );

                InvokeSetAdvice(advice);
            }
            else
            {
                InvokeStateChanged("Valid", true);
                InvokeStateChanged("Override", "");

                var advice = new Advice();
                advice.Include("Ready to be imported.\n", Color.Black);

                InvokeSetAdvice(advice);
            }
        }

        /// <summary>
        /// Tests if the column is referring to a known database trait
        /// </summary>
        private async Task<bool> IsTrait()
        {
            return
                    // Test if the trait is in the database
                    await InvokeQuery(new TraitExistsQuery() { Name = Component.ColumnName })
                    // Or in the spreadsheet traits table
                    || Component.Table.DataSet.Tables["Traits"] is DataTable traits
                    // If it is, find the column of trait names
                    && traits.Columns["Name"] is DataColumn name
                    // 
                    && traits.Rows.Cast<DataRow>().Any(r => r[name].ToString().ToLower() == Component.ColumnName.ToLower());
        }
    }

    /// <summary>
    /// Implements <see cref="INodeValidater"/> for ordinal columns
    /// </summary>
    public class OrdinalValidater : BaseValidater<DataColumn>
    {
        readonly int ordinal;
        readonly string name;

        public OrdinalValidater(DataColumn column, int o, string n)
        {
            Component = column;
            ordinal = o;
            name = n;
        }

        /// <inheritdoc/>
        public async override Task Validate()
        {
            bool valid = Component.Ordinal == ordinal && Component.ColumnName == name;
            InvokeStateChanged("Valid", valid);
            InvokeStateChanged("Override", valid ? "" : "Warning");
        }
    }

    #endregion
}
