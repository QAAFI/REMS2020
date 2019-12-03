using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rems.Persistence.Configurations
{
    public class TreatmentGroupItemConfiguration : IEntityTypeConfiguration<TreatmentGroupItem>
    {
        public void Configure(EntityTypeBuilder<TreatmentGroupItem> builder)
        {
            builder.HasKey(e => new { e.TreatmentGroupId, e.TreatmentId})
                .HasName("PrimaryKey");

            builder.HasOne(d => d.TreatmentGroup)
                .WithMany(p => p.Treatments)
                .HasForeignKey<TreatmentGroup>(d => d.TreatmentGroupId);
        }
    }
}
