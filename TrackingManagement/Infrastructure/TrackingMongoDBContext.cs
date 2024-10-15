using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using TrackingManagement.Domain;

namespace TrackingManagement.Infrastructure;

public class TrackingMongoDBContext : DbContext
{
    private readonly IMongoDatabase _database;
    private readonly TrackingMySQLContext _sqlContext;

    public TrackingMongoDBContext(IMongoDatabase database, TrackingMySQLContext trackingContext)
    {
        _database = database;
        _sqlContext = trackingContext;
    }

    public IMongoCollection<TrackingData> Trackings => _database.GetCollection<TrackingData>("Trackings");


    //TODO implement to SQL

}
