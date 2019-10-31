using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Crop : BaseEntity
    {
        public Crop() : base()
        {
            Experiments = new HashSet<Experiment>();
        }

        public int CropId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }

        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crop>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.CropId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.CropId)
                    .HasName("CropId")
                    .IsUnique();

                // Define the table properties
                entity.Property(e => e.CropId)
                    .HasColumnName("CropId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(30);
            });
        }
    }
}
