using System;
using System.Data;
using System.Drawing;
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
        Task<ColumnNode> CreateColumnNode(int i);
    }   

    /// <summary>
    /// A simple implementation of <see cref="ITableValidator"/>, for generic excel table data
    /// </summary>
    public class TableValidator : BaseValidator<DataTable>, ITableValidator
    {
        public bool ValidChildren { get; set; } = false;

        public TableValidator(DataTable table)
        {
            Component = table;
        }

        /// <inheritdoc/>
        public override void Validate()
        {
            // A table is valid if all of its columns are valid or ignored
            if (Valid)
                Advice = new ("This table is valid. Check the other tables prior to import.");
            else
                Advice = new ("This table contains unknown columns. Right-click on a column to " +
                    "see options for import validation.");            
        }

        /// <inheritdoc/>
        public async Task<ColumnNode> CreateColumnNode(int i)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);
            await excel.CheckIfTrait();
            var validater = new ColumnValidator(col);           

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
        public override void Validate()
        {
            if (Valid)
                Advice = new ("Ready for import.");
            else
            {
                Advice = new ("Mismatch in expected node order. \n\n" +
                    $"{"EXPECTED:",-20}{"DETECTED:",-20}\n");

                for (int i = 0; i < columns.Length; i++)
                {
                    Color color;

                    string name = Component.Columns[i].ColumnName;
                    string need = columns[i];

                    if (name == need)
                        color = SystemColors.HotTrack;
                    else
                        color = Color.MediumVioletRed;

                    Advice.Include($"{columns[i],-20}{name,-20}\n", color);
                }

                Advice.Include("\nRight-click nodes to see options.", Color.Black);
            }
        }

        /// <inheritdoc/>
        public async Task<ColumnNode> CreateColumnNode(int i)
        {
            var col = Component.Columns[i];
            var excel = new ExcelColumn(col);
            await excel.CheckIfTrait();

            INodeValidator validater = (i < columns.Length)
                ? new OrdinalValidator(col, i, columns[i])
                : new NullValidator();

            return new ColumnNode(excel, validater);
        }
    }
}
