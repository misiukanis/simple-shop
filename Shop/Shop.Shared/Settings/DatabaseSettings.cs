namespace Shop.Shared.Settings
{
    public class DatabaseSettings
    {
        public required string MongoDbConnectionString { get; init; }
        public required string MongoDbDatabaseName { get; init; }
        public required string EventStoreConnectionString { get; init; }
    }
}
