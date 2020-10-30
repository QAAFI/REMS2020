using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class ExportValidater : Validater
    {
        protected override void ValidateRow(ValidaterRow row)
        {
            var exists = new TraitExistsQuery() { Validater = row };
            SendQuery(exists);
        }

        protected override void FillRows()
        {
            grid.Rows.Clear();
            foreach (var item in Items)
                AddRow(item.Key, item.Value);

            Refresh();
        }
    }
}
