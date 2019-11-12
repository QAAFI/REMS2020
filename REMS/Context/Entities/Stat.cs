using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Stat : BaseEntity
    {
        public Stat() : base()
        { }

        public int StatId { get; set; }

        public int? TreatmentId { get; set; }

        public int? TraitId { get; set; }

        public int? UnitId { get; set; }

        public DateTime? Date { get; set; }

        public double? Mean { get; set; }

        public double? SE { get; set; }

        public int? Number { get; set; }
        

        public virtual Trait Trait { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stat>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.StatId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.StatId)
                    .HasName("StatId");

                entity.HasIndex(e => e.TraitId)
                    .HasName("StatTraitId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("StatTreatmentId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("StatUnitId");

                // Define properties
                entity.Property(e => e.StatId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("StatId");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Date)
                    .HasColumnName("Date");

                entity.Property(e => e.Mean)
                    .HasColumnName("Mean")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Number)
                    .HasColumnName("Number")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.SE)
                    .HasColumnName("SE")
                    .HasDefaultValueSql("0");
                

                // Define constraints
                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatTraitId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatTreatmentId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Stats)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("StatUnitId");
            });

        }
    }
}
