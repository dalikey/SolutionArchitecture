using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace OrderManagement.Infrastructure.Order;

public class OrderMongoDBContext
{
    private readonly IMongoDatabase _database;
    private readonly OrderMySQLContext _sqlContext;

    public OrderMongoDBContext(IMongoDatabase database, OrderMySQLContext sqlContext)
    {
        _database = database;
        _sqlContext = sqlContext;
    }
    public IMongoCollection<Domain.Order> Orders => _database.GetCollection<Domain.Order>("Orders");

    public async Task SQLToMongoDB()
    {
        Console.WriteLine("Migrating support data from SQL to MongoDB");

        List<Domain.Order> sqlOrders = await _sqlContext.Orders.ToListAsync();

        if (sqlOrders.Any())
        {
            await Orders.DeleteManyAsync(Builders<Domain.Order>.Filter.Empty);
            await Orders.InsertManyAsync(sqlOrders);
        }
    }
}

