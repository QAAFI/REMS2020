using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.CQRS;
using Rems.Infrastructure;

namespace WindowsClient.Models
{
    public class TraitNode : DataNode<ExcelColumn, DataColumn>
    {
        public override bool Valid => true;

        public bool TraitExists;

        public Func<Task> Add;

        public override string Key
        {
            get
            {
                if (Excel.Ignore)
                    return "ValidOff";
                else if (TraitExists)
                    return "ValidOn";
                else
                    return "Add";
            }
        }

        public TraitNode(ExcelColumn excel) : base(excel)
        {
            var ignore = new ToolStripMenuItem("Ignore", null, (s, e) => ToggleIgnore());
            Items.Add(ignore);

            EventHandler handler = async (s, e) => await Add.Invoke();
            var add = new ToolStripMenuItem("Add as trait", null, handler);            

            Items.Add(add);            
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

            TraitExists = true;
            Root.Refresh();
        }

        public async Task AddFactor()
        {
            if (Excel.Ignore)
                return;

            var query = new AddFactorCommand { Name = Excel.Data.ColumnName };
            await QueryManager.Request(query);

            TraitExists = true;
            Root.Refresh();
        }

        /// <summary>
        /// Initialisation that can only be done after being added to a parent node
        /// </summary>
        public async Task Initialise()
        {
            if (Excel.Data.Table.TableName == "Design")
            {
                Parent.Text = "Factors";
                Add = AddFactor;
                Items[1].Text = "Add as factor";
                Parent.ContextMenuStrip.Items[0].Text = "Add all as factors";
            }
            else
                Add = AddTrait;

            if (Excel.Data is null)
            {
                TraitExists = false;
                return;
            }                

            // Test if the trait is in the database
            bool inDB = await QueryManager.Request(new TraitExistsQuery() { Name = Excel.Data.ColumnName });
            bool isFactor = await IsFactor();
            TraitExists = inDB || InExcel() || isFactor;

            if (TraitExists)
                ToolTipText = "REMS found a matching trait";
            else
                ToolTipText = "No matching trait found. One will be added upon import";            
        }

        private bool InExcel()
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
        }

        private async Task<bool> IsFactor()
        {
            if (Parent?.Text != "Design")
                return false;

            var query = new FactorExistsQuery { Name = Excel.Data.ColumnName };
            return await QueryManager.Request(query);
        }
    }
}
