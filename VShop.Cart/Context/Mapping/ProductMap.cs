using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.Cart.Models;

namespace VShop.Cart.Context.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(c => c.ImageUrl)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(c => c.CategoryName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Price)
            .HasPrecision(12, 2);
    }
}
