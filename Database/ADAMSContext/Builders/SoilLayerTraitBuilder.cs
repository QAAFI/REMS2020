using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildSoilLayerTrait(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoilLayerTrait>(entity =>
            {
                entity.HasKey(e => e.SoilLayerTraitId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.SoilLayerId)
                    .HasName("SoilLayersSoilLayerTraits");

                entity.HasIndex(e => e.SoilLayerTraitId)
                    .HasName("SoilLayerTraitID");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitsSoilLayerTraits");

                entity.Property(e => e.SoilLayerTraitId).HasColumnName("SoilLayerTraitID");

                entity.Property(e => e.SoilLayerId)
                    .HasColumnName("SoilLayerID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraitId)
                    .HasColumnName("TraitID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasDefaultValueSql("0");

                entity.HasOne(d => d.SoilLayer)
                    .WithMany(p => p.SoilLayerTraits)
                    .HasForeignKey(d => d.SoilLayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SoilLayersSoilLayerTraits");

                entity.HasOne(d => d.Trait)
                    .WithMany(p => p.SoilLayerTraits)
                    .HasForeignKey(d => d.TraitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TraitsSoilLayerTraits");
            });

        }
    }
}