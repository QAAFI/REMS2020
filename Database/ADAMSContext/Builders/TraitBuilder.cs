using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildTrait(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trait>(entity =>
            {
                entity.HasKey(e => e.TraitId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.DefaultUnitId)
                    .HasName("UnitsTrait");

                entity.HasIndex(e => e.TraitId)
                    .HasName("TraitID");

                entity.Property(e => e.TraitId).HasColumnName("TraitID");

                entity.Property(e => e.DefaultUnitId).HasColumnName("DefaultUnitsID");

                entity.Property(e => e.Description).HasMaxLength(60);

                entity.Property(e => e.TraitName)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.TraitType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.DefaultUnit)
                    .WithMany(p => p.Traits)
                    .HasForeignKey(d => d.DefaultUnitId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UnitsTrait");
            });

        }
    }
}