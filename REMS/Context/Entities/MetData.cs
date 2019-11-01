using System;
using Schema = System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class MetData : BaseEntity
    {
        public MetData() : base()
        { }

        public int MetStationId { get; set; }

        public int? TraitId { get; set; }

        public DateTime Date { get; set; }

        public double? Value { get; set; }


        public virtual MetStation MetStation { get; set; }
        public virtual Trait Trait { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetData>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => new {
                        e.MetStationId,
                        e.TraitId,
                        e.Date})
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
