using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    /// <summary>
    /// A simple implementation of <see cref="INodeValidator"/> for generic columns of excel data
    /// </summary>
    public class ColumnValidator : BaseValidator<DataColumn>
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public ColumnValidator(DataColumn column)
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
        public async override Task Validate()
        {
            bool valid = Component.Ordinal == ordinal && Component.ColumnName == name;
            InvokeStateChanged("Valid", valid);
            InvokeStateChanged("Override", valid ? "" : "Warning");
        }
    }
}
