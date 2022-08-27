using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Core.Usuario;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    [Route("[controller]/[Action]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAspNetUser _user;
        public CartController(ICartService cartService, IAspNetUser user)
        {
            _cartService = cartService;
            _user = user;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            CartViewModel? cartVM = await GetCartByUser();
            if(cartVM is null)
            {
                ModelState.AddModelError("Carrinho não encontrado", "Não existe nenhum carrinho");
                return View("/Views/Cart/CartNotFound.cshtml");
            }
            return View(cartVM);
        }

        [Authorize]
        public async Task<IActionResult>RemoverItem(int id)
        {
            var result = await _cartService.RemoveItemFromCartAsync(id);

            if(result) return RedirectToAction(nameof(Index));

            return View(id);
        }

        private async Task<CartViewModel> GetCartByUser()
        {
            var cart = await _cartService.GetCartByUserIdAsync(_user.ObterUserId().ToString());

            if(cart?.CartHeader is not null)
            {
                cart.GetTotalAmount();
            }
            return cart;
        }
    }
}