using VShop.Web.Models;

namespace VShop.Web.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel> GetCartByUserIdAsync(string userId);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM);
    Task<bool> RemoveItemFromCartAsync(int cartId);

    Task<bool> ApplyCouponAsync(CartViewModel cartVM);
    Task<bool> RemoveCouponAsync(string userId);
    // Task<bool> ClearCartAsync(string userId);

    Task<CartHeaderViewModel> CheckoutAsync(CartHeaderViewModel cartHeader);
}
