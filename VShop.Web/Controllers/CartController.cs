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
        private readonly ICouponService _couponService;
        private readonly IAspNetUser _user;
        public CartController(ICartService cartService, IAspNetUser user, ICouponService couponService)
        {
            _cartService = cartService;
            _user = user;
            _couponService = couponService;
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

        [HttpPost, Authorize]
        public async Task<IActionResult> AppluCoupon(CartViewModel cartVM)
        {
            if(!ModelState.IsValid) return View(cartVM);

            var result = await _cartService.ApplyCouponAsync(cartVM);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> DeleteCoupon(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var result = await _cartService.RemoveCouponAsync(userId);

            if(!result) return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout()
        {
            return View(await GetCartByUser());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CartViewModel cartVM)
        {
            if(ModelState.IsValid)
            {
                var resutl = await _cartService.CheckoutAsync(cartVM.CartHeader);

                if(resutl is not null) return RedirectToAction(nameof(ChekcoutCompleted));
            }
            return View(cartVM);
        }

        private async Task<CartViewModel> GetCartByUser()
        {
            var cart = await _cartService.GetCartByUserIdAsync(_user.ObterUserId().ToString());

            if(cart?.CartHeader is not null)
            {
                if(cart.CartHeader.CupomEstaVazio())
                {
                    var coupon = await _couponService.GetDiscountCoupon(cart.GetCoupon());

                    if(coupon.ExistCoupon())
                        cart.AddCoupon(coupon.CouponCode);
                }
                cart.GetTotalAmount();

                cart.GetDiscount();
            }
            return cart;
        }
    }
}