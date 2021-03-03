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
    public interface INodeValidater
    {
        event Action<string, object> StateChanged;

        event Action<Advice> SetAdvice;

        void Validate();
    }

    public class NullValidater : INodeValidater
    {
        public event Action<string, object> StateChanged;
        public event Action<Advice> SetAdvice;

        public void Validate()
        {
            StateChanged?.Invoke("Valid", true);
        }
    }

    #region Table validation

    public class TableValidater : INodeValidater
    {
        public event Action<string, object> StateChanged;
        public event Action<Advice> SetAdvice;

        readonly DataTable table;

        public TableValidater(DataTable table)
        {
            this.table = table;
        }

        public void Validate()
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
    }

    public class CustomTableValidater : INodeValidater
    {
        public event Action<Advice> SetAdvice;

        public event Action<string, object> StateChanged;

        public readonly string[] columns;

        protected readonly DataTable table;

        public CustomTableValidater(DataTable table, string[] columns)
        {
            this.table = table;
            this.columns = columns;
        }

        public void Validate()
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
    }

    #endregion

    #region Column validation

    public class ColumnValidater : INodeValidater
    {
        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public event Action<string, object> StateChanged;
        public event Action<Advice> SetAdvice;

        readonly DataColumn column;

        public ColumnValidater(DataColumn column)
        {
            this.column = column;
        }

        public async void Validate()
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

    public class OrdinalValidater : INodeValidater
    {
        public event Action<string, object> StateChanged;
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

        public void Validate()
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
