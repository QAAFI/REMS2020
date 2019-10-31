using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Irrigation : BaseEntity
    {
        public Irrigation() : base()
        {
            IrrigationInfo = new HashSet<IrrigationInfo>();
        }

        public int IrrigationId { get; set; }

        public int? MethodId { get; set; }

        public int? TreatmentId { get; set; }

        public DateTime? Date { get; set; }        

        public int? Amount { get; set; }

        public string Notes { get; set; }


        public virtual Method IrrigationMethod { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual ICollection<IrrigationInfo> IrrigationInfo { get; set; }

        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Irrigation>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.IrrigationId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.IrrigationId)
                    .HasName("IrrigationId")
                    .IsUnique();

                entity.HasIndex(e => e.MethodId)
                    .HasName("IrrigationMethodId");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("IrrigationTreatmentId");

                // Define the table properties
                entity.Property(e => e.IrrigationId)
                    .HasColumnName("IrrigationId");

                entity.Property(e => e.Amount)
                    .HasColumnName("Amount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodID");

                entity.Property(e => e.Notes)
                    .HasColumnName("Notes")
                    .HasMaxLength(50);

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentId");

                // Define foreign key constraints
                entity.HasOne(d => d.IrrigationMethod)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.MethodId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationMethodId");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Irrigations)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationTreatmentId");
            });

        }
    }
}
