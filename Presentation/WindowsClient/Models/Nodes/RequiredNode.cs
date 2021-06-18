using System;
using System.Data;
using System.Windows.Forms;
using Rems.Application.Common;

namespace WindowsClient.Models
{
    public class RequiredNode : DataNode<ExcelColumn, DataColumn>
    {
        public override string Key
        {
            get
            {
                string key = Excel.Valid ? "Valid" : "Invalid";
                key += Excel.Ignore ? "Off" : "On";

                return key;
            }
        }

        public RequiredNode(ExcelColumn excel) : base(excel)
        { }

        #region Menu functions
        /// <inheritdoc/>
        protected override void OnMenuOpening(object sender, EventArgs args)
        {
            Items.Clear();

            if (Root is not TableNode table)
                return;

            if (Excel.Valid)
            {
                var detach = new ToolStripMenuItem("Detach from column", null, (s, e) => DetachColumn(table))
                {
                    ToolTipText = "Detach the column from this node and put it back into the " +
                    "'Traits' pool, making it available to attach to other nodes."
                };
                Items.Add(detach);
            }
            // Add an option to set the column
            else
            {
                foreach (TraitNode node in table.Traits.Nodes)
                {
                    var attach = new ToolStripMenuItem("Attach to " + node.Text, null, (s, e) => AttachColumn(node))
                    {
                        ToolTipText = "Specify the column that contains the required data for this node."
                    };
                    Items.Add(attach);
                }
            }            
        }

        private void DetachColumn(TableNode table)
        {            
            var node = new TraitNode(Excel);
            table.Traits.Nodes.Add(node);
            node.Refresh();

            var col = new DataColumn(Text + " not found");
            Excel = new ExcelColumn
            {
                Data = col,
                Valid = false,
                Info = node.Excel.Info
            };

            Refresh();
        }

        private void AttachColumn(TraitNode node)
        {
            Text = Excel.Info.Name;
            node.Excel.Info = Excel.Info;
            node.Excel.Data.ColumnName = Excel.Info.Name;

            Excel = node.Excel;            
            
            node.Parent.Nodes.Remove(node);
            Excel.Valid = true;
            Refresh();
        }        
        #endregion        

        public override void Refresh()
        {
            ImageKey = SelectedImageKey = Key;
        }
    }
}
