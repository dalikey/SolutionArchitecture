using MongoDB.Bson.Serialization.Attributes;

namespace SupplierManagement.Domain;

public class Supplier
{

    [BsonId]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
    public string Address { get; set; }

    public List<Product>? Products { get; set; } = new List<Product>();
}
