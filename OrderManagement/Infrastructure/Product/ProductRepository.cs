using OrderManagement.Domain;
using OrderManagement.Infrastructure.Order;
using System.Text.Json;

namespace OrderManagement.Infrastructure.Product;

public class ProductRepository
{
    private readonly OrderMySQLContext _sqlContext;
    
    public ProductRepository(OrderMySQLContext context)
    {
        _sqlContext = context;
    }

    public async Task<bool> AddProductAsync(Domain.Product product)
    {
        try
        {
            await _sqlContext.Products.AddAsync(product);
            await _sqlContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateProductAsync(Domain.Product product)
    {
        try
        {
            _sqlContext.Products.Attach(product);
            _sqlContext.Products.Update(product);
            await _sqlContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Domain.Product> GetProductAsync(string productId)
    {
        return await _sqlContext.Products.FindAsync(productId);
    }
}
