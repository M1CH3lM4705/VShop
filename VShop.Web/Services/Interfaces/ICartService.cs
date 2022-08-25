using VShop.Web.Models;

namespace VShop.Web.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel> GetCartByUserIdAsync(string userId);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
    Task<bool> RemoveItemFromCartAsync(int cartId);

    // Task<bool> ApplyCouponAsync(CartViewModel cartVM, string couponCode);
    // Task<bool> RemoveCouponAsync(string userId);
    // Task<bool> ClearCartAsync(string userId);

    // Task<CartViewModel> CheckoutAsync(CartHeaderViewModel cartHeader);
}
