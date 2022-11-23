using System.Linq.Expressions;

namespace Shop.Domain.Repositories.Interfaces
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<T>> FindAllAsync<T>(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
        Task<bool> AnyAsync<T>();
        Task InsertAsync<T>(T entity);
        Task UpdateAsync<T>(Guid id, T entity);
        Task DeleteAsync<T>(Guid id);
    }
}
