namespace VShop.Cart.Models;

    public class Carrinho
    {
        public CartHeader CartHeader {get; set;} = new CartHeader();
        public IEnumerable<CartItem> CartItems { get; set; } = Enumerable.Empty<CartItem>();
    }
