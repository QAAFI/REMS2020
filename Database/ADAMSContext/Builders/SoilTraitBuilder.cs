using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoilTrait(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilTrait>(entity =>
            {
                entity.HasKey(e => e.SoilTraitId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilId)
                    .HasName("SoilsSoilTraits");

                entity.HasIndex(e => e.SoilTraitId)
                    .HasName("SoilTraitID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitsSoilTraits");

                entity.Property(e => e.SoilTraitId).HasColumnName("SoilTraitID");

                entity.Property(e => e.SoilId)
                    .HasColumnName("SoilID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");

                entity.HasOne(d => d.Soil)
                    .WithMany(p => p.SoilTraits)
                    .HasForeignKey(d => d.SoilId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilsSoilTraits");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilTraits)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitsSoilTraits");
            });

        }
    }
}