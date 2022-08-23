using AutoMapper;
using VShop.Cart.Models;

namespace VShop.Cart.DTOs.Mappigns;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartDTO, Carrinho>().ReverseMap();

        CreateMap<CartHeaderDTO, CartHeaderDTO>().ReverseMap();

        CreateMap<CartItemDTO, CartItemDTO>().ReverseMap();

        CreateMap<ProductDTO, Product>().ReverseMap();
    }
}
