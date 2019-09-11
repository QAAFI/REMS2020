﻿using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("TillageInfo")]
    public class TillageInfo
    {
        [PrimaryKey]
        [Column("TillageInfoId")]
        public int TillageInfoId { get; set; }

        [Column("TillageId")]
        public int? TillageId { get; set; }

        [Column("Variable")]
        public string Variable { get; set; }

        [Column("Value")]
        public string Value { get; set; }


        public virtual Tillage Tillage { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TillageInfo>(entity =>
            {
                entity.HasIndex(e => e.TillageId)
                    .HasName("TillageTillInfo");

                entity.HasIndex(e => e.TillageInfoId)
                    .HasName("FactorLevelID647");

                entity.HasIndex(e => e.Variable)
                    .HasName("LevelID1878");

                entity.Property(e => e.TillageInfoId).HasColumnName("TillInfoID");

                entity.Property(e => e.TillageId)
                    .HasColumnName("TillID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.Tillage)
                    .WithMany(p => p.TillageInfo)
                    .HasForeignKey(d => d.TillageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TillageTillInfo");
            });

        }
    }
}
