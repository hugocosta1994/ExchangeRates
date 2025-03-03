using System.Linq.Expressions;

namespace ExchangeRates.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null);
        Task<T?> GetAsync(Guid id);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        void Remove(T entity);
        Task RemoveAsync(Guid id);
    }
}