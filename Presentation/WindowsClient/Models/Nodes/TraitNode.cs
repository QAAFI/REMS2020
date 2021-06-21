using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class TraitNode : DataNode<ExcelColumn, DataColumn>
    {
        public override bool Valid => Excel.Ignore || traitExists;

        private bool traitExists;

        public override string Key
        {
            get
            {
                string key = Valid ? "Valid" : "Invalid";
                key += Excel.Ignore ? "Off" : "On";

                return key;
            }
        }

        public TraitNode(ExcelColumn excel) : base(excel)
        {
            var ignore = new ToolStripMenuItem("Ignore", null, (s, e) => ToggleIgnore());
            var addtrait = new ToolStripMenuItem("Add as trait", null, AddTraitClicked);
            
            Items.Add(ignore);
            Items.Add(addtrait);
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
        public async void AddTraitClicked(object sender, EventArgs args)
        {
            await AddTrait();
        }

        public async Task AddTrait()
        {
            if (Excel.Ignore)
                return;

            var query = new AddTraitCommand
            {
                Name = Excel.Data.ColumnName,
                Type = (Excel.Source.ExtendedProperties["Type"] as Type).Name
            };

            await QueryManager.Request(query);

            traitExists = true;
            Root.Refresh();
        }

        /// <summary>
        /// Tests if the column is referring to a known database trait
        /// </summary>
        public async Task CheckForTrait()
        {
            if (Excel.Data is null)
            {
                traitExists = false;
                return;
            }                

            // Test if the trait is in the database
            bool inDB = await QueryManager.Request(new TraitExistsQuery() { Name = Excel.Data.ColumnName });

            // Test if the trait is in the excel data
            bool inExcel()
            {
                // Is there a traits table
                if (Excel.Data.Table?.DataSet?.Tables["Traits"] is not DataTable traits)
                    return false;

                // Is there a column with trait names
                if (traits.Columns["Name"] is not DataColumn name)
                    return false;

                var rows = traits.Rows.Cast<DataRow>();
                var trait = Excel.Data.ColumnName.ToLower();

                return rows.Any(r => r[name].ToString().ToLower() == trait);
            };

            traitExists = inDB || inExcel();
        }
    }
}
