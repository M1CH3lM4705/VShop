using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Context.Mapping;
public class CouponMap : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(c => c.CouponId);

        builder.Property(c => c.CouponCode)
            .HasMaxLength(30);

        builder.Property(c => c.Discount)
            .HasPrecision(10,2);

        builder.HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "VSHOP_PROMO_10",
            Discount = 10
        });

        builder.HasData(new Coupon
        {
            CouponId = 2,
            CouponCode = "VSHOP_PROMO_20",
            Discount = 20
        });         
    }
}
