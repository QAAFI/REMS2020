using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
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


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetInfo>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.MetInfoId)
                    .HasName("PrimaryKey");

                // Define the indices
                entity.HasIndex(e => e.MetInfoId)
                    .HasName("MetInfoId");

                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetInfoMetStationId");

                entity.HasIndex(e => e.Variable)
                    .HasName("MetInfoVariable");

                // Define the properties
                entity.Property(e => e.MetInfoId)
                    .HasColumnName("MetInfoId");

                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value)
                    .HasColumnName("Value")
                    .HasMaxLength(20);

                entity.Property(e => e.Variable)
                    .HasColumnName("Variable")    
                    .HasMaxLength(20);

                // Define the constraints
                entity.HasOne(d => d.MetStation)
                    .WithMany(p => p.MetInfo)
                    .HasForeignKey(d => d.MetStationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("MetInfoMetStationId");
            });

        }
    }
}
