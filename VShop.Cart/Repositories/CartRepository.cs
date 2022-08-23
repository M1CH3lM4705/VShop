using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.Cart.Context;
using VShop.Cart.DTOs;
using VShop.Cart.Models;

namespace VShop.Cart.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CartRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CleanCartAsync(string userId)
    {
        var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

        if(cartHeader is not null)
        {
            _context.CartItems.RemoveRange(_context.CartItems
                .Where(c => c.CartHeaderId == cartHeader.Id));
            
            _context.CartHeaders.Remove(cartHeader);

            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteItemCartAsync(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            int total = _context.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();

            _context.CartItems.Remove(cartItem);

            if (total == 1)
            {
                var cartHeaderRemove = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);
                _context.CartHeaders.Remove(cartHeaderRemove);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        Carrinho cart = new Carrinho
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
            .Include(c => c.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> UpdateCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }
}
