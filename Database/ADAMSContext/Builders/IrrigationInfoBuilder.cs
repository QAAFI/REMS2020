using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildIrrigationInfo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IrrigationInfo>(entity =>
            {
                entity.HasIndex(e => e.IrrigationId)
                    .HasName("OperationID");

                entity.HasIndex(e => e.IrrigationInfoId)
                    .HasName("FactorLevelID12");

                entity.HasIndex(e => e.Variable)
                    .HasName("LevelID94");

                entity.Property(e => e.IrrigationInfoId).HasColumnName("IrrigInfoID");

                entity.Property(e => e.IrrigationId)
                    .HasColumnName("IrrigID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Value).HasMaxLength(20);

                entity.Property(e => e.Variable).HasMaxLength(20);

                entity.HasOne(d => d.Irrigation)
                    .WithMany(p => p.IrrigationInfo)
                    .HasForeignKey(d => d.IrrigationId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IrrigationIrrigInfo");
            });

        }
    }
}