using MongoDB.Bson;
using MongoDB.Driver;
using Shop.Domain.Repositories.Interfaces;
using Shop.Infrastructure.Persistence.MongoDb;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private const string IdColumnName = "_id";
        private readonly IMongoDatabase _mongoDatabase;


        public MongoDbRepository(MongoDbContext mongoDbContext)
        {
            _mongoDatabase = mongoDbContext.MongoDatabase;
        }


        public async Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate)
        {
            var collectionName = typeof(T).Name;
            return await _mongoDatabase.GetCollection<T>(collectionName).Find(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var collectionName = typeof(T).Name;
            return await _mongoDatabase.GetCollection<T>(collectionName).Find(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            var collectionName = typeof(T).Name;
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            return await _mongoDatabase.GetCollection<T>(collectionName).Find(filter).SingleAsync();
        }

        public async Task<bool> AnyAsync<T>()
        {
            var collectionName = typeof(T).Name;
            return await _mongoDatabase.GetCollection<T>(collectionName).Find(new BsonDocument()).AnyAsync();
        }

        public async Task InsertAsync<T>(T entity)
        {
            var collectionName = typeof(T).Name;
            await _mongoDatabase.GetCollection<T>(collectionName).InsertOneAsync(entity);
        }

        public async Task UpdateAsync<T>(Guid id, T entity)
        {
            var collectionName = typeof(T).Name;
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            await _mongoDatabase.GetCollection<T>(collectionName).ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync<T>(Guid id)
        {
            var collectionName = typeof(T).Name;
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            await _mongoDatabase.GetCollection<T>(collectionName).DeleteOneAsync(filter);
        }
    }
}

