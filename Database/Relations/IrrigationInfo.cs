using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("IrrigationInfo")]
    public class IrrigationInfo
    {
        [PrimaryKey]
        [Column("IrrigationInfoId")]
        public int IrrigationInfoId { get; set; }

        [Column("IrrigationId")]
        public int? IrrigationId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Irrigation Irrigation { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IrrigationInfo>(entity =>
            {
                // Define the primary key
                entity.HasIndex(e => e.IrrigationInfoId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.IrrigationInfoId)
                    .HasName("IrrigationInfoId")
                    .IsUnique();

                entity.HasIndex(e => e.IrrigationId)
                    .HasName("IrrigationInfoIrrigationId");

                entity.HasIndex(e => e.Variable)
                    .HasName("IrrigationVariable");

                // Define the table properties
                entity.Property(e => e.IrrigationInfoId)
                    .HasColumnName("IrrigInfoID");

                entity.Property(e => e.IrrigationId)
                    .HasColumnName("IrrigID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasMaxLength(20);

                entity.Property(e => e.Variable)
                    .HasMaxLength(20);

                // Define foreign key constraints
                entity.HasOne(d => d.Irrigation)
                    .WithMany(p => p.IrrigationInfo)
                    .HasForeignKey(d => d.IrrigationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationInfoIrrigationId");
            });

        }
    }
}
