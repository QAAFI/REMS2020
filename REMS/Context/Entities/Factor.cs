using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Factor : BaseEntity
    {
        public Factor() : base()
        {
            Level = new HashSet<Level>();
        }

        public int FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Level> Level { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
