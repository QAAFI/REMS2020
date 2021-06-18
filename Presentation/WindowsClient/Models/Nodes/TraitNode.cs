using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Application.Common;
using Rems.Application.CQRS;

namespace WindowsClient.Models
{
    public class TraitNode : DataNode<ExcelColumn, DataColumn>
    {
        private ToolStripMenuItem addtrait;

        public override string Key
        {
            get
            {
                string key = Excel.Valid ? "Valid" : "Invalid";
                key += Excel.Ignore ? "Off" : "On";

                return key;
            }
        }

        public TraitNode(ExcelColumn excel) : base(excel)
        {
            addtrait = new ToolStripMenuItem("Add as trait", null, AddTraitClicked);

            Items.Add(addtrait);
        }

        #region Menu functions
        /// <inheritdoc/>
        protected override void OnMenuOpening(object sender, EventArgs args)
        {
                
        }

        /// <summary>
        /// Adds a trait to the database representing the current node
        /// </summary>
        public async void AddTraitClicked(object sender, EventArgs args)
        {
            await AddTrait();
            InvokeUpdated();
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

            await Excel.CheckIfTrait();
        }

        
        #endregion        

        public override void Refresh()
        {
            ImageKey = SelectedImageKey = Key;
        }
    }
}
