using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CouponService : BaseService, ICouponService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/coupon";

    public CouponService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<CouponViewModel> GetDiscountCoupon(string couponCode)
    {
        var client = _clientFactory.CreateClient("DiscontApi");

        var response = await client.GetAsync($"{apiEndpoint}/{couponCode}");

        if(!TratarErrosResponse(response)) return await DeserializarObjetoResponse<CouponViewModel>(response);

        return null;
    }
}
