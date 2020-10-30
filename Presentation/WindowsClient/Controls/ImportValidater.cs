using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class ImportValidater : Validater
    {
        protected override void FillRows()
        {
            throw new NotImplementedException();
        }

        protected override void ValidateRow(ValidaterRow row)
        {
            throw new NotImplementedException();
        }

        public void OnFoundInvalids(IEnumerable<string> items)
        {
            foreach (var item in items) AddRow(item, "");

            MessageBox.Show("Invalid items found in import. Please resolve conflicts before continuing.");
        }
    }
}
