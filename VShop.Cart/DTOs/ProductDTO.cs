

namespace VShop.Cart.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
    public string? Nome { get; set; } = string.Empty;

    public Decimal Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public long Stock { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;
    public string? CategoryName { get; set; } = string.Empty;
}
