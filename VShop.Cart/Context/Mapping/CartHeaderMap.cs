

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.Cart.Models;

namespace VShop.Cart.Context.Mapping;

public class CartHeaderMap : IEntityTypeConfiguration<CartHeader>
{
    public void Configure(EntityTypeBuilder<CartHeader> builder)
    {
        builder.Property(c => c.UserId)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.CouponCode)
            .HasMaxLength(100);
    }
}
