using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WindowsClient.Models
{   

    /// <summary>
    /// Extends <see cref="INodeValidator"/> to handle the creation of column nodes dependent on the table
    /// </summary>
    public interface ITableValidator : INodeValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        DataNode<DataColumn> CreateColumnNode(int i);
    }   

    /// <summary>
    /// A simple implementation of <see cref="ITableValidator"/>, for generic excel table data
    /// </summary>
    public class TableValidator : BaseValidator<DataTable>, ITableValidator
    {
        public TableValidator(DataTable table)
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
        public DataNode<DataColumn> CreateColumnNode(int i)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);
            var validater = new ColumnValidator(col);
           
            validater.SetAdvice += a => InvokeSetAdvice(a);

            return new ColumnNode(excel, validater);
        }
    }

    /// <summary>
    /// Implements <see cref="ITableValidator"/> with handling for specialised tables
    /// </summary>
    public class CustomTableValidator : BaseValidator<DataTable>, ITableValidator
    {
        public readonly string[] columns;

        public CustomTableValidator(DataTable table, string[] columns)
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
        public DataNode<DataColumn> CreateColumnNode(int i)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);

            INodeValidator validater = (i < columns.Length) ? 
                new OrdinalValidator(col, i, columns[i]) as INodeValidator : 
                new NullValidator<DataColumn>();
            
            validater.SetAdvice += a => InvokeSetAdvice(a);

            return new ColumnNode(excel, validater);
        }
    }
}
