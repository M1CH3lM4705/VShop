using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoriesService;
        public CategoriesController( ICategoryServices categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO categoryDto)
        {
            if(ModelState.IsValid)
            {
                await _categoriesService.AddCategory(categoryDto);

                return new CreatedAtRouteResult("GetCategory", new {id = categoryDto.CategoryId},
                    categoryDto);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categoriesDto = await _categoriesService.GetCategories();
            if(categoriesDto is null)
                return NotFound();
            
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            if(!(id > 0))
                return BadRequest();
            var categoryDto = await _categoriesService.GetCategoryById(id);

            if(categoryDto is null) return NotFound("Não encontrado");

            return Ok(categoryDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categoriesDto = await _categoriesService.GetCategoriesProducts();

            if(categoriesDto is null) return NotFound();

            return Ok(categoriesDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if(id != categoryDTO.CategoryId) return BadRequest();

            if(categoryDTO is null) return BadRequest();

            await _categoriesService.UpdateCategory(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}"), Authorize]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var categoryDTO = await _categoriesService.GetCategoryById(id);

            if(categoryDTO is null) return NotFound("Categoria não encontrada.");

            await _categoriesService.RemoveCategory(id);

            return Ok(categoryDTO);
        }
    }
}