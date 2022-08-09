using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositorio;

namespace VShop.ProductApi.Services
{
    
    public class CategoryService : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.Create(category);

            categoryDTO.CategoryId = category.CategoryId;
        }
        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.Update(category);
        }

        
        public async Task RemoveCategory(int id)
        {
            var categoryRemove = await _categoryRepository.GetById(id);

            if(categoryRemove is null)
                throw new Exception("Não existe.");
            await _categoryRepository.Delete(id);
        }
        public async Task<IEnumerable<CategoryDTO>> GetCategoriesProducts()
        {
            var categoriesEntity = await _categoryRepository.GetCatogoriesProducts();

            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetAll();

            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetById(id);

            return _mapper.Map<CategoryDTO>(category);
        }
        
    }
}