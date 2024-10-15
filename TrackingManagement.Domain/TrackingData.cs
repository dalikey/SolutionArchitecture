using MongoDB.Bson.Serialization.Attributes;
using System.Threading;
namespace TrackingManagement.Domain;

public class TrackingData
{
    [BsonId]
    public int TrackingId { get; set; }
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public string Carrier {  get; set; }
    public string Status { get; set; }
    public DateTime EstimatedDelivery {  get; set; }
}

