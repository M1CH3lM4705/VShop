using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context.Mappings;

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Products)
            .WithOne(c => c.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Material Escolar",
            },
            new Category
            {
                CategoryId = 2,
                Name = "Acessórios",
            }
        );
    }
}
