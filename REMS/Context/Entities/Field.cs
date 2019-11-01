using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Field : BaseEntity
    {
        public Field() : base()
        {
            Experiments = new HashSet<Experiment>();
        }        

        public int FieldId { get; set; }

        public int SiteId { get; set; }

        public int SoilId { get; set; }

        public string Name { get; set; } = null;

        public double? Latitude { get; set; } = null;

        public double? Longitude { get; set; } = null;

        public double? Elevation { get; set; } = null;

        public double? Slope { get; set; } = null;

        public int? Depth { get; set; } = null;

        public string Notes { get; set; } = null;


        public virtual Site Site { get; set; }
        public virtual Soil Soil { get; set; }
        public virtual ICollection<Experiment> Experiments { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
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
