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
            Advice = new Advice();

            bool isProperty = Component.ExtendedProperties["Info"] is not null;
            bool isTrait = Component.ExtendedProperties["IsTrait"] is true;
            
            if (Valid = isProperty || isTrait)
                Advice.Include("Ready to be imported.\n", Color.Black);
            else
            {
                Advice.Include("The type of node could not be determined. ", Color.Black);
                Advice.Include("Right click to view options. \n\n", Color.Black);
                Advice.Include("Ignore\n", Color.Blue);
                Advice.Include("    - The node data is not imported\n\n", Color.Black);
                Advice.Include("Add trait\n", Color.Blue);
                Advice.Include("    - Add a trait named for the node\n", Color.Black);
                Advice.Include("    - Only valid traits are imported\n\n", Color.Black);
                Advice.Include("Set property\n", Color.Blue);
                Advice.Include("    - Match the node to a REMS property\n", Color.Black);
                Advice.Include("    - See available properties through right-click\n", Color.Black);
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
