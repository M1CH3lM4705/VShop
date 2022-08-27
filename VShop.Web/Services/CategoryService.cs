using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/categories/";
        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            var response = await client.GetAsync(apiEndpoint);

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<CategoryViewModel>>(response);
        }
    }
}