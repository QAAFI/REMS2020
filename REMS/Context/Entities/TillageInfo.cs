using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class TillageInfo : BaseEntity
    {
        public TillageInfo() : base()
        { }

        public int TillageInfoId { get; set; }

        public int? TillageId { get; set; }

        public string Variable { get; set; }

        public string Value { get; set; }


        public virtual Tillage Tillage { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TillageInfo>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.TillageInfoId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.TillageId)
                    .HasName("TillageInfoTillageId");

                entity.HasIndex(e => e.TillageInfoId)
                    .HasName("TillageInfoId");

                entity.HasIndex(e => e.Variable)
                    .HasName("TillageInfoVariable");

                // Define properties
                entity.Property(e => e.TillageInfoId)
                    .HasColumnName("TillageInfoId");

                entity.Property(e => e.TillageId)
                    .HasColumnName("TillageID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasMaxLength(20);

                entity.Property(e => e.Variable)
                    .HasColumnName("Variable")
                    .HasMaxLength(20);

                // Define constraints
                entity.HasOne(d => d.Tillage)
                    .WithMany(p => p.TillageInfo)
                    .HasForeignKey(d => d.TillageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TillageInfoTillageId");
            });

        }
    }
}
