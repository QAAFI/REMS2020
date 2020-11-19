using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public class ExportValidater : Validater
    {
        public Dictionary<string, string> Items = new Dictionary<string, string>()
        {
            // Physical
            { "BD", "" },
            { "AirDry", "" },
            { "LL15", "" },
            { "DUL", "" },
            { "SAT", "" },
            { "KS", "" },
                
            // SoilCrop
            { "LL", "" },
            { "KL", "" },
            { "XF", "" },

            // WaterBalance
            { "SWCON", "" },
            { "KLAT", "" },

            // Organic
            { "Carbon", "" },
            { "SoilCNRatio", "" },
            { "FBiom", "" },
            { "FInert", "" },
            { "FOM", "" },

            // Chemical
            { "NO3N", "" },
            { "NH4N", "" },
            { "PH", "" },

            // Sample
            { "SW", "" },
        };

        public ExportValidater() : base()
        { }

        protected override void ValidateRow(ValidaterRow row)
        {
            var exists = new TraitExistsQuery();
            SendQuery(exists);
        }

        public void FillRows()
        {
            grid.Rows.Clear();
            foreach (var item in Items)
                AddRow(item.Key, item.Value);

            Refresh();
        }
    }
}
