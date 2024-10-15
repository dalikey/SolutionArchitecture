using SupplierManagement.Domain;

namespace SupplierManagement.Infrastructure;

public class SupplierRepository
{
    private readonly SupplierMySQLContext _SQLcontext;
    private readonly SupplierMongoDBContext _MongoDBcontext;

    public SupplierRepository(SupplierMySQLContext context, SupplierMongoDBContext mongoDBcontext)
    {
        _SQLcontext = context;
        _MongoDBcontext = mongoDBcontext;
    }

    public async Task<bool> AddSupplierAsync(Supplier supplier)
    {
        try
        {
            await _SQLcontext.Suppliers.AddAsync(supplier);
            await _SQLcontext.SaveChangesAsync();

            return true;
        } catch 
        {
            return false;
        }
        
    }


    public async Task<Product> AddProductOfSupplierAsync(Product product, int supplierId)
    {
        var supplierInSQL = await _SQLcontext.Suppliers.FindAsync(supplierId);

        supplierInSQL.Products.Add(product);
        await _SQLcontext.SaveChangesAsync();
        return supplierInSQL.Products.Find(p => p.ProductName == product.ProductName);
    }

    public async Task<Supplier?> GetSupplierAsync(string supplierId)
    {
        return await _SQLcontext.Suppliers.FindAsync(supplierId);
    }
}
