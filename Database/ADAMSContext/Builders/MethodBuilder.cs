using Microsoft.EntityFrameworkCore;

namespace Database
{
    public partial class ADAMSContext : DbContext
    {
        private void BuildMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Method>(entity =>
            {
                // Define the primary key
                entity.HasKey(e => e.MethodId)
                    .HasName("PrimaryKey");

                // Define the table indices
                entity.HasIndex(e => e.MethodId)
                    .HasName("MethodId")
                    .IsUnique();

                entity.HasIndex(e => e.MethodName)
                    .HasName("MethodName");
                               
                // Define the table properties
                entity.Property(e => e.MethodId)
                    .HasColumnName("MethodId");

                entity.Property(e => e.MethodName)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15);
            });

        }
    }
}