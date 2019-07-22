using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildPlot(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plot>(entity =>
            {
                entity.HasKey(e => e.PlotId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.PlotId)
                    .HasName("PlotID");

                entity.HasIndex(e => e.TreatmentId)
                    .HasName("TreatmentsPlots");

                entity.Property(e => e.PlotId).HasColumnName("PlotID");

                entity.Property(e => e.Columns).HasDefaultValueSql("0");

                entity.Property(e => e.Rep).HasDefaultValueSql("0");

                entity.Property(e => e.Rows).HasDefaultValueSql("0");

                entity.Property(e => e.TreatmentId)
                    .HasColumnName("TreatmentID")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.Plots)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TreatmentsPlots");
            });

        }
    }
}