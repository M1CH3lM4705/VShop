

using AutoMapper;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.DTOs.Mappings;
public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CouponDTO, Coupon>().ReverseMap();
    }
}
