using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Site : BaseEntity
    {
        public Site() : base()
        {
            Fields = new HashSet<Field>();
        }

        public int SiteId { get; set; }

        public int RegionId { get; set; }

        public string Name { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public double? Elevation { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Region Region { get; set; }
        public virtual ICollection<Field> Fields { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
