namespace VShop.Web.Models;

public class CartViewModel
{
    public CartHeaderViewModel CartHeader { get; set; } = new();
    public IEnumerable<CartItemViewModel> CartItems { get; set; } = Enumerable.Empty<CartItemViewModel>();

    internal void GetTotalAmount()
    {
        CartHeader.TotalAmount = CartItems.Sum(ci => ci.GetPriceItems());
    }

    internal bool TemItens()
    {
        return CartItems.Any();
    }
    internal void AddCoupon(string coupon)
    {
        CartHeader.CouponCode = coupon;
    }
    internal string GetCoupon()
    {
        return CartHeader.CouponCode;
    }

    internal void GetDiscount(){
        CartHeader.GetTotalDiscount();
    }
}
