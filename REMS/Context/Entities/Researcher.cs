using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
{
    public class Researcher : BaseEntity
    {
        public Researcher() : base()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        public int ResearcherId { get; set; }

        public string Name { get; set; }

        public string Organisation { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }


        public override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Researcher>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.ResearcherId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.ResearcherId)
                    .HasName("ResearcherID");

                // Define properties
                entity.Property(e => e.ResearcherId)
                    .HasColumnName("ResearcherID");

                entity.Property(e => e.Organisation)
                    .HasColumnName("Organisation")
                    .HasMaxLength(15);

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(50);
            });
        }
    }
}
