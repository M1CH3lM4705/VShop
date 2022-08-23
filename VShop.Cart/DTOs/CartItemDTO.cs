namespace VShop.Cart.DTOs;

public class CartItemDTO
{
    public int Id { get; set; }
    public ProductDTO Product { get; set; } =  new();
    public int Quantity { get; set; } = 1;
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderDTO CartHeader { get; set; } = new();
}
