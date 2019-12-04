using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class TreatmentGroupConfiguration : IEntityTypeConfiguration<TreatmentGroup>
    {
        public void Configure(EntityTypeBuilder<TreatmentGroup> builder)
        {
            builder.HasKey(e => e.TreatmentGroupId)
                .HasName("PrimaryKey");
            builder.HasIndex(e => e.TreatmentGroupId)
                .HasName("TreatmentGroupId")
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.HasOne(d => d.TreatmentProfile)
                .WithMany(p => p.TreatmentGroups)
                .HasForeignKey(d => d.TreatmentProfileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("TreatmentGroupsId");
        }

    }
}
