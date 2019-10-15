using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    [Relation("Level")]
    public class Level
    {
        public Level()
        {
            Designs = new HashSet<Design>();
        }

        [PrimaryKey]
        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("FactorId")]
        public int? FactorId { get; set; }

        [Column("LevelName")]
        public string LevelName { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual Factor Factor { get; set; }
        public virtual ICollection<Design> Designs { get; set; }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.LevelId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FactorId)
                    .HasName("LevelFactorId");

                entity.HasIndex(e => e.LevelId)
                    .HasName("LevelId");

                // Define the table properties
                entity.Property(e => e.LevelId)
                    .HasColumnName("LevelId");

                entity.Property(e => e.FactorId)
                    .HasColumnName("FactorId");

                entity.Property(e => e.LevelName)
                    .HasColumnName("Name")
                    .HasMaxLength(40);

                // Define foreign key constraints
                entity.HasOne(d => d.Factor)
                    .WithMany(p => p.Level)
                    .HasForeignKey(d => d.FactorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LevelFactorId");
            });

        }
    }
}
