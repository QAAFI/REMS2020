using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Level : BaseEntity
    {
        public Level() : base()
        {
            Designs = new HashSet<Design>();
        }

        public int LevelId { get; set; }

        public int? FactorId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }


        public virtual Factor Factor { get; set; }
        public virtual ICollection<Design> Designs { get; set; }

        public override void BuildModel(ModelBuilder modelBuilder)
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

                entity.Property(e => e.Name)
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
