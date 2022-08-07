
namespace VShop.ProductApi.Models;

public class Product
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public Decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? ImageUrl { get; set; }
    public Category? Category{get; set;}
    public int CategoryId {get; set;}
}
