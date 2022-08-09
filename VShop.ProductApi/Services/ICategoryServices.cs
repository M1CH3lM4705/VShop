using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services;

public interface ICategoryServices
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategoriesProducts();
    Task<CategoryDTO> GetCategoryById(int id);
    Task AddCategory(CategoryDTO categoryDTO);
    Task UpdateCategory(CategoryDTO categoryDTO);
    Task RemoveCategory(int id);
}
