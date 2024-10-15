using ProductManagement.Domain;

namespace ProductManagement.Infrastructure;

public class ProductRepository
{
    private readonly ProductMySQLContext _SQLcontext;

    public ProductRepository(ProductMySQLContext context)
    {
        _SQLcontext = context;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        try
        {
            await _SQLcontext.Products.AddAsync(product);
            await _SQLcontext.SaveChangesAsync();



            return _SQLcontext.Products.FirstOrDefault(p => p.ProductName == product.ProductName);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

    }

    public async Task<Product?> GetProductAsync(int productId)
    {
        return await _SQLcontext.Products.FindAsync(productId);
    }
}
