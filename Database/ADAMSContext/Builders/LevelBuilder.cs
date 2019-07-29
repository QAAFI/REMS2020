using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildLevel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.LevelId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.FactorId)
                    .HasName("LevelFactorId");

                entity.HasIndex(e => e.LevelId)                
                    .HasName("LevelId");

                // Define the table properties
                entity.Property(e => e.LevelId)
                    .HasColumnName("LevelId");

                entity.Property(e => e.FactorId)
                    .HasColumnName("FactorId");

                entity.Property(e => e.LevelName)
                    .HasColumnName("Name")
                    .HasMaxLength(40);

                // Define foreign key constraints
                entity.HasOne(d => d.Factor)
                    .WithMany(p => p.Level)
                    .HasForeignKey(d => d.FactorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LevelFactorId");
            });

        }
    }
}