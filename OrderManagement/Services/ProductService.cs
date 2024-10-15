using OrderManagement.Domain;
using OrderManagement.Domain.Events;
using OrderManagement.Infrastructure;
using OrderManagement.Infrastructure.Product;

namespace OrderManagement.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> AddProduct(Product product)
    {
        var result = await _productRepository.AddProductAsync(product);

        if (!result)
            return false;

        // BUS NAAR EVENT

        return true;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var result = await _productRepository.UpdateProductAsync(product);

        if (!result)
            return false;

        // BUS NAAR EVENT

        return true;
    }
}
