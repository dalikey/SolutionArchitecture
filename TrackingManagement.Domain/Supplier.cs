using MongoDB.Bson.Serialization.Attributes;

namespace TrackingManagement.Domain;

public class Supplier
{

    [BsonId]
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
}
