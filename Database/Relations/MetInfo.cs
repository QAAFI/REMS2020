using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("MetInfo")]
    public class MetInfo
    {
        [PrimaryKey]
        [Column("MetInfoId")]
        public int MetInfoId { get; set; }

        [Column("MetStationId")]
        public int? MetStationId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual MetStation MetStation { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetInfo>(entity =>
            {
                entity.HasIndex(e => e.MetInfoId)
                    .HasName("MetInfoID");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationsMetInfo");

                entity.HasIndex(e => e.Variable)
                    .HasName("Variable");

                entity.Property(e => e.MetInfoId).HasColumnName("MetInfoID");

                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetInfo)
                    .HasForeignKey(d => d.MetStationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MetStationsMetInfo");
            });

        }
    }
}
