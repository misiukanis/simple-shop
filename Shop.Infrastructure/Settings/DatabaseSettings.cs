namespace Shop.Infrastructure.Settings
{
    public class DatabaseSettings
    {
        public string MongoDbConnectionString { get; set; } = default!;
        public string MongoDbDatabaseName { get; set; } = default!;
        public string EventStoreConnectionString { get; set; } = default!;
    }
}
