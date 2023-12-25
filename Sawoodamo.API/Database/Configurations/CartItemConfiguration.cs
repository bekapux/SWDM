﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sawoodamo.API.Database.Configurations;

public sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).HasMaxLength(Constants.Other.GuidMaxLength).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasIndex(x => x.UserId);

        builder.HasQueryFilter(x => x.Product != null && !x.Product.IsDeleted);
    }
}
