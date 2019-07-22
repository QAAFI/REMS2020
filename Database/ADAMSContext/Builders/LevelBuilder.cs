using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildLevel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.LevelId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.FactorId)
                    .HasName("FactorsLevels");

                entity.HasIndex(e => e.LevelId)                
                    .HasName("LevelID");

                entity.Property(e => e.LevelId)
                    .HasColumnName("LevelID");

                entity.Property(e => e.FactorId)
                    .HasColumnName("FactorID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LevelName).HasMaxLength(40);

                entity.HasOne(d => d.Factor)
                    .WithMany(p => p.Level)
                    .HasForeignKey(d => d.FactorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FactorsLevels");
            });

        }
    }
}