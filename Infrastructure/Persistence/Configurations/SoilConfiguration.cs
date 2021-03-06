﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rems.Domain.Entities;

namespace Rems.Persistence.Configurations
{
    public class SoilConfiguration : IEntityTypeConfiguration<Soil>
    {
        public void Configure(EntityTypeBuilder<Soil> builder)
        {
            builder.HasKey(e => e.SoilId)
                .HasName("PrimaryKey");

            builder.HasIndex(e => e.SoilId)
                .HasDatabaseName("SoilId")
                .IsUnique();

            builder.Property(e => e.SoilId)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.SoilType)
                .HasMaxLength(30);
        }
    }
}

