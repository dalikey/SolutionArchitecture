using MongoDB.Bson.Serialization.Attributes;

namespace OrderManagement.Domain;

public class Product
{
    [BsonId]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public float Price { get; set; }

    public int Quantity { get; set; } = 0;

    public string Category { get; set; }



    public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
}
