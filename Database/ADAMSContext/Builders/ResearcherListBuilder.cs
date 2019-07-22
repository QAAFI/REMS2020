using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildResearcherList(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResearcherList>(entity =>
            {
                entity.HasIndex(e => e.ExperimentId)
                    .HasName("ExpIDnnn");

                entity.HasIndex(e => e.ResearcherId)
                    .HasName("ResearchersResearcherList");

                entity.HasIndex(e => e.ResearcherListId)
                    .HasName("ResListID");

                entity.Property(e => e.ResearcherListId).HasColumnName("ResearcherListID");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ResearcherId)
                    .HasColumnName("ResearcherID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.ResearcherList)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentsResearcherList");

                entity.HasOne(d => d.Researcher)
                    .WithMany(p => p.ResearcherLists)
                    .HasForeignKey(d => d.ResearcherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ResearchersResearcherList");
            });
        }
    }
}