using System;
using System.Data;
using System.Drawing;

namespace WindowsClient.Models
{
    /// <summary>
    /// A simple implementation of <see cref="INodeValidator"/> for generic columns of excel data
    /// </summary>
    public class ColumnValidator : BaseValidator<DataColumn>
    {
        public ColumnValidator(DataColumn column)
        {
            Component = column;
        }

        /// <inheritdoc/>
        public override void Validate()
        {
            bool isProperty = Component.ExtendedProperties["Info"] is null;
            bool isTrait = Component.ExtendedProperties["IsTrait"] is true;

            // If the colum node is not valid for import, update the state to warn the user
            if (isProperty && !isTrait)
            {
                Valid = false;

                var advice = new Advice();
                advice.Include("The type of column could not be determined. ", Color.Black);
                advice.Include("Right click to view options. \n\n", Color.Black);
                advice.Include("Ignore\n", Color.Blue);
                advice.Include("    - The column is not imported\n\n", Color.Black);
                advice.Include("Add trait\n", Color.Blue);
                advice.Include("    - Add a trait named for the column\n", Color.Black);
                advice.Include("    - Only valid traits are imported\n\n", Color.Black);
                advice.Include("Set property\n", Color.Blue);
                advice.Include("    - Match the column to a REMS property\n", Color.Black);

                InvokeSetAdvice(advice);
            }
            else
            {
                Valid = true;

                var advice = new Advice();
                advice.Include("Ready to be imported.\n", Color.Black);

                InvokeSetAdvice(advice);
            }
        }
    }

    /// <summary>
    /// Implements <see cref="INodeValidator"/> for ordinal columns
    /// </summary>
    public class OrdinalValidator : BaseValidator<DataColumn>
    {
        readonly int ordinal;
        readonly string name;

        public OrdinalValidator(DataColumn column, int o, string n)
        {
            Component = column;
            ordinal = o;
            name = n;
        }

        /// <inheritdoc/>
        public override void Validate()
        {
            Valid = Component.Ordinal == ordinal && Component.ColumnName == name;
        }
    }
}
