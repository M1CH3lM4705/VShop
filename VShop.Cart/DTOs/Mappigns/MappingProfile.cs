using AutoMapper;
using VShop.Cart.Models;

namespace VShop.Cart.DTOs.Mappigns;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartDTO, Carrinho>().ReverseMap();

        CreateMap<CartHeaderDTO, CartHeader>().ReverseMap();

        CreateMap<CartItemDTO, CartItem>().ReverseMap();

        CreateMap<ProductDTO, Product>().ReverseMap();
    }
}
