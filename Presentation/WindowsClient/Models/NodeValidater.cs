using MediatR;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
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

    /// <summary>
    /// A default implementation of <see cref="INodeValidater"/> that always validates
    /// </summary>
    public class NullValidater : INodeValidater
    {
        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        /// <inheritdoc/>
        public async Task Validate()
        {
            StateChanged?.Invoke("Valid", true);
        }
    }

    #region Table validation

    /// <summary>
    /// A simple implementation of <see cref="ITableValidater"/>, for generic excel table data
    /// </summary>
    public class TableValidater : ITableValidater
    {
        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        readonly DataTable table;

        public TableValidater(DataTable table)
        {
            this.table = table;
        }

        /// <inheritdoc/>
        public async Task Validate()
        {
            var valid = table.Columns
                .Cast<DataColumn>()
                .Select(c => (bool)c.ExtendedProperties["Valid"] || (bool)c.ExtendedProperties["Ignore"])
                .Aggregate((v1, v2) => v1 &= v2);

            // A table is valid if all of its columns are valid or ignored
            if (valid)
            {
                StateChanged?.Invoke("Valid", true);
                StateChanged?.Invoke("Override", "");

                var advice = new Advice();
                advice.Include("This table is valid. Check the other tables prior to import.", Color.Black);

                SetAdvice?.Invoke(advice);
            }
            else
            {
                StateChanged?.Invoke("Valid", false);
                StateChanged?.Invoke("Override", "Warning");

                var advice = new Advice();
                advice.Include("This table contains columns that REMS does not recognise." +
                    " Please fix the columns before importing", Color.Black);
                SetAdvice?.Invoke(advice);
            }
        }

        /// <inheritdoc/>
        public DataNode CreateColumnNode(int i, Func<object, Task<object>> query)
        {
            var col = table.Columns[i];
            var excel = new ExcelColumn(col);
            var validater = new ColumnValidater(col);

            validater.Query += (o) => query?.Invoke(o);            
            validater.SetAdvice += a => SetAdvice?.Invoke(a);

            return new DataNode(excel, validater);
        }
    }

    /// <summary>
    /// Implements <see cref="ITableValidater"/> with handling for specialised tables
    /// </summary>
    public class CustomTableValidater : ITableValidater
    {
        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        public readonly string[] columns;

        protected readonly DataTable table;

        public CustomTableValidater(DataTable table, string[] columns)
        {
            this.table = table;
            this.columns = columns;
        }

        /// <inheritdoc/>
        public async Task Validate()
        {
            bool valid = true;

            foreach (DataColumn col in table.Columns)
                valid &= (bool)col.ExtendedProperties["Valid"];

            if (valid)
            {
                StateChanged?.Invoke("Valid", true);
                StateChanged?.Invoke("Override", "");

                var advice = new Advice();
                advice.Include("Ready for import.", Color.Black);
                SetAdvice?.Invoke(advice);
            }
            else
            {
                var advice = new Advice();
                advice.Include("Mismatch in expected node order. \n\n" +
                    $"{"EXPECTED:", -20}{"DETECTED:", -20}\n",  Color.Black);

                for (int i = 0; i < columns.Length; i++)
                {
                    Color color;

                    string name = table.Columns[i].ColumnName;
                    string need = columns[i];

                    if (name == need)
                        color = SystemColors.HotTrack;
                    else
                        color = Color.MediumVioletRed;

                    advice.Include($"{columns[i],-20}{name,-20}\n", color);
                }

                advice.Include("\nRight-click nodes to see options.", Color.Black);

                SetAdvice?.Invoke(advice);
                StateChanged?.Invoke("Valid", false);
                StateChanged?.Invoke("Override", "Warning");
            }
                
        }

        /// <inheritdoc/>
        public DataNode CreateColumnNode(int i, Func<object, Task<object>> query)
        {
            var col = table.Columns[i];
            var excel = new ExcelColumn(col);

            INodeValidater validater = (i < columns.Length) ? 
                new OrdinalValidater(col, i, columns[i]) as INodeValidater : 
                new NullValidater();
            
            validater.SetAdvice += a => SetAdvice?.Invoke(a);

            return new DataNode(excel, validater);
        }
    }

    #endregion

    #region Column validation
    /// <summary>
    /// A simple implementation of <see cref="INodeValidater"/> for generic columns of excel data
    /// </summary>
    public class ColumnValidater : INodeValidater
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        readonly DataColumn column;

        public ColumnValidater(DataColumn column)
        {
            this.column = column;
        }

        /// <inheritdoc/>
        public async Task Validate()
        {
            // If the colum node is not valid for import, update the state to warn the user
            if (column.ExtendedProperties["Info"] is null && !await IsTrait())
            {
                StateChanged?.Invoke("Valid", false);

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

                SetAdvice?.Invoke(advice);
            }
            else
            {
                StateChanged?.Invoke("Valid", true);
                StateChanged?.Invoke("Override", "");

                var advice = new Advice();
                advice.Include("Ready to be imported.\n", Color.Black);

                SetAdvice?.Invoke(advice);
            }
        }

        /// <summary>
        /// Tests if the column is referring to a known database trait
        /// </summary>
        private async Task<bool> IsTrait()
        {
            return
                    // Test if the trait is in the database
                    await InvokeQuery(new TraitExistsQuery() { Name = column.ColumnName })
                    // Or in the spreadsheet traits table
                    || column.Table.DataSet.Tables["Traits"] is DataTable traits
                    // If it is, find the column of trait names
                    && traits.Columns["Name"] is DataColumn name
                    // 
                    && traits.Rows.Cast<DataRow>().Any(r => r[name].ToString().ToLower() == column.ColumnName.ToLower());
        }
    }

    /// <summary>
    /// Implements <see cref="INodeValidater"/> for ordinal columns
    /// </summary>
    public class OrdinalValidater : INodeValidater
    {
        /// <inheritdoc/>
        public event Action<string, object> StateChanged;

        /// <inheritdoc/>
        public event Action<Advice> SetAdvice;

        readonly DataColumn col;
        readonly int ordinal;
        readonly string name;

        public OrdinalValidater(DataColumn column, int o, string n)
        {
            col = column;
            ordinal = o;
            name = n;
        }

        /// <inheritdoc/>
        public async Task Validate()
        {
            if (col.Ordinal == ordinal && col.ColumnName == name)
            {
                StateChanged?.Invoke("Valid", true);
                StateChanged?.Invoke("Override", "");
            }
            else
            {
                StateChanged?.Invoke("Valid", false);
                StateChanged?.Invoke("Override", "Warning");
            }
        }
    }

    #endregion
}
