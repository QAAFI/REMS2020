using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class IrrigationInfo : BaseEntity
    {
        public IrrigationInfo() : base()
        { }

        public int IrrigationInfoId { get; set; }

        public int? IrrigationId { get; set; }

        public string Variable { get; set; }

        public string Value { get; set; }


        public virtual Irrigation Irrigation { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
                    .HasName("IrrigationInfoVariable");

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
                    .HasColumnName("Variable")
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
