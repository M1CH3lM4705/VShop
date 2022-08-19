using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.Core.Utils;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();

            if(productsDto is null) return NotFound("Produtos não encontrados");

            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if(productDto is null) return NotFound("Produto não encontrado");

            return Ok(productDto);
        }

        [HttpPost, Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _productService.AddProduct(productDTO);

            return new CreatedAtRouteResult("GetProduct", new {id = productDTO.Id}, productDTO);
        }

        [HttpPut, Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> Put([FromBody] ProductDTO productDTO)
        {
            
            if(productDTO is null) return BadRequest("Dados Inválidos");

            await _productService.UpdateProduct(productDTO);

            return Ok(productDTO);
        }

        [HttpDelete("{id:int}"), Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if(productDto is null) return NotFound("Não encontrado");

            await _productService.RemoveProduct(id);

            return Ok(productDto);
        }
    }
}