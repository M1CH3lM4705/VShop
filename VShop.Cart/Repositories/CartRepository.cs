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
            await _context.SaveChangesAsync();

            if (total == 1)
            {
                var cartHeaderRemove = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);
                _context.CartHeaders.Remove(cartHeaderRemove);
                await _context.SaveChangesAsync();
            }

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
        Carrinho carrinho = _mapper.Map<Carrinho>(cart);

        await SaveProductInDataBase(cart, carrinho);

        var cartHeader = await _context.CartHeaders.AsNoTrackingWithIdentityResolution()
                        .FirstOrDefaultAsync(c => c.UserId == carrinho.CartHeader.UserId);
        
        if(cartHeader is null){
            await CreateHeaderAndItems(carrinho);

            async Task CreateHeaderAndItems(Carrinho carrinho){
                _context.CartHeaders.Add(carrinho.CartHeader);
                await _context.SaveChangesAsync();

                carrinho.CartItems.FirstOrDefault().CartHeaderId = carrinho.CartHeader.Id;
                cart.CartItems.FirstOrDefault().Product = null;

                _context.CartItems.Add(carrinho.CartItems.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }
        else
        {
            await UpdateQuantityAndItems(cart, carrinho, cartHeader);
        }

        return _mapper.Map<CartDTO>(carrinho);
    }

    private async Task UpdateQuantityAndItems(CartDTO cart, Carrinho carrinho, CartHeader? cartHeader)
    {
        var cartDetail = await _context.CartItems.AsNoTracking().FirstOrDefaultAsync(
            p => p.ProductId == cart.CartItems.FirstOrDefault()
                .ProductId && p.CartHeaderId == cartHeader.Id);
        
        if(carrinho is null)
        {
            await CreateCartItems(carrinho, cartHeader);
        }
        else
        {
            await AddItemsAndUpdate(carrinho, cartDetail);
        }
    }

    private async Task AddItemsAndUpdate(Carrinho carrinho, CartItem? cartDetail)
    {
        carrinho.CartItems.FirstOrDefault().Product = null;
        carrinho.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
        carrinho.CartItems.FirstOrDefault().Id = cartDetail.Id;
        carrinho.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
        _context.CartItems.Update(carrinho.CartItems.FirstOrDefault());
        await _context.SaveChangesAsync();
    }

    private async Task CreateCartItems(Carrinho carrinho, CartHeader? cartHeader)
    {
        carrinho.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
        carrinho.CartItems.FirstOrDefault().Product = null;
        _context.CartItems.Add(carrinho.CartItems.FirstOrDefault());
        await _context.SaveChangesAsync();
    }

    private async Task SaveProductInDataBase(CartDTO cart, Carrinho carrinho)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cart.CartItems.FirstOrDefault().ProductId);

        if(product is null)
        {
            _context.Products.Add(carrinho.CartItems.FirstOrDefault().Product);

            await _context.SaveChangesAsync();
        }
    }
}
