using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repositorio
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<Category>> GetCatogoriesProducts();
        Task<Category> GetById(int Id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(int Id);
    }
}