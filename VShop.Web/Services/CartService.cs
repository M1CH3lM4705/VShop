using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CartService : BaseService, ICartService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/cart";
    public CartService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<CartViewModel> GetCartByUserIdAsync(string userId, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");

        var result = await client.GetAsync($"{apiEndpoint}/getCart/{userId}");

        TratarErrosResponse(result);

        return await DeserializarObjetoResponse<CartViewModel>(result);
    }
    public async Task<CartViewModel> AddItemToCartAsync(CartViewModel cartVM)
    {
        var client = _clientFactory.CreateClient("CartApi");
        
        var content = ObterConteudo(cartVM);

        var response = await client.PostAsync($"{apiEndpoint}/addcart", content);

        if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<CartViewModel>(response);

        return null;
    }


    public async Task<CartViewModel> UpdateCartAsync(CartViewModel cartVM, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        
        var content = ObterConteudo(cartVM);

        var response = await client.PutAsync($"{apiEndpoint}/updatecart", content);

        if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<CartViewModel>(response);

        return null;
    }
    public async Task<bool> RemoveItemFromCartAsync(int cartId, string token)
    {
        var client = _clientFactory.CreateClient("CartApi");
        
        var response = await client.DeleteAsync($"{apiEndpoint}/deletecart/{cartId}");

        return await DeserializarObjetoResponse<bool>(response);
    }

}
