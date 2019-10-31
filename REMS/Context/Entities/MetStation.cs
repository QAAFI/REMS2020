using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class MetStation : BaseEntity
    {
        public MetStation() : base()
        {
            Experiments = new HashSet<Experiment>();
            MetData = new HashSet<MetData>();
            MetInfo = new HashSet<MetInfo>();
        }

        public int MetStationId { get; set; }

        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double? Elevation { get; set; }

        public double? Amp { get; set; }

        public double? TemperatureAverage { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
        public virtual ICollection<MetData> MetData { get; set; }
        public virtual ICollection<MetInfo> MetInfo { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetStation>(entity =>
            {
                // Define the keys
                entity.HasKey(e => e.MetStationId)
                    .HasName("PrimaryKey");

                // Define the indices
                entity.HasIndex(e => e.MetStationId)
                    .HasName("MetStationId");

                // Define the properties
                entity.Property(e => e.MetStationId)
                    .HasColumnName("MetStationId");

                entity.Property(e => e.Amp)
                    .HasColumnName("Amp")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(40);

                entity.Property(e => e.TemperatureAverage)
                    .HasColumnName("TemperatureAverage")
                    .HasDefaultValueSql("0");
            });

        }
    }
}
