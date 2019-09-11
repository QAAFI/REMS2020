using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
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


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.SiteId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.RegionId)
                    .HasName("RegionsSites");

                entity.HasIndex(e => e.SiteId)
                    .HasName("SiteID123");

                entity.HasIndex(e => e.Name)
                    .HasName("SiteName");

                entity.Property(e => e.SiteId).HasColumnName("SiteID");

                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Sites)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("RegionsSites");
            });

        }
    }
}
