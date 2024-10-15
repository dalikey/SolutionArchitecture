using MongoDB.Bson.Serialization.Attributes;

namespace TrackingManagement.Domain;

public class Product
{

    [BsonId]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
}
