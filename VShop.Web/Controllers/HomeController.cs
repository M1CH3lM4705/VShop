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

    public HomeController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetAllProducts(
                string.Empty)
            );

    }

    public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
    {
        var product = await _productService.FindProductById(id, string.Empty);

        if(product is null) return View("Error");

        return View(product);
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
