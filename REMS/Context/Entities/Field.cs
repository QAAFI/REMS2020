using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    [Relation("Field")]
    public class Field
    {
        public Field()
        {
            Experiments = new HashSet<Experiment>();
        }

        // For use with Activator.CreateInstance
        public Field(
            double fieldId,
            double siteId,
            double soilId,
            string name,
            double? latitude,
            double? longitude,
            double? elevation,
            double? slope,
            int? depth,
            string notes
        )
        {
            FieldId = (int)fieldId;
            SiteId = (int)siteId;
            SoilId = (int)soilId;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Slope = slope;
            Depth = depth;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("FieldId")]
        public int FieldId { get; set; }

        [Column("SiteId")]
        public int SiteId { get; set; }

        [Column("SoilId")]
        public int SoilId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Latitude")]
        public double? Latitude { get; set; } = null;

        [Nullable]
        [Column("Longitude")]
        public double? Longitude { get; set; } = null;

        [Nullable]
        [Column("Elevation")]
        public double? Elevation { get; set; } = null;

        [Nullable]
        [Column("Slope")]
        public double? Slope { get; set; } = null;

        [Nullable]
        [Column("Depth")]
        public int? Depth { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual Site Site { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.FieldId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FieldId)
                    .HasName("FieldId")
                    .IsUnique();

                entity.HasIndex(e => e.SiteId)
                    .HasName("FieldSiteId");

                entity.HasIndex(e => e.SoilId)
                    .HasName("FieldSoilId");

                // Define the table properties
                entity.Property(e => e.FieldId)
                    .HasColumnName("FieldId");

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteId");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20);

                entity.Property(e => e.Latitude)
                    .HasColumnName("Latitude");

                entity.Property(e => e.Longitude)
                    .HasColumnName("Longitude");

                entity.Property(e => e.Elevation)
                    .HasColumnName("Elevation");

                entity.Property(e => e.Slope)
                    .HasColumnName("Slope")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Depth)
                    .HasColumnName("Depth")
                    .HasDefaultValueSql("0");

                // Define foreign key constraints
                entity.HasOne(d => d.Site)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FieldSiteId");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FieldSoilId");
            });

        }
    }
}
