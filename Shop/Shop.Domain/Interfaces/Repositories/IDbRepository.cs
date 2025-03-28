using System.Linq.Expressions;

namespace Shop.Domain.Interfaces.Repositories
{
    public interface IDbRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> FindByIdAsync(Guid id);
        Task<bool> AnyAsync();
        Task InsertAsync(T entity);
        Task UpdateAsync(Guid id, T entity);
        Task DeleteAsync(Guid id);
    }
}
