using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VShop.Cart.DTOs;
using VShop.Cart.Repositories;

namespace VShop.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new
                            ArgumentNullException(nameof(repository));
        }

        public async Task<ActionResult<CartDTO>> GetById()
        {
            return Ok();
        }

        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            return Ok();
        }
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            return Ok();
        }
        public async Task<ActionResult<bool>> DeleteCart(int id)
        {
            return Ok();
        }
    }
}