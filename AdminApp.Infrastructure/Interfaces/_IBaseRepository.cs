using System.Linq.Expressions;

namespace AdminApp.Infrastructure.Interfaces
{
    public interface _IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByAsync(Expression<Func<T, bool>> func);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
