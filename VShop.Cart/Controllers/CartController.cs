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
    }
}