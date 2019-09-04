using System;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    [Relation("MetData")]
    public class MetData
    {
        public MetData()
        { }

        public MetData(
            double metStationId,
            double traitId,
            DateTime date
        )
        {
            MetStationId = (int)metStationId;
            TraitId = (int)traitId;
            Date = date;
        }

        [PrimaryKey]
        [Column("MetStationId")]
        [Schema.DatabaseGenerated(Schema.DatabaseGeneratedOption.Identity)]
        public int MetStationId { get; set; }

        [PrimaryKey]
        [Column("TraitId")]
        [Schema.DatabaseGenerated(Schema.DatabaseGeneratedOption.Identity)]
        public int TraitId { get; set; }

        [PrimaryKey]
        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Value")]
        public double? Value { get; set; }


        public virtual MetStations MetStation { get; set; }
        public virtual Trait Trait { get; set; }
    }
}
