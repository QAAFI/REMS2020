using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace REMS.Context.Entities
{
    public class Sowing : BaseEntity
    {
        public Sowing() : base()
        {
            
        }

        public int SowingId { get; set; }

        public int ExperimentId { get; set; }

        public DateTime? Date { get; set; }

        public string Cultivar { get; set; }
        public double? RowSpace { get; set; }
        public double? Depth { get; set; }
        public double? Population { get; set; }
        public double? FTN { get; set; }
        public string SkipConfig { get; set; } //single, double, skip

        public string Notes { get; set; }

        public virtual Experiment Experiment { get; set; }

        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sowing>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.SowingId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.SowingId)
                    .HasName("SowingId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.FTN)
                    .HasDefaultValueSql("0");

                //entity.Property(e => e.Date)
                //    .HasDefaultValueSql(null);

                //entity.Property(e => e.RowSpacing)
                //    .HasDefaultValueSql(null);

                //entity.Property(e => e.Depth)
                //    .HasDefaultValueSql(null);

                //entity.Property(e => e.Population)
                //    .HasDefaultValueSql(null);

                entity.Property(e => e.Cultivar)
                    .HasMaxLength(30);

                entity.Property(e => e.SkipConfig)
                    .HasMaxLength(30);

                entity.Property(e => e.Notes)
                    .HasMaxLength(50);

                // Define foreign key constraints
                entity.HasOne(d => d.Experiment)
                    .WithOne(p => p.Sowing)
                    .HasForeignKey<Sowing>(p => p.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
