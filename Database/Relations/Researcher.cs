using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Database
{
    [Relation("Researcher")]
    public class Researcher
    {
        public Researcher()
        {
            ResearcherLists = new HashSet<ResearcherList>();
        }

        // For use with Activator.CreateInstance
        public Researcher(
            double researcherId,
            string name,
            string organisation,
            string notes
        )
        {
            ResearcherId = (int)researcherId;
            Name = name;
            Organisation = organisation;
            Notes = notes;
        }

        [PrimaryKey]
        [Column("ResearcherId")]
        public int ResearcherId { get; set; }

        [Nullable]
        [Column("Name")]
        public string Name { get; set; }

        [Nullable]
        [Column("Organisation")]
        public string Organisation { get; set; }

        [Nullable]
        [Column("Notes")]
        public string Notes { get; set; }

        public virtual ICollection<ResearcherList> ResearcherLists { get; set; }


        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Researcher>(entity =>
            {
                entity.HasKey(e => e.ResearcherId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ResearcherId)
                    .HasName("ResearcherID");

                entity.Property(e => e.ResearcherId).HasColumnName("ResearcherID");

                entity.Property(e => e.Organisation).HasMaxLength(15);

                entity.Property(e => e.Name).HasMaxLength(50);
            });
        }
    }
}
