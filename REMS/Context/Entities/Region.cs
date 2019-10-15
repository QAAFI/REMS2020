using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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



        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.RegionId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.RegionId)
                    .HasName("RegionId");

                // Define properties
                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(20);
            });
        }
    }
}
