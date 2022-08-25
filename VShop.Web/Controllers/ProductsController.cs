using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Core.Utils;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Controllers
{
    [Route("[controller]/[Action]"), Authorize(Roles = Role.Admin)]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService,
                                  ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {

            return base.View(await _productService.GetAllProducts(await GetToken()));
        }

        

        public async Task<IActionResult> CreateProduct()
        {
            await MontarSelectListDeCategorias();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(productVM);

                if(result != null) return RedirectToAction(nameof(Index));
            }
            
            await MontarSelectListDeCategorias();

            return View(productVM);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            await MontarSelectListDeCategorias();

            var result = await _productService.FindProductById(id);

            if(result is null) return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            if(ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(productVM);


                if(result is not null)
                    return RedirectToAction(nameof(Index));
            }
            return View(productVM);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.FindProductById(id);

            if(result is null)
                return View("Error");
            
            return View(result);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _productService.DeleteProductById(id);

            if(!result) return View("Error");

            return RedirectToAction(nameof(Index));
        }

        private async Task MontarSelectListDeCategorias()
        {
            ViewBag.CategoryId = new SelectList(await 
                _categoryService.GetAllCategories(), "CategoryId", "Name");
        }

        private async Task<string> GetToken()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}