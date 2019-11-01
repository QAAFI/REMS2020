using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Region : BaseEntity
    {
        public Region() : base()
        {
            Sites = new HashSet<Site>();
        }

        public int RegionId { get; set; }

        public string Name { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual ICollection<Site> Sites { get; set; }



        public override void BuildModel(ModelBuilder modelBuilder)
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
