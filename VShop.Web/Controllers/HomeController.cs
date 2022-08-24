using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
    public HomeController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetAllProducts(
                string.Empty)
            );

    }

    [Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var product = await _productService.FindProductById(id);

        if(product is null) return View("Error");

        return View(product);
    }

    [HttpPost, ActionName("ProductDetails"), Authorize]
    public async Task<ActionResult<ProductViewModel>> ProductDetailsPost(ProductViewModel productVM)
    {
        if(!ModelState.IsValid) return View(productVM);

        CartViewModel cart = new()
        {
            CartHeader = new CartHeaderViewModel
            {
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
            }
        };

        CartItemViewModel cartItem = new()
        {
            Quantity = productVM.Quantity,
            ProductId = productVM.Id,
            Product = await _productService.FindProductById(productVM.Id)
        };

        List<CartItemViewModel> cartItemsVM = new();
        cartItemsVM.Add(cartItem);
        cart.CartItems = cartItemsVM;

        var result = await _cartService.AddItemToCartAsync(cart);

        if(result is not null) return RedirectToAction(nameof(Index));

        return View(productVM);
    }


    [Authorize]
    public async Task<IActionResult> Login()
    {
        var accesToken = await HttpContext.GetTokenAsync("access_token");
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout(){
        return SignOut("Cookies", "oidc");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
