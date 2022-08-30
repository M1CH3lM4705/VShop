using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Cart.DTOs;
using VShop.Cart.Repositories;

namespace VShop.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize ]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new
                            ArgumentNullException(nameof(repository));
        }

        [HttpGet("getcart/{id}")]
        public async Task<ActionResult<CartDTO>> GetById(string id)
        {
            var cart = await _repository.GetCartByUserIdAsync(id);
            if(cart is null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("addcart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cartDto = await _repository.UpdateCartAsync(cartDTO);

            if(cartDto is null) return NotFound();
            return Ok(cartDTO);
        }

        [HttpPut("updatecart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            var cartDto = await _repository.UpdateCartAsync(cartDTO);

            if(cartDTO is null) return NotFound();
            return Ok(cartDTO);
        }

        [HttpDelete("deletecart/{id}")]
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            var status = await _repository.DeleteItemCartAsync(id);
            if(!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("applycoupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var result = await _repository.ApplyCouponAsync(cartDTO.CartHeader.UserId,
                                                            cartDTO.CartHeader.CouponCode);
            
            if(!result)
                return NotFound($"CartHeader not found for userId = {cartDTO.CartHeader.UserId}");

            return Ok(result);
        }

        [HttpDelete("deletecoupon/{userId}")]
        public async Task<ActionResult<CartDTO>> DeleteCoupon(string userId)
        {
            var result = await _repository.DeleteCouponAsync(userId);

            if(!result)
                return NotFound($"Cupom de Desconto não encontrado para este usúario {userId}");
            
            return Ok(result);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutDto)
        {
            var cart = await _repository.GetCartByUserIdAsync(checkoutDto.UserId);

            if(cart is null)
            {
                return NotFound($"Nenhum carrinho foi encontrado para {checkoutDto.UserId}");
            }

            checkoutDto.CartItems = cart.CartItems;
            checkoutDto.Cliente.DataAtual = DateTime.Now;

            return Ok(checkoutDto);
        }
    }
}