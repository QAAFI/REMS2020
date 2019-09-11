using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("Crop")]
    public class Crop
    {
        public Crop()
        {
            Experiments = new HashSet<Experiment>();
        }

        // For use with Activator.CreateInstance()
        public Crop(
            double cropId,
            string name,
            string notes
        )
        {
            CropId = (int)cropId;
            Name = name;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("CropId")]
        public int CropId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        [ForeignKey]
        public virtual ICollection<Experiment> Experiments { get; set; }

        public static void Build(ModelBuilder modelBuilder)
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
