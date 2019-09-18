using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    [Relation("ResearcherList")]
    public class ResearcherList
    {
        [PrimaryKey]
        [Column("ResearcherListId")]
        public int ResearcherListId { get; set; }

        [Column("ResearcherId")]
        public int? ResearcherId { get; set; }

        [Column("ExperimentId")]
        public int? ExperimentId { get; set; }        

        public virtual Experiment Experiment { get; set; }
        public virtual Researcher Researcher { get; set; }


        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearcherList>(entity =>
            {
                // Define keys
                entity.HasKey(e => e.ResearcherListId)
                    .HasName("PrimaryKey");

                // Define indices
                entity.HasIndex(e => e.ExperimentId)
                    .HasName("ResearcherListExperimentId");

                entity.HasIndex(e => e.ResearcherId)
                    .HasName("ResearcherListResearcherId");

                entity.HasIndex(e => e.ResearcherListId)
                    .HasName("ResearcherListId");

                // Define properties
                entity.Property(e => e.ResearcherListId)
                    .HasColumnName("ResearcherListId");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExperimentId")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ResearcherId)
                    .HasColumnName("ResearcherId")
                    .HasDefaultValueSql("0");

                // Define constraints
                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.ResearcherList)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ResearcherListExperimentId");

                entity.HasOne(d => d.Researcher)
                    .WithMany(p => p.ResearcherLists)
                    .HasForeignKey(d => d.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ResearcherListResearcherId");
            });
        }
    }
}
