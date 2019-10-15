using System;
using Schema = System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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


        public virtual MetStation MetStation { get; set; }
        public virtual Trait Trait { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetData>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => new {
                    e.MetStationId,
                    e.TraitId,
                    e.Date
                })
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetDataMetStationId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("MetDataTraitId");

                // Define the table properties
                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasDefaultValueSql("0");

                // Define foregin key constraints
                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.MetStationId)
                    .HasConstraintName("MetDataMetStationId");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.MetData)
                    .HasForeignKey(d => d.TraitId)
                    .HasConstraintName("MetDataTraitId");
            });

        }
    }
}
