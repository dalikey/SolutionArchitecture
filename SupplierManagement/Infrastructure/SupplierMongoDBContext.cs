using SupplierManagement.Domain;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace SupplierManagement.Infrastructure
{
    public class SupplierMongoDBContext
    {
        private readonly IMongoDatabase _database;
        private readonly SupplierMySQLContext _sqlContext;

        public SupplierMongoDBContext(IMongoDatabase database, SupplierMySQLContext sqlContext)
        {
            _database = database;
            _sqlContext = sqlContext;
        }

        public IMongoCollection<Supplier> Suppliers => _database.GetCollection<Supplier>("Suppliers");

        public async Task SQLToMongoDB()
        {
            Console.WriteLine("Migrating support data from SQL to MongoDB");
            // Retrieve data from SQL database
            List<Supplier> sqlSuppliers = await _sqlContext.Suppliers.ToListAsync();

            // Insert data into MongoDB
            if (sqlSuppliers.Any())
            {
                await Suppliers.InsertManyAsync(sqlSuppliers);
            }
        }
    }
}
