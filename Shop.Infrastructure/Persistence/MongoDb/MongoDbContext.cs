using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shop.Infrastructure.Settings;

namespace Shop.Infrastructure.Persistence.MongoDb
{
    public class MongoDbContext
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoDbContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.MongoDbConnectionString);
            MongoDatabase = client.GetDatabase(databaseSettings.Value.MongoDbDatabaseName);
        }
    }
}
