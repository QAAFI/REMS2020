using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Method>(entity =>
            {
                entity.HasKey(e => e.MethodId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.MethodName)
                    .HasName("Method");

                entity.HasIndex(e => e.MethodId)
                    .HasName("MethodID");

                entity.Property(e => e.MethodId).HasColumnName("MethodID");

                entity.Property(e => e.MethodName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15);
            });

        }
    }
}