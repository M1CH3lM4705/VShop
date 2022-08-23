using VShop.Cart.DTOs;

namespace VShop.Cart.Repositories;

    public interface ICartRepository
    {
        Task<CartDTO> GetCartByUserIdAsync(string userId);
        Task<CartDTO> UpdateCartAsync(CartDTO cart);
        Task<bool> DeleteItemCartAsync(int cartItemId);
        Task<bool> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> DeleteCouponAsync(string userId);
        Task<bool> CleanCartAsync(string userId);
    }
