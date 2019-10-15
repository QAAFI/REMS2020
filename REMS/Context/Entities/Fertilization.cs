using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    [Relation("Fertilization")]
    public class Fertilization
    {
        public Fertilization()
        {
            FertilizationInfo = new HashSet<FertilizationInfo>();
        }

        [PrimaryKey]
        [Column("FertilizationId")]
        public int FertilizationId { get; set; }

        [Column("TreatmentId")]
        public int? TreatmentId { get; set; }

        [Column("FertilizerId")]
        public int? FertilizerId { get; set; }

        [Column("MethodId")]
        public int? MethodId { get; set; }

        [Column("UnitId")]
        public int? UnitId { get; set; }

        [Nullable]
        [Column("Date")]
        public DateTime? Date { get; set; }        

        [Nullable]
        [Column("Amount")]
        public int? Amount { get; set; }

        [Nullable]
        [Column("Depth")]
        public int? Depth { get; set; }        

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Fertilizer Fertilizer { get; set; }
        public virtual Method Method { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<FertilizationInfo> FertilizationInfo { get; set; }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fertilization>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FertilizationId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FertilizationId)
                    .HasName("FertilizationId")
                    .IsUnique();

                entity.HasIndex(e => e.FertilizerId)
                    .HasName("FertilizationFertilizerId");

                entity.HasIndex(e => e.MethodId)
                    .HasName("FertilizationMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("FertilizationTreatmentId");

                entity.HasIndex(e => e.UnitId)
                    .HasName("FertilizationUnitId");

                // Define the table properties
                entity.Property(e => e.FertilizationId)
                    .HasColumnName("FertilizationId");

                entity.Property(e => e.Amount)
                    .HasColumnName("Amount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Depth)
                    .HasColumnName("Depth")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FertilizerId)
                    .HasColumnName("FertilizerId");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodID");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(100);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.UnitId)
                    .HasColumnName("UnitId")
                    .HasDefaultValueSql("0");

                // Define foreign key constraints
                entity.HasOne(d => d.Fertilizer)
                    .WithMany(p => p.Fertilization)
                    .HasForeignKey(d => d.FertilizerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationFertilizerId");

                entity.HasOne(d => d.Method)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationTreatmentId");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Fertilizations)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FertilizationUnitId");
            });
        }
    }
}
