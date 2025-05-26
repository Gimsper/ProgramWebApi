using System.Linq.Expressions;
using AdminApp.Utils.Models;

namespace AdminApp.Services.Interfaces
{
    public interface _IBaseService<T> where T : class
    {
        Task<ResultOperation<T>> GetAllAsync();
        Task<ResultOperation<T>> GetByIdAsync(int id);
        Task<ResultOperation<T>> GetByAsync(Expression<Func<T, bool>> func);
        Task<ResultOperation> AddAsync(T entity);
        Task<ResultOperation> UpdateAsync(T entity);
        Task<ResultOperation> DeleteAsync(int id);
    }
}