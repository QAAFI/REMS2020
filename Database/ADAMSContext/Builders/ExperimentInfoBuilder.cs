using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildExperimentInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExperimentInfo>(entity =>
            {
                entity.HasKey(e => e.ExperimentInfoId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ExperimentId)
                    .HasName("TestNameID");

                entity.HasIndex(e => e.ExperimentInfoId)
                    .HasName("OtherTestNameID");

                entity.HasIndex(e => e.Variable)
                    .HasName("LevelID149");

                entity.Property(e => e.ExperimentInfoId).HasColumnName("ExpInfoID");

                entity.Property(e => e.ExperimentId)
                    .HasColumnName("ExpID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InfoType).HasMaxLength(1);

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.Experiment)
                    .WithMany(p => p.ExperimentInfo)
                    .HasForeignKey(d => d.ExperimentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ExperimentsExpInfo");
            });
        }
    }
}