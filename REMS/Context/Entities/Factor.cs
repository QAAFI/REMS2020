using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Factor")]
    public class Factor
    {
        public Factor()
        {
            Level = new HashSet<Level>();
        }

        [PrimaryKey]
        [Column("FactorId")]
        public int FactorId { get; set; }

        [Column("FactorName")]
        public string Name { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }


        public virtual ICollection<Level> Level { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factor>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FactorId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FactorId)
                    .HasName("FactorId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.FactorId)
                    .HasColumnName("FactorId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20);
            });
        }
    }
}
