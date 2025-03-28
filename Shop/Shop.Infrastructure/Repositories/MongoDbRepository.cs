using MongoDB.Driver;
using Shop.Domain.Interfaces.Repositories;
using Shop.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Shop.Infrastructure.Repositories
{
    public class MongoDbRepository<T> : IDbRepository<T> where T : class
    {
        private const string IdColumnName = "_id";
        private readonly IMongoCollection<T> _collection;


        public MongoDbRepository(MongoDbContext mongoDbContext)
        {
            _collection = mongoDbContext.MongoDatabase.GetCollection<T>(typeof(T).Name);
        }


        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            return await _collection.Find(filter).SingleAsync();
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> AnyAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).AnyAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Guid id, T entity)
        {
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
            await _collection.DeleteOneAsync(filter);
        }
    }
}
