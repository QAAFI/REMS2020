using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("Site")]
    public class Site
    {
        public Site()
        {
            Fields = new HashSet<Field>();
        }

        // For use with Activator.CreateInstance
        public Site(
            double siteId,
            double regionId,
            string name,
            double? latitude,
            double? longitude,
            double? elevation,
            string notes
        )
        {
            SiteId = (int)siteId;
            RegionId = (int)regionId;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("SiteId")]
        public int SiteId { get; set; }

        [Column("RegionId")]
        public int RegionId { get; set; }

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
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.SiteId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.RegionId)
                    .HasName("SiteRegionId");

                entity.HasIndex(e => e.SiteId)
                    .HasName("SiteId");

                entity.HasIndex(e => e.Name)
                    .HasName("SiteName");

                // Define properties
                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteId");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50);

                // Define constraints
                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SiteRegionId");
            });

        }
    }
}
