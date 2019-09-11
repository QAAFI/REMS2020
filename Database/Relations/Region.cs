using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("Region")]
    public class Region
    {
        public Region()
        {
            Sites = new HashSet<Site>();
        }

        // For use with Activator.CreateInstance
        public Region(
            double regionId,
            string name,
            string notes
        )
        {
            RegionId = (int)regionId;
            Name = name;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("RegionId")]
        public int RegionId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; } = null;

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; } = null;


        public virtual ICollection<Site> Sites { get; set; }



        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.RegionId)
                    .HasName("RegionID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.Name).HasMaxLength(20);
            });
        }
    }
}
