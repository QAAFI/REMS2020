using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context.Entities
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


        public static void BuildModel(ModelBuilder modelBuilder)
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
