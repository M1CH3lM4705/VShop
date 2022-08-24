using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _options;
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsVm;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        var content = new StringContent(JsonSerializer.Serialize(productVM),
                        Encoding.UTF8, "application/json");

        using(var response = await client.PostAsync(apiEndpoint, content))
        {
            if(response.IsSuccessStatusCode){

                var apiResponse = await response.Content.ReadAsStreamAsync();

                productVM = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
        }
        return productVM;
    }
    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdate = new (); 

        using(var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if(response.IsSuccessStatusCode){

                var apiResponse = await response.Content.ReadAsStreamAsync();

                productUpdate = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
        }
        return productVM;
    }
    public async Task<bool> DeleteProductById(int Id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using(var response = await client.DeleteAsync(apiEndpoint + Id))
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<ProductViewModel> FindProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
                
        using(var response = await client.GetAsync(apiEndpoint + id))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                productVM = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else{
                return null;
            }
        }

        return productVM;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        using(var response = await client.GetAsync(apiEndpoint))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                productsVm = await JsonSerializer
                            .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
            }
            else
                return null;
        }
        return productsVm;
    }

    
}
