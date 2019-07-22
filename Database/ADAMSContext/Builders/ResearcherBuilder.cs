using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildResearcher(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Researcher>(entity =>
            {
                entity.HasKey(e => e.ResearcherId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ResearcherId)
                    .HasName("ResearcherID");

                entity.Property(e => e.ResearcherId).HasColumnName("ResearcherID");

                entity.Property(e => e.Organisation).HasMaxLength(15);

                entity.Property(e => e.ResearcherName).HasMaxLength(50);
            });
        }
    }
}