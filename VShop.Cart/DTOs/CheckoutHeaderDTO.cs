namespace VShop.Cart.DTOs;

public class CheckoutHeaderDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; } = 0.00m;
    public decimal Discount { get; set; } = 0.00m;
    public ClienteDTO Cliente { get; set; } = new();
    public CreditCardDTO Card { get; set; } = new();

    public int CartTotalItens { get; set; }
    public IEnumerable<CartItemDTO>? CartItems { get; set; }
}
