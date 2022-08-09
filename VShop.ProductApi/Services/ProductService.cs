using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repositorio;

namespace VShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task AddProduct(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);

        await _productRepository.Create(product);

        productDTO.Id = product.Id;
    }

    public async Task UpdateProduct(ProductDTO productDTO)
    {
        var product = _mapper.Map<Product>(productDTO);

        await _productRepository.Update(product);
    }
    public async Task<ProductDTO> GetProductById(int id)
    {
        var product = await _productRepository.GetById(id);

        return _mapper.Map<ProductDTO>(product);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var product = await _productRepository.GetAll();

        return _mapper.Map<IEnumerable<ProductDTO>>(product);
    }

    public async Task RemoveProduct(int id)
    {
        var product = await _productRepository.GetById(id);

        if(product is null)
            throw new ArgumentNullException("Não existe");

        await _productRepository.Delete(id);
    }

}
